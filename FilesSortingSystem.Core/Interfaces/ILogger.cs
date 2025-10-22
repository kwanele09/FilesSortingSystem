namespace FilesSortingSystem.Core.Interfaces
{
    public interface ILogger
    {
        void logFileMoved(string from, string to);
    }
}