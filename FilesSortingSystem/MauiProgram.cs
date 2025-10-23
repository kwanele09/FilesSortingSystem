using CommunityToolkit.Maui;
using FilesSortingSystem.Core.Guards;
using FilesSortingSystem.Core.Interactors;
using FilesSortingSystem.Core.Interfaces;
using FilesSortingSystem.Interfaces;
using FilesSortingSystem.Providers;
using FilesSortingSystem.Services;
using FilesSortingSystem.Storage.Interfaces;
using FilesSortingSystem.Storage.Mappers;
using FilesSortingSystem.Storage.Provider;
using FilesSortingSystem.Storage.Storage;
using FilesSortingSystem.ViewModels;
using FilesSortingSystem.Views;
using Microsoft.Extensions.Logging;
using ILogger = FilesSortingSystem.Core.Interfaces.ILogger;
using Settings = FilesSortingSystem.Services.Settings;

namespace FilesSortingSystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
           

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IFileSorter, FileSorter>();
            builder.Services.AddSingleton<ILogger, Logger>();
            builder.Services.AddSingleton<IRulesEngine, RulesEngine>();
            builder.Services.AddSingleton<ISettings, Settings>();
            builder.Services.AddSingleton<IUtils, Utils>();
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<INavigate, Navigator>();
            builder.Services.AddSingleton<DatabaseConnectionProvider>();
            builder.Services.AddSingleton<IApplicationProvider, ApplicationProvider>();
            builder.Services.AddSingleton<ICategoryPathResolver, CategoryPathResolver>();
            builder.Services.AddSingleton<ISubCategoryResolver, DocumentSubCategoryResolver>();
            builder.Services.AddSingleton<ILogStorage, LogStorage>();
            builder.Services.AddSingleton<IAddLogsEntryInteractor, AddLogsEntryInteractor>();
            builder.Services.AddSingleton<ILogMapper, LogMapper>();
            builder.Services.AddSingleton<IGetLogsEntryInteractor, GetLogsEntryInteractor>();
            builder.Services.AddSingleton<IClearLogsInteractor, ClearLogsInteractor>();


            // Interactors
            builder.Services.AddSingleton<IAddRuleInteractor, AddRuleInteractor>();
            builder.Services.AddSingleton<IGetRulesInteractor, GetRulesInteractor>();
            builder.Services.AddSingleton<IFileSortRuleStorage, FileSortRuleStorage>();
            builder.Services.AddSingleton<IFileSortRuleMapper, FileSortRuleMapper>();
            builder.Services.AddSingleton<IGuardExtensions, GuardExtensions>();


            builder.Services.AddTransient<SortingPageViewModel>();
            builder.Services.AddTransient<LogsViewModel>();
            builder.Services.AddTransient<SortingRulesViewModel>();
            builder.Services.AddTransient<SetRuleViewModel>();


            builder.Services.AddTransient<SortingPage>();
            builder.Services.AddTransient<LogsPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<SortingRulesPage>();


            return builder.Build();
        }
    }
}