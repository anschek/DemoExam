using Avalonia.Controls;
using Avalonia.Media;
using PartnerInteraction.Views;
using ReactiveUI;
using System.Threading.Tasks;

namespace PartnerInteraction.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _currentView;
        public UserControl CurrentView { get => _currentView; set => this.RaiseAndSetIfChanged(ref _currentView, value); }

        public MainWindowViewModel()
        {
            CurrentView = new PartnersListView(this);
        }

        private string _mesage;
        private SolidColorBrush _messageColor = new SolidColorBrush(Colors.Black);
        public string Message { get => _mesage; set => this.RaiseAndSetIfChanged(ref _mesage, value); }
        public SolidColorBrush MessageColor { get => _messageColor; set => this.RaiseAndSetIfChanged(ref _messageColor, value); }
        public void SetError(string text)
        {
            MessageColor = new SolidColorBrush(Colors.Red);
            Message = text;
        }
        public async Task SetSuccess(string text)
        {
            MessageColor = new SolidColorBrush(Colors.Black);
            Message = text;
            await Task.Delay(5000);
            Message = "";
        }
    }
}
