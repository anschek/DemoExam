using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StaffQualificationAssessment.ViewModels;

namespace StaffQualificationAssessment.Views;

public partial class AdminAuthView : UserControl
{
    public AdminAuthView(MainWindowViewModel mainVm)
    {
        InitializeComponent();
        DataContext = new AdminAuthViewModel(mainVm);
    }
}