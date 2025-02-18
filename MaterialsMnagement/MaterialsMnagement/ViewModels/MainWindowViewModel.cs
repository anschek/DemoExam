using Avalonia.Controls;
using MaterialsMnagement.Views;
using ReactiveUI;

namespace MaterialsMnagement.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _currentView;
        public UserControl CurrentView { get => _currentView; set => this.RaiseAndSetIfChanged(ref _currentView, value); }
        public MainWindowViewModel()
        {
            CurrentView = new MaterialsListView(this);
        }
    }
}
