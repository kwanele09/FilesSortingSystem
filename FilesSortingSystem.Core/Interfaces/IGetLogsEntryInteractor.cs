using FilesSortingSystem.DomainEntities;

namespace FilesSortingSystem.Core.Interfaces;

public interface IGetLogsEntryInteractor
{
    Task<List<LogEntry>> Handle();
}