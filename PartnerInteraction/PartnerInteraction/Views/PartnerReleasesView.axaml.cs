using Avalonia.Controls;
using PartnerInteraction.Models;
using PartnerInteraction.ViewModels;

namespace PartnerInteraction.Views;

public partial class PartnerReleasesView : UserControl
{
    public PartnerReleasesView(MainWindowViewModel mainVM, DemoContext db, int partnerId)
    {
        InitializeComponent();
        DataContext = new PartnerReleasesViewModel(mainVM, db, partnerId);
    }
}