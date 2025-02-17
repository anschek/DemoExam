using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PartnerInteraction.Models;
using PartnerInteraction.ViewModels;

namespace PartnerIntercation.Views;

public partial class PartnerInfoView : UserControl
{
    public PartnerInfoView(MainWindowViewModel mainVM, DemoContext db, int? partnerId=null)
    {
        InitializeComponent();
        DataContext = new PartnerInfoViewModel(mainVM, db, partnerId);
    }
}