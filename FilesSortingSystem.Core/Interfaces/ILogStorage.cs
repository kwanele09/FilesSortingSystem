using FilesSortingSystem.DomainEntities;

namespace FilesSortingSystem.Storage.Storage;

public interface ILogStorage
{
    Task<List<LogEntry>> AddLogEntriesAsync(IEnumerable<LogEntry> logs);
    Task<List<LogEntry>> GetAllLogEntriesAsync();
}