using FilesSortingSystem.Core.InputObjects;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.Core.Guards
{ public class GuardExtensions(IFileSortRuleStorage fileSortRuleStorage, IDialogService dialogService) : IGuardExtensions
    {
        public async Task GuardRuleExists(FileSortRuleInput fileSortRuleInput)
        {
            if (fileSortRuleStorage.Exists(fileSortRuleInput.Extension, fileSortRuleInput.Extension))
            {
                await dialogService.DisplayAlertAsync("Error", "Rule already exists. You can not create duplicated rules",
                    "OK");
            }
        }
    }
}
