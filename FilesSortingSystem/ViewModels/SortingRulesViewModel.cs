using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FilesSortingSystem.ViewModels
{
    public partial class SortingRulesViewModel(
        IAddRuleInteractor addRuleInteractor,
        IGetRulesInteractor getRulesInteractor,
        INavigate navigate) : ObservableObject
    {
        [ObservableProperty] private ObservableCollection<FileSortRule> rules = new();

        [RelayCommand]
        private async Task AddRuleAsync()
        {
            await navigate.NavigateTo("addRule");
        }

        [RelayCommand]
        private async Task DoneAsync()
        {
            await navigate.PopModal();
        }

        public async Task LoadAllRulesAsync()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "FilesSortingSystem.Configurations.DefaultFileSortRules.json";

                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                {
                    Debug.WriteLine($"[SortingRulesViewModel] Embedded resource not found: {resourceName}");
                    return;
                }

                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                var defaultRules = JsonSerializer.Deserialize<List<FileSortRule>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<FileSortRule>();

                var userRules = await getRulesInteractor.Handle() ?? new List<FileSortRule>();

                // Merge, remove duplicates
                var combinedRules = defaultRules
                    .Concat(userRules)
                    .GroupBy(r => (r.Extension.ToLowerInvariant(), r.Category.ToLowerInvariant()))
                    .Select(g => g.First())
                    .ToList();

                Rules = new ObservableCollection<FileSortRule>(combinedRules);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[SortingRulesViewModel] Error loading rules: {ex}");
            }
        }
    }
}
