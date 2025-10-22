using FilesSortingSystem.Interfaces;

namespace FilesSortingSystem.Services
{
    public class ImageSubCategoryResolver : ISubCategoryResolver
    {
        public string? GetSubDirectory(string category, string extension)
        {
            if (!category.Equals("images", StringComparison.OrdinalIgnoreCase) &&
                !category.Equals("pictures", StringComparison.OrdinalIgnoreCase))
                return null;

            return extension.ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "JPEG",
                ".png" => "PNG",
                ".gif" => "GIF",
                ".bmp" => "BMP",
                ".tiff" => "TIFF",
                _ => null
            };
        }
    }
}
