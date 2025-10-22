using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.Storage.DatabaseEntities;
using FilesSortingSystem.Storage.Interfaces;
using FilesSortingSystem.Storage.Provider;
using System.Data;
using System.Diagnostics;

namespace FilesSortingSystem.Storage.Storage
{
    public class FileSortRuleStorage(DatabaseConnectionProvider dbProvider, IFileSortRuleMapper fileSortRuleMapper)
        : IFileSortRuleStorage
    {
        public async Task<FileSortRule> AddRuleAsync(FileSortRule rule)
        {
            var conn = await dbProvider.GetConnectionAsync();
            await conn.CreateTableAsync<FileSortRuleEntity>();
            var ruleEntity = fileSortRuleMapper.MapToDatabaseEntity(rule);
            await conn.InsertAsync(ruleEntity);
            rule.Id = ruleEntity.Id;
            return fileSortRuleMapper.MapToDomainEntity(ruleEntity);
        }

        public async Task<List<FileSortRule>> GetAllRulesAsync()
        {
            var conn = await dbProvider.GetConnectionAsync();
            await conn.CreateTableAsync<FileSortRuleEntity>();
            var ruleEntities = await conn.Table<FileSortRuleEntity>().ToListAsync();
            return ruleEntities.Select(fileSortRuleMapper.MapToDomainEntity).ToList();
        }

        public async Task DeleteRuleAsync(int ruleId)
        {
            try
            {
                var conn = await dbProvider.GetConnectionAsync();
                if (conn == null)
                    throw new InvalidOperationException("Database connection could not be established.");

                var existingRule = await conn.Table<FileSortRuleEntity>()
                    .Where(r => r.Id == ruleId)
                    .FirstOrDefaultAsync();

                if (existingRule != null)
                {
                    await conn.DeleteAsync(existingRule);
                    Console.WriteLine($"Rule with ID {ruleId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Rule with ID {ruleId} not found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting rule {ruleId}: {ex.Message}");
                throw;
            }
        }

        public async Task<FileSortRule> UpdateRuleAsync(FileSortRule rule)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            try
            {
                var conn = await dbProvider.GetConnectionAsync();
                if (conn == null)
                    throw new InvalidOperationException("Database connection could not be established.");

                var existingRule = await conn.Table<FileSortRuleEntity>()
                    .Where(r => r.Id == rule.Id)
                    .FirstOrDefaultAsync();

                if (existingRule != null)
                {
                    var updatedEntity = fileSortRuleMapper.MapToDatabaseEntity(rule);
                    updatedEntity.Id = existingRule.Id;

                    await conn.UpdateAsync(updatedEntity);

                    var updatedRule =  fileSortRuleMapper.MapToDomainEntity(updatedEntity);

                    Debug.WriteLine($"Rule with ID {rule.Id} updated successfully.");
                    return updatedRule;
                }
                else
                {
                    Console.WriteLine($"Rule with ID {rule.Id} not found. Cannot update.");
                    return null; 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating rule {rule.Id}: {ex.Message}");
                throw;
            }
        }

    }
}