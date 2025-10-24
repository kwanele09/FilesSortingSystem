using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.ViewModels
{
    public partial class MenuBarViewModel : ObservableObject
    {
        private readonly INavigate _navigate;

        public MenuBarViewModel(INavigate navigate)
        {
            _navigate = navigate;
        }

        [RelayCommand]
        public async Task OpenSettingsAsync()
        {
            await _navigate.NavigateTo("settings");
        }

        [RelayCommand]
        public async Task ViewLogsAsync()
        {
            await _navigate.NavigateTo("logs");
        }

        [RelayCommand]
        public async Task OpenRulesAsync()
        {
            await _navigate.NavigateTo("rules");
        }

    }
}
