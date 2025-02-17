using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PartnerInteraction.ViewModels;

namespace PartnerInteraction.Views;

public partial class PartnersListView : UserControl
{
    public PartnersListView(MainWindowViewModel mainVM)
    {
        InitializeComponent();
        DataContext = new PartnersListViewModel(mainVM);
    }
}