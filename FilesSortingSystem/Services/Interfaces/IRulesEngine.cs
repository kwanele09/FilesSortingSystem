namespace FilesSortingSystem.Services.Interfaces
{
    public interface IRulesEngine
    {
        string ResolvePath(string basePath, string relativePath);
    }
}