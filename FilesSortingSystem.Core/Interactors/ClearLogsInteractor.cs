using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.Storage.Storage;

namespace FilesSortingSystem.Core.Interactors
{
    public class ClearLogsInteractor(ILogStorage logStorage) : IClearLogsInteractor
    {
        public async Task Handle()
        {
            await logStorage.DeleteAllLogEntriesAsync();
        }
    }
}
