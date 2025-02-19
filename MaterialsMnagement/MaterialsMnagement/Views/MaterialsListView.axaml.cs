using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MaterialsMnagement.Models.DTOs;
using MaterialsMnagement.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace MaterialsMnagement.Views;

public partial class MaterialsListView : UserControl
{
    public MaterialsListView(ViewModelBase mainVm)
    {
        InitializeComponent();
        DataContext = new MaterialsListViewModel(mainVm);
    }
    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MaterialsListViewModel viewModel)
        {
            foreach (var removed in e.RemovedItems.Cast<MaterialListItem>())
            {
                viewModel.SelectedMaterials.Remove(removed);
            }
            foreach (var added in e.AddedItems.Cast<MaterialListItem>())
            {
                viewModel.SelectedMaterials.Add(added);
            }

            viewModel.IsButtonVisible = viewModel.SelectedMaterials.Count > 0;
        }
    }
}
