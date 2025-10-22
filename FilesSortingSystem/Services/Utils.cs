using FilesSortingSystem.Core.Interfaces;
using System.Diagnostics;

namespace FilesSortingSystem.Services
{
    public class Utils : IUtils
    {
        public string GetFileExtension(string path)
        {
            return Path.GetExtension(path)?.ToLowerInvariant() ?? string.Empty;
        }

        public void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void MoveFile(string src, string dstFolder)
        {
            if (!File.Exists(src))
            {
                Debug.WriteLine($"[WARN] Source file does not exist: {src}");
                return;
            }

            string fileName = Path.GetFileName(src);
            string destination = Path.Combine(dstFolder, fileName);

            if (File.Exists(destination))
            {
                string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{Path.GetExtension(fileName)}";
                destination = Path.Combine(dstFolder, newFileName);
                Debug.WriteLine($"[INFO] File already exists. Renaming to: {newFileName}");
            }
            try
            {
                File.Move(src, destination);
                Debug.WriteLine($"[INFO] Moved file from '{src}' to '{destination}'");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Failed to move file '{src}' to '{destination}': {ex.Message}");
            }
        }
    }
}
