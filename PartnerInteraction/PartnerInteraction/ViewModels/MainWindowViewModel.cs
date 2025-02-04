using Avalonia.Controls;
using PartnerInteraction.Views;
using ReactiveUI;

namespace PartnerInteraction.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _currentView;
        public UserControl CurrentView { get => _currentView; set => this.RaiseAndSetIfChanged(ref _currentView, value); }

        public MainWindowViewModel()
        {
            CurrentView = new PartnersListView();
        }
    }
}
