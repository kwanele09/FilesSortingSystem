using FilesSortingSystem.Core.DomainEntities;

namespace FilesSortingSystem.Core.Interfaces
{
    public interface IFileSortRuleStorage
    {
        Task<List<FileSortRule>> GetAllRulesAsync();
        Task<FileSortRule> AddRuleAsync(FileSortRule rule);
        Task<FileSortRule> UpdateRuleAsync(FileSortRule rule);
        Task DeleteRuleAsync(int ruleId);
        bool Exists(string extension, string category);
    }
}
