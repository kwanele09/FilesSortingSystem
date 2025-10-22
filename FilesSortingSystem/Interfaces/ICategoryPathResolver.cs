namespace FilesSortingSystem.Interfaces;

public interface ICategoryPathResolver
{
    string ResolvePath(string category, string baseFolder);
}