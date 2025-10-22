using FilesSortingSystem.Core.Interfaces;
using System.Text.Json;

namespace FilesSortingSystem.Core.Interactors
{
    public class FileSortRulesLoader : IFileSortRulesLoader
    {
        /// <summary>
        /// Loads and deserializes file sorting rules from a JSON file.
        /// </summary>
        /// <param name="jsonFilePath">Path to the JSON file.</param>
        /// <param name="caseSensitive">
        /// Determines whether dictionary keys should be case-sensitive.
        /// Default is <c>false</c> (case-insensitive).
        /// </param>
        public async Task<Dictionary<string, string>> Handle(string jsonFilePath, bool caseSensitive = false)
        {
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("The specified JSON file was not found.", jsonFilePath);
            }

            try
            {
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var tempRules = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

                var comparer = caseSensitive
                    ? StringComparer.Ordinal     
                    : StringComparer.OrdinalIgnoreCase; 

                var rules = new Dictionary<string, string>(tempRules ?? new(), comparer);

                return rules;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse the JSON file. Ensure it is valid JSON.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while loading the JSON file.", ex);
            }
        }
    }
}