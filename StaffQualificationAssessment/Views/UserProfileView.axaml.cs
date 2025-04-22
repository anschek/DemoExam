using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StaffQualificationAssessment.ViewModels;

namespace StaffQualificationAssessment.Views;

public partial class UserProfileView : UserControl
{
    public UserProfileView(MainWindowViewModel mainVM, string userCode)
    {
        InitializeComponent();
        DataContext = new UserProfileViewModel(mainVM, userCode);
    }
}