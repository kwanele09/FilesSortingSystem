using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.InputObjects;
using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.Interfaces;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace FilesSortingSystem.Services
{
    public class FileSorter(
        IGetRulesInteractor rulesInteractor,
        ILogger logger,
        IUtils utils,
        IEnumerable<ISubCategoryResolver> subCategoryResolvers,
        ICategoryPathResolver categoryPathResolver) : IFileSorter
    {
        public async Task SortAsync(string folderPath, bool excludeSubfolders)
        {
            ValidateFolderPath(folderPath);

            var allRules = await GetAllRulesAsync();
            var extensionToCategory = MapExtensionsToCategories(allRules);

            var allFiles = GetFiles(folderPath, excludeSubfolders);

            foreach (var file in allFiles)
                await ProcessFileAsync(file, folderPath, extensionToCategory);
            DeleteEmptyDirectories(folderPath);
        }

        private static void ValidateFolderPath(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");
        }

        private async Task<List<FileSortRule>> GetAllRulesAsync()
        {
            var userRules = await rulesInteractor.Handle();
            var defaultRules = await LoadDefaultRulesAsync();

            return defaultRules
                .Concat(userRules)
                .GroupBy(r => (r.Extension.ToLowerInvariant(), r.Category.ToLowerInvariant()))
                .Select(g => g.First())
                .ToList();
        }

        private static Dictionary<string, string> MapExtensionsToCategories(IEnumerable<FileSortRule> rules)
            => rules.ToDictionary(
                r => r.Extension,
                r => r.Category,
                StringComparer.OrdinalIgnoreCase);

        private static string[] GetFiles(string folderPath, bool excludeSubfolders)
        {
            var searchOption = excludeSubfolders ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            return Directory.GetFiles(folderPath, "*", searchOption);
        }

        private async Task ProcessFileAsync(string file, string folderPath, Dictionary<string, string> extensionToCategory)
        {
            try
            {
                var ext = utils.GetFileExtension(file);
                var (category, finalPath) = ResolveFileDestination(ext, folderPath, extensionToCategory);

                utils.EnsureDirectory(finalPath);
                utils.MoveFile(file, finalPath);
                await LogFileMoveAsync(file, finalPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileSorter] Failed to sort file {file}: {ex}");
            }
        }

        private (string category, string finalPath) ResolveFileDestination(string ext, string folderPath, Dictionary<string, string> extensionToCategory)
        {
            if (extensionToCategory.TryGetValue(ext, out var category))
            {
                var basePath = categoryPathResolver.ResolvePath(category, folderPath);

                var subCategory = subCategoryResolvers
                    .Select(r => r.GetSubDirectory(category, ext))
                    .FirstOrDefault(sub => sub != null);

                var finalPath = string.IsNullOrWhiteSpace(subCategory)
                    ? basePath
                    : Path.Combine(basePath, subCategory);

                return (category, finalPath);
            }

            var defaultCategory = ext.Trim('.').ToUpperInvariant();
            var defaultPath = Path.Combine(folderPath, defaultCategory);
            return (defaultCategory, defaultPath);
        }

        private Task LogFileMoveAsync(string file, string finalPath)
        {
            var logEntry = new LogEntryInput
            {
                IsMoved = true,
                Message = $"Moved: {file} -> {finalPath}",
                MoveDateTime = DateTime.Now
            };

            logger.logFileMoved([logEntry]);
            return Task.CompletedTask;
        }

        private async Task<List<FileSortRule>> LoadDefaultRulesAsync()
        {
            try
            {
                await using var stream = GetEmbeddedRulesStream();
                if (stream == null)
                    return LogAndReturnEmpty($"Embedded resource not found.");

                var json = await ReadJsonFromStreamAsync(stream);
                return DeserializeRules(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileSorter] Failed to load default rules: {ex}");
                return [];
            }
        }

        private static Stream? GetEmbeddedRulesStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "FilesSortingSystem.Configurations.DefaultFileSortRules.json";
            return assembly.GetManifestResourceStream(resourceName);
        }

        private static async Task<string> ReadJsonFromStreamAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private static List<FileSortRule> DeserializeRules(string json)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<FileSortRule>>(json, options) ?? [];
        }

        private static List<FileSortRule> LogAndReturnEmpty(string message)
        {
            Debug.WriteLine($"[FileSorter] {message}");
            return [];
        }

        private static void DeleteEmptyDirectories(string rootPath)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(rootPath))
                {
                    DeleteEmptyDirectories(directory); 

                    if (!Directory.EnumerateFileSystemEntries(directory).Any())
                    {
                        Directory.Delete(directory);
                        Debug.WriteLine($"[FileSorter] Deleted empty directory: {directory}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileSorter] Failed to delete empty directories: {ex}");
            }
        }
    }
}
