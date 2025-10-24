namespace FilesSortingSystem.Core.Interfaces
{
    public interface IFileSorter
    {
        Task SortAsync(string folderPath, bool excludeSubfolders);
    }
}