using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FilesSortingSystem.ViewModels
{
    public partial class LogsViewModel : ObservableObject
    {
        private readonly string logFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "sort_log.txt");

        [ObservableProperty] private string logContent = string.Empty;
        public bool HasLogs => !string.IsNullOrWhiteSpace(LogContent) && LogContent != "No logs found.";

        [RelayCommand]
        public async Task LoadLogsAsync()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    LogContent = await File.ReadAllTextAsync(logFilePath);
                }
                else
                {
                    LogContent = "No logs found.";
                }
            }
            catch (Exception ex)
            {
                LogContent = $"Error loading logs: {ex.Message}";
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
                LogContent = string.Empty;
            }
            catch (Exception ex)
            {
                LogContent = $"Error deleting logs: {ex.Message}";
            }
        }

        [RelayCommand]
        public async Task CloseAsync()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

    }
}
