using FilesSortingSystem.ViewModels;

namespace FilesSortingSystem.Views;

public partial class SortingRulesPage : ContentPage
{
	public SortingRulesPage(SortingRulesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        this.Loaded += (s, e) =>
        {
            MainBorder.WidthRequest = this.Width * 0.8;
            MainBorder.HeightRequest = this.Height * 0.9;
        };

        this.SizeChanged += (s, e) =>
        {
            MainBorder.WidthRequest = this.Width * 0.8;
            MainBorder.HeightRequest = this.Height * 0.9;
        };
        this.Loaded += (s, e) =>
        {
            RulesBorder.WidthRequest = this.Width * 0.77;
            RulesBorder.HeightRequest = this.Height * 0.3;
        };

        this.SizeChanged += (s, e) =>
        {
            RulesBorder.WidthRequest = this.Width * 0.77;
            RulesBorder.HeightRequest = this.Height * 0.4;
        };
    }
}