using FilesSortingSystem.ViewModels;

namespace FilesSortingSystem.Views;

public partial class SortingRulesPage : ContentPage
{
    private readonly SortingRulesViewModel _viewModel;

    public SortingRulesPage(SortingRulesViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;

        Loaded += async (s, e) =>
        {
            MainBorder.WidthRequest = Width * 0.8;
            MainBorder.HeightRequest = Height * 0.9;

            RulesBorder.WidthRequest = Width * 0.77;
            RulesBorder.HeightRequest = Height * 0.3;


            await _viewModel.LoadAllRulesAsync();
        };

        SizeChanged += (s, e) =>
        {
            MainBorder.WidthRequest = Width * 0.8;
            MainBorder.HeightRequest = Height * 0.9;

            RulesBorder.WidthRequest = Width * 0.77;
            RulesBorder.HeightRequest = Height * 0.4;
        };
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAllRulesAsync();
    }
}