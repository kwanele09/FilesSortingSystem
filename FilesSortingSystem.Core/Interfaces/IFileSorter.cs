namespace FilesSortingSystem.Core.Interfaces
{
    public interface IFileSorter
    {
        void SetRules(Dictionary<string, string> extensionToFolder);
        void Sort(string folderPath);
    }
}