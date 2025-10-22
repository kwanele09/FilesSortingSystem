namespace FilesSortingSystem.Core.Interfaces
{
    public interface ILogger
    {
        public void logFileMoved(string from, string to, DateTime moveDateTime);

    }
}