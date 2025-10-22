using Ardalis.GuardClauses;
using FilesSortingSystem.Core.InputObjects;
using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.DomainEntities;
using FilesSortingSystem.Storage.Storage;

namespace FilesSortingSystem.Core.Interactors
{
    public class AddLogsEntryInteractor(ILogStorage logStorage) : IAddLogsEntryInteractor
    {
        public async Task<List<LogEntry>> Handle(List<LogEntryInput> inputs)
        {
            Guard.Against.Null(inputs, nameof(inputs));

            var logs = CreateLogEntries(inputs);
            var storedLogs = await logStorage.AddLogEntriesAsync(logs);

            return storedLogs;
        }

        private static List<LogEntry> CreateLogEntries(List<LogEntryInput> inputs)
        {
            var logs = new List<LogEntry>();

            foreach (var input in inputs)
            {
                logs.Add(new LogEntry
                {
                    Message = input.Message,
                    IsMoved = input.IsMoved,
                    MoveDateTime = input.MoveDateTime
                });
            }

            return logs;
        }
    }
}