using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MaterialsMnagement.ViewModels;

namespace MaterialsMnagement.Views;

public partial class MaterialsListView : UserControl
{
    public MaterialsListView(ViewModelBase mainVm)
    {
        InitializeComponent();
        DataContext = new MaterialsListViewModel(mainVm);
    }
}