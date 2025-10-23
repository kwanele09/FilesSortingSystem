using FilesSortingSystem.DomainEntities;
using FilesSortingSystem.Storage.DatabaseEntities;
using FilesSortingSystem.Storage.Interfaces;
using FilesSortingSystem.Storage.Provider;

namespace FilesSortingSystem.Storage.Storage
{
    public class LogStorage(DatabaseConnectionProvider dbProvider, ILogMapper logMapper) : ILogStorage
    {
        public async Task<List<LogEntry>> AddLogEntriesAsync(IEnumerable<LogEntry> logs)
        {
            var conn = await dbProvider.GetConnectionAsync();
            await conn.CreateTableAsync<LogEntryEntity>();
            var logEntities = logs.Select(logMapper.MapToDatabaseEntity).ToList();
            await conn.InsertAllAsync(logEntities);
            for (int i = 0; i < logEntities.Count; i++)
            {
                logs.ElementAt(i).Id = logEntities[i].Id;
            }

            var allLogEntities = await conn.Table<LogEntryEntity>().ToListAsync();
            var allLogs = allLogEntities.Select(logMapper.MapToDomainEntity).ToList();

            return allLogs;
        }

        public async Task<List<LogEntry>> GetAllLogEntriesAsync()
        {
            var conn = await dbProvider.GetConnectionAsync();
            await conn.CreateTableAsync<LogEntryEntity>();
            var logEntryEntities = await conn.Table<LogEntryEntity>().ToListAsync();
            return logEntryEntities.Select(logMapper.MapToDomainEntity).ToList();
        }

        public async Task DeleteAllLogEntriesAsync()
        {
            var conn = await dbProvider.GetConnectionAsync();
            await conn.DeleteAllAsync<LogEntryEntity>();
        }
    }
}
