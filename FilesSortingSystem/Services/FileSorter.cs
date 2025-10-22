using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.Interfaces;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

public class FileSorter(
    IGetRulesInteractor rulesInteractor,
    ILogger logger,
    IUtils utils,
    IEnumerable<ISubCategoryResolver> subCategoryResolvers,
    ICategoryPathResolver categoryPathResolver) : IFileSorter
{
    public async Task SortAsync(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

        // ✅ Load rules from DB and JSON
        var userRules = await rulesInteractor.Handle() ?? new List<FileSortRule>();
        var defaultRules = await LoadDefaultRulesAsync();

        var allRules = defaultRules
            .Concat(userRules)
            .GroupBy(r => (r.Extension.ToLowerInvariant(), r.Category.ToLowerInvariant()))
            .Select(g => g.First())
            .ToList();

        var extensionToCategory = allRules.ToDictionary(
            r => r.Extension,
            r => r.Category,
            StringComparer.OrdinalIgnoreCase
        );

        // ✅ Sort files
        var allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
        foreach (var file in allFiles)
        {
            try
            {
                string ext = utils.GetFileExtension(file);

                if (extensionToCategory.TryGetValue(ext, out var category))
                {
                    // ✅ Resolve base path using CategoryPathResolver
                    string basePath = categoryPathResolver.ResolvePath(category, folderPath);

                    // ✅ Optional subcategory (PDF, PNG, etc.)
                    string? subCategory = subCategoryResolvers
                        .Select(r => r.GetSubDirectory(category, ext))
                        .FirstOrDefault(sub => sub != null);

                    string finalPath = string.IsNullOrWhiteSpace(subCategory)
                        ? basePath
                        : Path.Combine(basePath, subCategory);

                    utils.EnsureDirectory(finalPath);
                    utils.MoveFile(file, finalPath);
                    logger.logFileMoved(file, finalPath);
                }
                else
                {
                    // ✅ Fallback: create folder by extension
                    var fallback = Path.Combine(folderPath, ext.Trim('.').ToUpperInvariant());
                    utils.EnsureDirectory(fallback);
                    utils.MoveFile(file, fallback);
                    logger.logFileMoved(file, fallback);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileSorter] Failed to sort file {file}: {ex}");
            }
        }
    }

    private async Task<List<FileSortRule>> LoadDefaultRulesAsync()
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "FilesSortingSystem.Configurations.DefaultFileSortRules.json";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                Debug.WriteLine($"[FileSorter] Embedded resource not found: {resourceName}");
                return new List<FileSortRule>();
            }

            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var rules = JsonSerializer.Deserialize<List<FileSortRule>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return rules ?? new List<FileSortRule>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[FileSorter] Failed to load default rules: {ex}");
            return new List<FileSortRule>();
        }
    }
}
