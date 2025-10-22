using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.Services;

namespace FilesSortingSystem.ViewModels
{
    public partial class SortingPageViewModel(IFileSorter fileSorter, IDialogService dialogService, INavigate navigation, IGetRulesInteractor getRulesInteractor) : ViewModel(navigation)
    {
        private string _selectedFolderPath = string.Empty;
        public string SelectedFolderPath
        {
            get => _selectedFolderPath;
            set => SetProperty(ref _selectedFolderPath, value);
        }

        [RelayCommand]
        private async Task OpenSettingsAsync()
        {
            await Navigation.NavigateTo("settings");
        }

        [RelayCommand]
        private async Task ViewLogsAsync()
        {
            await Navigation.NavigateTo("logs");
        }

        [RelayCommand]
        private async Task OpenRulesAsync()
        {
            await Navigation.NavigateTo("rules");
        }

        [RelayCommand]
        private async Task PickFolderAsync()
        {
            var result = await FolderPicker.Default.PickAsync();
            if (result.IsSuccessful)
            {
                SelectedFolderPath = result.Folder.Path;
            }
        }

        [RelayCommand]
        private async Task SortFilesAsync()
        {
            if (string.IsNullOrEmpty(SelectedFolderPath))
            {
                await dialogService.DisplayAlertAsync("Error", "Please select a folder", "OK");
                return;
            }

            try
            {
                await fileSorter.SortAsync(SelectedFolderPath);
                await dialogService.DisplayAlertAsync("Success", "Sorting completed", "OK");
            }
            catch (Exception ex)
            {
                await dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        public async Task<List<FileSortRule>> GetSortingRulesAsync()
        {
            return await getRulesInteractor.Handle();
        }
    }
}