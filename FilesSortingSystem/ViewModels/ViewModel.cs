using CommunityToolkit.Mvvm.ComponentModel;
using FilesSortingSystem.Core.Interfaces;

namespace FilesSortingSystem.ViewModels
{
    public abstract partial class ViewModel : ObservableObject
    {
        public INavigate Navigation { get; init; }
        internal ViewModel(INavigate navigation) => Navigation = navigation;
    }
}
