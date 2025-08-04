using FilesSortingSystem.Services.Interfaces;

namespace FilesSortingSystem.Services
{
    public class RulesEngine : IRulesEngine
    {
        public string ResolvePath(string basePath, string relativePath)
        {
            return Path.Combine(basePath, relativePath);
        }
    }
}