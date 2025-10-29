using FilesSortingSystem.ViewModels;

namespace FilesSortingSystem.Views;

public partial class SortingRulesPage : ContentPage
{
    private readonly SortingRulesViewModel _viewModel;

    public SortingRulesPage(SortingRulesViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAllRulesAsync();
    }
}