namespace FilesSortingSystem.Core.Interfaces
{
    public interface IRulesEngine
    {
        string ResolvePath(string basePath, string relativePath);
    }
}