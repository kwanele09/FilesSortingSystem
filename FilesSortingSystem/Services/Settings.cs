using FilesSortingSystem.Services.Interfaces;
using System.Text.Json;

namespace FilesSortingSystem.Services
{
    public class Settings : ISettings
    {
        public Dictionary<string, string> LoadRules(string path)
        {
            if (!File.Exists(path))
                return new Dictionary<string, string>();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                   ?? new Dictionary<string, string>();
        }

        public void SaveRules(string path, Dictionary<string, string> rules)
        {
            var json = JsonSerializer.Serialize(rules, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}