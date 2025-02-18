using System;
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
		}

		private List<Material> _materials => _db.Materials
			.Include(material => material.TypeNavigation).ToList();

		public List<MaterialListItem> Materials => _materials.Select( material => new MaterialListItem
		{
			Name = material.Name,
			TypeName = material.TypeNavigation.Name,
			Amount = material.Amount,
			MinimalAmount = material.MinimalAmount,
			Cost = material.Cost,
			Description = material.Description ?? "*без описания*" ,
			Image = material.Image == null ? new Bitmap("empty_image.jpg")
			: new Bitmap(new MemoryStream(material.Image))
		}).ToList();


		// сортировка: возрастание/убывание, название/стоимость/остаток

		// фильтрация: тип + все типы

		// поиск: название и описание
	}
}