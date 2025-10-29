using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Storage.DatabaseEntities;
using FilesSortingSystem.Storage.Interfaces;

namespace FilesSortingSystem.Storage.Mappers
{
    public class FileSortRuleMapper : IFileSortRuleMapper
    {
        public FileSortRule MapToDomainEntity(FileSortRuleEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return
                new FileSortRule
                {
                    Id = entity.Id,
                    Extension = entity.Extension,
                    Category = entity.Category,
                    IsCaseSensitive = entity.IsCaseSensitive,
                    IsUserDefined = entity.IsUserDefined,
                };
        }

        public FileSortRuleEntity MapToDatabaseEntity(FileSortRule domain)
        {
            ArgumentNullException.ThrowIfNull(domain);

            return 
                new FileSortRuleEntity
                {
                    Id = domain.Id,
                    Extension = domain.Extension,
                    Category = domain.Category,
                    IsCaseSensitive = domain.IsCaseSensitive,
                    IsUserDefined = domain.IsUserDefined,
                };
        }
    }
}
