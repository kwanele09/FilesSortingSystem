using FilesSortingSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSortingSystem.Services
{
    public class Logger : ILogger
    {
        private readonly string logFile = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "sort_log.txt");

        public void logFileMoved(string from, string to)
        {
            try
            {
                File.AppendAllText(logFile, $"Moved: {from} -> {to}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging file move: {ex.Message}");
            }
        }
    }
}
