using FilesSortingSystem.ViewModels;

namespace FilesSortingSystem.Views;

public partial class SortingPage : ContentPage
{
	public SortingPage(SortingPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}