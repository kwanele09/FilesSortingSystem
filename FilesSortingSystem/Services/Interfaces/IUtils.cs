namespace FilesSortingSystem.Services.Interfaces
{
    public interface IUtils
    {
        void EnsureDirectory(string path);
        string GetFileExtension(string path);
        void MoveFile(string src, string dstFolder);
    }
}