using FilesSortingSystem.Core.InputObjects;
using FilesSortingSystem.DomainEntities;

namespace FilesSortingSystem.Core.Interfaces;

public interface IAddLogsEntryInteractor
{
    Task<List<LogEntry>> Handle(List<LogEntryInput> inputs);
}