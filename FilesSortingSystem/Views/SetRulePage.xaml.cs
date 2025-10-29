using FilesSortingSystem.ViewModels;

namespace FilesSortingSystem.Views;

public partial class SetRulePage : ContentPage
{
	public SetRulePage(SetRuleViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
		this.Loaded += (s, e) =>
		{
			MainBorder.WidthRequest = this.Width * 0.8;
			MainBorder.HeightRequest = this.Height * 0.5;
		};
		this.SizeChanged += (s, e) =>
		{
			MainBorder.WidthRequest = this.Width * 0.8;
			MainBorder.HeightRequest = this.Height * 0.5;
		};
    }
}