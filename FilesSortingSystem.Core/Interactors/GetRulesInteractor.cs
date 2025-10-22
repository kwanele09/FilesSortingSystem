using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.Core.Interactors
{
    public class GetRulesInteractor(IFileSortRuleStorage fileSortRuleStorage) : IGetRulesInteractor
    {
        public async Task<List<FileSortRule>> Handle()
        {
            return await fileSortRuleStorage.GetAllRulesAsync();
        }
    }
}
