using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.Interfaces;

namespace FilesSortingSystem.Services
{
    public class DialogService(IApplicationProvider applicationProvider) : IDialogService
    {
        public Task DisplayAlertAsync(string title, string message, string cancel)
        {
            var currentPage = applicationProvider.CurrentApplication?.Windows?[0].Page;

            if (currentPage != null)
            {
                return currentPage.DisplayAlert(title, message, cancel);
            }

            return Task.CompletedTask;
        }
    }
}