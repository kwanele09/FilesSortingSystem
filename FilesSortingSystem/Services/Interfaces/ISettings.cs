namespace FilesSortingSystem.Services.Interfaces
{
    public interface ISettings
    {
        Dictionary<string, string> LoadRules(string path);
        void SaveRules(string path, Dictionary<string, string> rules);
    }
}