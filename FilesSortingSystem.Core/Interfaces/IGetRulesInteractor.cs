using FilesSortingSystem.Core.DomainEntities;

namespace FilesSortingSystem.Core.Interfaces;

public interface IGetRulesInteractor
{
    Task<List<FileSortRule>> Handle();
}