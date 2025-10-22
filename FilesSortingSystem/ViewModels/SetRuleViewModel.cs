using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.InputObjects;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.ViewModels
{
    public partial class SetRuleViewModel(IAddRuleInteractor addRuleInteractor, IDialogService dialogService, INavigate navigate) : ObservableObject
    {
        [ObservableProperty] private string extension = string.Empty;
        [ObservableProperty] private string category = string.Empty;
        [ObservableProperty] private bool isCaseSensitive = true;


        [RelayCommand]
        private async Task AddRuleAsync()
        {
            await GuardIsNotNull();
            var input = CreateFileSortRuleInput();
            var addedRule = await addRuleInteractor.Handle(input);
            await SuccessAlert(addedRule);
        }

        private async Task GuardIsNotNull()
        {
            if (string.IsNullOrWhiteSpace(Extension) || string.IsNullOrWhiteSpace(Category))
            {
                await dialogService.DisplayAlertAsync("Error", "Please fill in all the fields", "OK");
                return;
            }
        }

        private FileSortRuleInput CreateFileSortRuleInput()
        {
            return new FileSortRuleInput
            {
                Extension = Extension,
                Category = Category,
                IsCaseSensitive = IsCaseSensitive
            };
        }

        [RelayCommand]
        private async Task CloseAsync()
        {
            await navigate.PopModal();
        }

        private async Task SuccessAlert(FileSortRule rule)
        {
            if (rule != null)
            {
                await dialogService.DisplayAlertAsync("Success", "Rule added successful.", "OK");
            }
        }
    }
}
