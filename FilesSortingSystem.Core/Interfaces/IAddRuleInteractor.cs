using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.InputObjects;

namespace FilesSortingSystem.Core.Interfaces;

public interface IAddRuleInteractor
{
    Task<FileSortRule> Handle(FileSortRuleInput rule);
}