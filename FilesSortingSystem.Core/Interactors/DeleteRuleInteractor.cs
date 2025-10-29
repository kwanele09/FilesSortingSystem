using Ardalis.GuardClauses;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.Core.Interactors
{
    public class DeleteRuleInteractor(IFileSortRuleStorage fileSortRuleStorage) : IDeleteRuleInteractor
    {
        public async Task Handle(int ruleId)
        {
            Guard.Against.NegativeOrZero(ruleId, nameof(ruleId));
            Guard.Against.Null(ruleId, nameof(ruleId));
            await fileSortRuleStorage.DeleteRuleAsync(ruleId);
        }
    }
}