using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.Models;
using System.Collections.ObjectModel;

namespace FilesSortingSystem.ViewModels
{
    public partial class LogsViewModel(IGetLogsEntryInteractor getLogsInteractor) : ObservableObject
    {
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

                var logEntries = await getLogsInteractor.Handle();

                foreach (var log in logEntries)
                {
                    Logs.Add(new LogEntry
                    {
                        Message = log.Message,
                        IsMoved = log.IsMoved,
                        MoveDateTime = DateTime.Now
                    });
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
                Logs.Clear();
                HasLogs = false;
            }
            catch (Exception ex)
            {
                Logs.Clear();
                Logs.Add(new LogEntry { Message = $"Error clearing logs: {ex.Message}", IsMoved = false });
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
