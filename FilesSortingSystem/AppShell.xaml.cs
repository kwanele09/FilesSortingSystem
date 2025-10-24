using FilesSortingSystem.Views;

namespace FilesSortingSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("settings", typeof(SettingsPage));
            Routing.RegisterRoute("logs", typeof(LogsPage));
            Routing.RegisterRoute("addRule", typeof(SetRulePage));
            Routing.RegisterRoute("rules", typeof(SortingRulesPage));
            Routing.RegisterRoute("help", typeof(HelpPage));
        }
    }
}
