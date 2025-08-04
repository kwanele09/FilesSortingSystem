using FilesSortingSystem.ViewModels;

namespace FilesSortingSystem.Views;

public partial class LogsPage : ContentPage
{
	private readonly LogsViewModel _viewModel;
	public LogsPage(LogsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadLogsAsync();

    }
}