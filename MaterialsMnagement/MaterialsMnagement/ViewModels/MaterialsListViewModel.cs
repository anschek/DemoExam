using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Materials = _originalMaterials
                .Where(isMaterialWithMatchingType)
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

        // сортировка: возрастание/убывание, название/стоимость/остаток

        // поиск: название и описание
    }
}