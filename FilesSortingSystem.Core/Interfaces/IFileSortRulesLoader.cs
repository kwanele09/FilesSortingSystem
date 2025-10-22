namespace FilesSortingSystem.Core.Interfaces
{
    public interface IFileSortRulesLoader
    {
        Task<Dictionary<string, string>> Handle(string jsonFilePath, bool caseSensitive = false);
    }
}