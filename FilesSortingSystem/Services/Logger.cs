using FilesSortingSystem.Core.InputObjects;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.Services
{
    public class Logger(IAddLogsEntryInteractor addLogsEntryInteractor) : ILogger
    {
        public void logFileMoved(List<LogEntryInput> logEntryInput)
        {
            try
            {
                Task.Run(async () => await addLogsEntryInteractor.Handle(logEntryInput));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging file move: {ex.Message}");
            }
        }
    }
}