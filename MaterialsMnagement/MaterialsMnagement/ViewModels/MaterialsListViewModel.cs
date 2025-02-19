using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using MaterialsMnagement.Models;
using MaterialsMnagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace MaterialsMnagement.ViewModels
{
    public class MaterialsListViewModel : ViewModelBase
    {
        ViewModelBase _mainVm;
        DemoContext _db;
        public MaterialsListViewModel(ViewModelBase mainVm)
        {
            _mainVm = mainVm;
            _db = new DemoContext();
            UpdateMaterials();
            ChangeMinimalAmountCommand = ReactiveCommand.Create(OnChangeMinimalAmount);
        }

        // исходные материалы из бд (чтобы данные не тер€лись при фильтрации и поиске)
        private IEnumerable<Material> _originalMaterials => _db.Materials
            .Include(material => material.TypeNavigation);

        // переменные материалы между отображаемым свойством и данными из бд
        private List<MaterialListItem> _materials;

        // свойство (через него _materials обновл€етс€ при изменени€х)
        public List<MaterialListItem> Materials { get => _materials; private set => this.RaiseAndSetIfChanged(ref _materials, value); }

        // обновление материалов после некоторых действий
        private void UpdateMaterials()
        {
            var sortedMaterials = SelectedSortingType
            switch
            {
                "название" => _originalMaterials.OrderBy(m => m.Name),
                "остаток" => _originalMaterials.OrderBy(m => m.Amount),
                "стоимость" => _originalMaterials.OrderBy(m => m.Cost),
                _ => _originalMaterials
            };

            if(SelectedSortingAsc!=_asc) sortedMaterials = sortedMaterials.Reverse();

            Materials = sortedMaterials
                .Where(isMaterialWithMatchingType)
                .Where(MaterialIsMatchesSearchQuery)
                .Select(MapToMaterialListItem)
                .ToList();
        }
        // конвертаци€ модели бд в dto
        private MaterialListItem MapToMaterialListItem(Material material)
        {
            return new MaterialListItem
            {
                Name = material.Name,
                TypeName = material.TypeNavigation?.Name ?? "Ѕез типа",
                Amount = material.Amount,
                MinimalAmount = material.MinimalAmount,
                Cost = material.Cost,
                Description = material.Description ?? "*без описани€*",
                Image = material.Image == null
                    ? new Bitmap("empty_image.jpg")
                    : new Bitmap(new MemoryStream(material.Image))
            };
        }
        // фильтраци€ по типц
        private static MaterialType _allTypesObject = new MaterialType { Id = 0, Name = "¬се типы" };
        private MaterialType _selectedType = _allTypesObject;
        public MaterialType SelectedType
        {
            get => _selectedType;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedType, value);
                UpdateMaterials();
            }
        }
        private List<MaterialType> _materialType => _db.MaterialTypes.ToList();
        public List<MaterialType> MaterialTypes
        {
            get
            {
                var list = _materialType.ToList();
                list.Add(_allTypesObject);
                return list;
            }
        }
        private bool isMaterialWithMatchingType(Material material)
        {
            // ¬се типы
            if (_selectedType.Id == 0) return true;
            // ѕодход€щий тип
            return _selectedType.Id == material.TypeNavigation.Id;
        }

        // поиск
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                this.RaiseAndSetIfChanged(ref _searchQuery, value);
                UpdateMaterials();
            }
        }
        private bool MaterialIsMatchesSearchQuery(Material material)
        {
            // —трока пуста (пользовать ничего конкретного не ищет)
            if (string.IsNullOrWhiteSpace(_searchQuery)) return true;
            // Ќазвание или описание материала содержит поисковый запрос
            return (material.Name + material.Description).Contains(_searchQuery);
        }

        // сортировка
        private static string _asc = "ѕо возрастанию";
        //private List<string> _sortingAsc = [ _asc, "ѕо убыванию" ];
        public List<string> SortingAsc => [_asc, "ѕо убыванию"];

        private string _selectedSortingAsc = _asc;
        public string SelectedSortingAsc
        {
            get => _selectedSortingAsc;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSortingAsc, value);
                UpdateMaterials();
            }
        }

        public List<string> SortingTypes => [" - ", "название", "остаток", "стоимость"];
        private string _selectedSortingtype = " - ";
        public string SelectedSortingType
        {
            get => _selectedSortingtype;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSortingtype, value);
                UpdateMaterials();
            }
        }

        // изменение минимального количества
        private readonly ObservableCollection<MaterialListItem> _selectedMaterials = new();
        public ObservableCollection<MaterialListItem> SelectedMaterials => _selectedMaterials;

        private bool _isButtonVisible = false;
        public bool IsButtonVisible {get => _isButtonVisible; set => this.RaiseAndSetIfChanged(ref _isButtonVisible, value); }

        public ICommand ChangeMinimalAmountCommand { get; }
        private void OnChangeMinimalAmount()
        {
            // Ћогика открыти€ модального окна
        }
    }
}