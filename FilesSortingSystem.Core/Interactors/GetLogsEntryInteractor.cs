using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.DomainEntities;
using FilesSortingSystem.Storage.Storage;

namespace FilesSortingSystem.Core.Interactors
{
    public class GetLogsEntryInteractor(ILogStorage logStorage) : IGetLogsEntryInteractor
    {
        public async Task<List<LogEntry>> Handle()
        {
            return await logStorage.GetAllLogEntriesAsync();
        }
    }
}