using FilesSortingSystem.Core.Interfaces;
using System.Diagnostics;

namespace FilesSortingSystem.Services
{
    public class FileSorter(IRulesEngine rulesEngine, ILogger logger, ISettings settings, IUtils utils) : IFileSorter
    {
        private Dictionary<string, string> _rules = [];

        public void SetRules(Dictionary<string, string> extensionToFolder)
        {
            _rules = new Dictionary<string, string>(extensionToFolder, StringComparer.OrdinalIgnoreCase);
        }

        public void Sort(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            var allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

            Debug.WriteLine($"[DEBUG] Total files found: {allFiles.Length}");

            foreach (var filePath in allFiles)
            {
                string ext = utils.GetFileExtension(filePath);

                if (_rules.TryGetValue(ext, out var category))
                {
                    string? basePath = GetSpecialFolderPath(category);

                    if (string.IsNullOrEmpty(basePath))
                    {
                        basePath = Path.Combine(folderPath, category);
                        Debug.WriteLine($"[DEBUG] Using fallback path for category '{category}': {basePath}");
                    }
                    else
                    {
                        Debug.WriteLine($"[DEBUG] Using special folder for '{category}': {basePath}");
                    }

                    string subFolderName = GetSubFolderName(ext);
                    string destination = Path.Combine(basePath, subFolderName);

                    utils.EnsureDirectory(destination);
                    utils.MoveFile(filePath, destination);

                    Debug.WriteLine($"[INFO] Moved file '{filePath}' to '{destination}'");
                    logger.logFileMoved(filePath, destination);
                }
                else
                {
                    var unknownCategory = ext.Trim('.').ToUpperInvariant();
                    var fallbackPath = Path.Combine(folderPath, unknownCategory);

                    utils.EnsureDirectory(fallbackPath);
                    utils.MoveFile(filePath, fallbackPath);

                    Debug.WriteLine($"[INFO] Moved unknown file '{filePath}' to '{fallbackPath}'");
                    logger.logFileMoved(filePath, fallbackPath);
                }
            }
        }

        private string GetSubFolderName(string extension)
        {
            return extension.ToLowerInvariant() switch
            {
                ".pdf" => "PDFs",
                ".doc" or ".docx" => "Docs",
                ".txt" => "TextFiles",
                ".xls" or ".xlsx" => "Spreadsheets",
                ".ppt" or ".pptx" => "Presentations",
                ".epub" or ".mobi" => "Ebooks",

                ".jpg" or ".jpeg" or ".png" or ".bmp" or ".gif" or ".svg" or ".tiff" => "Images",
                ".mp3" or ".wav" or ".aac" or ".flac" => "AudioFiles",
                ".mp4" or ".mov" or ".wmv" or ".avi" or ".mkv" => "Videos",

                _ => extension.Trim('.').ToUpperInvariant() + " Files"
            };
        }

        private string? GetSpecialFolderPath(string category)
        {
            return category.ToLowerInvariant() switch
            {
                "documents" => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "images" or "pictures" => Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                "audio" or "music" => Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                "videos" => Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                _ => null
            };
        }
    }
}
