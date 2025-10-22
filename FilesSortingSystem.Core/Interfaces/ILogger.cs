using FilesSortingSystem.Core.InputObjects;

namespace FilesSortingSystem.Core.Interfaces
{
    public interface ILogger
    {
        void logFileMoved(List<LogEntryInput> logEntryInput);
    }
}