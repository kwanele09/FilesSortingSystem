namespace FilesSortingSystem.Services.Interfaces
{
    public interface IDialogService
    {
        Task DisplayAlertAsync(string title, string message, string cancel);
    }
}
