using FilesSortingSystem.DomainEntities;
using FilesSortingSystem.Storage.DatabaseEntities;

namespace FilesSortingSystem.Storage.Interfaces
{
    public interface ILogMapper
    {
        LogEntry MapToDomainEntity(LogEntryEntity entity);
        LogEntryEntity MapToDatabaseEntity(LogEntry domain);
    }
}
