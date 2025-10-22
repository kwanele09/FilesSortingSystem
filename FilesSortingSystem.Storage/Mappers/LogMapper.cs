using FilesSortingSystem.DomainEntities;
using FilesSortingSystem.Storage.DatabaseEntities;
using FilesSortingSystem.Storage.Interfaces;

namespace FilesSortingSystem.Storage.Mappers
{
    public class LogMapper : ILogMapper
    {
        public LogEntry MapToDomainEntity(LogEntryEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return
                new LogEntry
                {
                    Id = entity.Id,
                    IsMoved = entity.IsMoved,
                    Message = entity.Message,
                    MoveDateTime = entity.MoveDateTime
                };
        }

        public LogEntryEntity MapToDatabaseEntity(LogEntry domain)
        {
            ArgumentNullException.ThrowIfNull(domain);

            return
                new LogEntryEntity
                {
                    Id = domain.Id,
                    IsMoved = domain.IsMoved,
                    Message = domain.Message,
                    MoveDateTime = domain.MoveDateTime
                };
        }
    }
}
