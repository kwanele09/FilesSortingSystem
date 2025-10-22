using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.Interfaces;
using System.Text.Json;

namespace FilesSortingSystem.Storage.Seeders
{
    public class FileSortRuleSeeder
    {
        private readonly IFileSortRuleStorage _storage;

        public FileSortRuleSeeder(IFileSortRuleStorage storage)
        {
            _storage = storage;
        }

        public async Task SeedAsync()
        {
            var existingRules = await _storage.GetAllRulesAsync();
            if (existingRules.Any())
                return; 

            var json = await File.ReadAllTextAsync("DefaultFileSortRules.json");

            var rules = JsonSerializer.Deserialize<List<FileSortRule>>(json);

            if (rules == null || !rules.Any())
                return;

            foreach (var rule in rules)
            {
                if (!_storage.Exists(rule.Extension, rule.Category))
                    await _storage.AddRuleAsync(rule);
            }
        }
    }
}