using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilesSortingSystem.Models;
using System.Collections.ObjectModel;

namespace FilesSortingSystem.ViewModels
{
    public partial class LogsViewModel : ObservableObject
    {
        private readonly string logFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "sort_log.txt");

        [ObservableProperty]
        private ObservableCollection<LogEntry> logs = new();

        [ObservableProperty]
        private bool hasLogs;

        [RelayCommand]
        public async Task LoadLogsAsync()
        {
            try
            {
                Logs.Clear();

                if (File.Exists(logFilePath))
                {
                    var lines = await File.ReadAllLinesAsync(logFilePath);
                    foreach (var line in lines)
                    {
                        Logs.Add(new LogEntry
                        {
                            Message = line,
                            IsMoved = true 
                        });
                    }
                }

                HasLogs = Logs.Count > 0;
            }
            catch (Exception ex)
            {
                Logs.Clear();
                Logs.Add(new LogEntry { Message = $"Error loading logs: {ex.Message}", IsMoved = false });
                HasLogs = false;
            }
        }

        [RelayCommand]
        public async Task ClearLogsAsync()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    File.Delete(logFilePath);
                }

                Logs.Clear();
                HasLogs = false;
            }
            catch (Exception ex)
            {
                Logs.Clear();
                Logs.Add(new LogEntry { Message = $"Error deleting logs: {ex.Message}", IsMoved = false });
                HasLogs = false;
            }
        }

        [RelayCommand]
        public async Task CloseAsync()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}
