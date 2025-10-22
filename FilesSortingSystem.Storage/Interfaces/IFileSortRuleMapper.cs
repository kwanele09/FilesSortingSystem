using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Storage.DatabaseEntities;

namespace FilesSortingSystem.Storage.Interfaces
{
    public interface IFileSortRuleMapper
    {
        FileSortRule MapToDomainEntity(FileSortRuleEntity entity);
        FileSortRuleEntity MapToDatabaseEntity(FileSortRule domain);
    }
}
    