using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.Services
{
    public class Logger : ILogger
    {
        private readonly string _logFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "sort_log.txt");

        public void logFileMoved(string from, string to, DateTime moveDateTime)
        {
            try
            {
                File.AppendAllText(_logFile, $"[{moveDateTime:yyyy-MM-dd HH:mm:ss}] Moved: {from} -> {to}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging file move: {ex.Message}");
            }
        }
    }
}