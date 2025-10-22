using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.ViewModels
{
    public partial class SortingPageViewModel : ViewModel
    {
        private readonly IFileSorter _fileSorter;
        private readonly IDialogService _dialogService;

        private string _selectedFolderPath = string.Empty;
        public string SelectedFolderPath
        {
            get => _selectedFolderPath;
            set => SetProperty(ref _selectedFolderPath, value);
        }

        public SortingPageViewModel(IFileSorter fileSorter, IDialogService dialogService, INavigate navigation) : base(navigation)
        {
            _fileSorter = fileSorter;
            _dialogService = dialogService;
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
                await _dialogService.DisplayAlertAsync("Error", "Please select a folder to sort.", "OK");
                return;
            }

            //_fileSorter.SetRules(FileSortRules.DefaultRules);

            try
            {
                _fileSorter.Sort(SelectedFolderPath);
                await _dialogService.DisplayAlertAsync("Success", "Sorting complete!", "OK");
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
    }
}