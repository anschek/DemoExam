using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StaffQualificationAssessment.ViewModels;

namespace StaffQualificationAssessment.Views;

public partial class RegistrationView : UserControl
{
    public RegistrationView(MainWindowViewModel mainVm)
    {
        InitializeComponent();
        DataContext = new RegistrationViewModel(mainVm);
    }
}