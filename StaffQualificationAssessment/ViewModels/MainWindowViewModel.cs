using Avalonia.Controls;
using ReactiveUI;
using StaffQualificationAssessment.Views;
using System.Threading.Tasks;

namespace StaffQualificationAssessment.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _currentView;
        public UserControl CurrentView { get => _currentView; set => this.RaiseAndSetIfChanged(ref _currentView, value); }
        public MainWindowViewModel()
        {
            _errorMessage = "";
            _currentView = new AuthView(this);
        }
        private string _errorMessage;
        public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
        public async Task SetError(string errorMessage, int seconds=3)
        {
            ErrorMessage = errorMessage;
            await Task.Delay(1000 * seconds);
            ErrorMessage = string.Empty;
        }

        public void ClearError() => ErrorMessage = string.Empty;
        public void GoToAuthView() => CurrentView = new AuthView(this);
    }
}
