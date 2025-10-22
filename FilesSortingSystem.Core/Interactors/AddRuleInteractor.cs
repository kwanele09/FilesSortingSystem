using Ardalis.GuardClauses;
using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.InputObjects;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.Core.Interactors
{
    public class AddRuleInteractor(IFileSortRuleStorage fileSortRuleStorage, IGuardExtensions guardExtensions) : IAddRuleInteractor
    {
        public async Task<FileSortRule> Handle(FileSortRuleInput rule)
        {
            Guard.Against.Null(rule);
            await guardExtensions.GuardRuleExists(rule);
            var fileSortRule = CreateFileSortRule(rule);
            var storedRule = fileSortRuleStorage.AddRuleAsync(fileSortRule);
            return await storedRule;
        }

        private static FileSortRule CreateFileSortRule(FileSortRuleInput fileSortRuleInput)
        {
            return new FileSortRule
            {
                Category = fileSortRuleInput.Category,
                Extension = fileSortRuleInput.Extension,
                IsCaseSensitive = fileSortRuleInput.IsCaseSensitive
            };
        }
    }
}