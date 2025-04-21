using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StaffQualificationAssessment.ViewModels;

namespace StaffQualificationAssessment.Views;

public partial class AuthView : UserControl
{
    public AuthView(MainWindowViewModel mainVm)
    {
        InitializeComponent();
        DataContext = new AuthViewModel(mainVm);
    }
}