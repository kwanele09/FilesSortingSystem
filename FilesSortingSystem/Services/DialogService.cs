using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.Services
{
    public class DialogService : IDialogService
    {
        public Task DisplayAlertAsync(string title, string message, string cancel)
        {
            if (Application.Current?.MainPage != null)
            {
                return Application.Current.MainPage.DisplayAlert(title, message, cancel);
            }
            return Task.CompletedTask;
        }
    }
}
