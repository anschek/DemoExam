using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        }

        // исходные материалы из бд (чтобы данные не терялись при фильтрации и поиске)
        private IEnumerable<Material> _originalMaterials => _db.Materials
            .Include(material => material.TypeNavigation);

        // переменные материалы между отображаемым свойством и данными из бд
        private List<MaterialListItem> _materials;

        // свойство (через него _materials обновляется при изменениях)
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
        // конвертация модели бд в dto
        private MaterialListItem MapToMaterialListItem(Material material)
        {
            return new MaterialListItem
            {
                Name = material.Name,
                TypeName = material.TypeNavigation?.Name ?? "Без типа",
                Amount = material.Amount,
                MinimalAmount = material.MinimalAmount,
                Cost = material.Cost,
                Description = material.Description ?? "*без описания*",
                Image = material.Image == null
                    ? new Bitmap("empty_image.jpg")
                    : new Bitmap(new MemoryStream(material.Image))
            };
        }
        // фильтрация по типц
        private static MaterialType _allTypesObject = new MaterialType { Id = 0, Name = "Все типы" };
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
            // Все типы
            if (_selectedType.Id == 0) return true;
            // Подходящий тип
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
            // Строка пуста (пользовать ничего конкретного не ищет)
            if (string.IsNullOrWhiteSpace(_searchQuery)) return true;
            // Название или описание материала содержит поисковый запрос
            return (material.Name + material.Description).Contains(_searchQuery);
        }

        // сортировка
        private static string _asc = "По возрастанию";
        //private List<string> _sortingAsc = [ _asc, "По убыванию" ];
        public List<string> SortingAsc => [_asc, "По убыванию"];

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
    }
}