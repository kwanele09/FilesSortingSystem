using FilesSortingSystem.Interfaces;

namespace FilesSortingSystem.Services
{
    public class CategoryPathResolver : ICategoryPathResolver
    {
        public string ResolvePath(string category, string baseFolder)
        {
            if (string.IsNullOrWhiteSpace(category))
                return baseFolder;

            var normalized = category.ToLowerInvariant();

            return normalized switch
            {
                // ✅ Documents
                "documents" or "document" or "docs" =>
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),

                // ✅ Pictures
                "images" or "image" or "pictures" or "picture" or "imgs" or "photos" =>
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),

                // ✅ Music
                "audio" or "music" or "musics" or "songs" =>
                    Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),

                // ✅ Videos
                "videos" or "video" or "vid" or "movie" or "movies" =>
                    Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),

                // ✅ Downloads
                "downloads" or "download" =>
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),

                // ✅ Desktop
                "desktop" =>
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),

                // fallback
                _ => Path.Combine(baseFolder, category)
            };
        }
    }
}