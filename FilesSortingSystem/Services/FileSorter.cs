using FilesSortingSystem.Services.Interfaces;
using System.Diagnostics;
using ILogger = FilesSortingSystem.Services.Interfaces.ILogger;

namespace FilesSortingSystem.Services
{
    public class FileSorter : IFileSorter
    {
        private Dictionary<string, string> rules = [];
        private readonly IRulesEngine _rulesEngine;
        private readonly ILogger _logger;
        private readonly ISettings _settings;
        private readonly IUtils _utils;

        public FileSorter(IRulesEngine rulesEngine, ILogger logger, ISettings settings, IUtils utils)
        {
            _rulesEngine = rulesEngine;
            _logger = logger;
            _settings = settings;
            _utils = utils;
        }

        public void SetRules(Dictionary<string, string> extensionToFolder)
        {
            rules = new Dictionary<string, string>(extensionToFolder, StringComparer.OrdinalIgnoreCase);
        }

        public void Sort(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            var allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

            Debug.WriteLine($"[DEBUG] Total files found: {allFiles.Length}");

            foreach (var filePath in allFiles)
            {
                string ext = _utils.GetFileExtension(filePath);

                if (rules.TryGetValue(ext, out var category))
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

                    string subFolderName = GetSubFolderName(ext); // ✅ FIXED: Pass extension here
                    string destination = Path.Combine(basePath, subFolderName);

                    _utils.EnsureDirectory(destination);
                    _utils.MoveFile(filePath, destination);

                    Debug.WriteLine($"[INFO] Moved file '{filePath}' to '{destination}'");
                    _logger.logFileMoved(filePath, destination);
                }
                else
                {
                    var unknownCategory = ext.Trim('.').ToUpperInvariant();
                    var fallbackPath = Path.Combine(folderPath, unknownCategory);

                    _utils.EnsureDirectory(fallbackPath);
                    _utils.MoveFile(filePath, fallbackPath);

                    Debug.WriteLine($"[INFO] Moved unknown file '{filePath}' to '{fallbackPath}'");
                    _logger.logFileMoved(filePath, fallbackPath);
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
