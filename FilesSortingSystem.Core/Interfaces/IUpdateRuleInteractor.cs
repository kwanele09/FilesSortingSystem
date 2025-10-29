using FilesSortingSystem.Core.DomainEntities;

namespace FilesSortingSystem.Core.Interfaces
{
    public interface IUpdateRuleInteractor
    {
        Task Handle(FileSortRule fileSortRule);
    }
}