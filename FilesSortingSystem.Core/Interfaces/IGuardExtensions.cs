using FilesSortingSystem.Core.InputObjects;

namespace FilesSortingSystem.Core.Interfaces;

public interface IGuardExtensions
{
    Task GuardRuleExists(FileSortRuleInput fileSortRuleInput);
}