namespace FilesSortingSystem.Services.Interfaces
{
    public interface ILogger
    {
        void logFileMoved(string from, string to);
    }
}