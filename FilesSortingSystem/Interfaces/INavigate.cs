namespace FilesSortingSystem.Core.Interfaces
{
    public interface INavigate
    {
        Task NavigateTo(string route);
        Task NavigateTo(string route, IDictionary<string, object>? parameters = null);
        Task PushModal(Page page);
        Task PopModal();
    }
}