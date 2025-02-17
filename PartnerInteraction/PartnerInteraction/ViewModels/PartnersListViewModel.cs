using System.Collections.Generic;
using PartnerInteraction.Models;
using System.Linq;
using PartnerInteraction.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using PartnerIntercation.Views;

namespace PartnerInteraction.ViewModels
{
    public class PartnersListViewModel : ViewModelBase
    {
        private DemoContext _db;
        private MainWindowViewModel _mainVM;
        public PartnersListViewModel(MainWindowViewModel mainVM)
        {
            _mainVM = mainVM;
            _db = new DemoContext();
        }
        private ushort GetDiscount(Partner partner)
        {
            int productsSum = partner.PartnersProducts.Sum(products => products.Amount);
            return productsSum switch
            {
                > 10000 and < 50000 => 5,
                > 50000 and < 300000 => 10,
                > 300000 => 15,
                _ => 0
            };
        }
        private List<Partner> _partners
        {
            get
            {
                List<Partner> partners = new List<Partner>();
                try
                {
                    partners = _db.Partners
                        .Include(partner => partner.PartnerType)
                        .Include(partner => partner.PartnersProducts)
                        .ToList();
                    _mainVM.Message = "";
                }
                catch (Exception ex)
                {
                    _mainVM.SetError($"Ошибка получения партнеров. Дополнительно: {ex.Message}");
                }
                return partners;

            }
        }
        public List<PartnerDto> Partners => _partners.Select(partner => new PartnerDto
        {
            Id = partner.Id,
            Name = partner.Name,
            Type = partner.PartnerType.Name,
            Director = partner.Director ?? "Нет информации о директоре",
            TelephoneNumber = $"+7 {partner.TelephoneNumber}",
            Rating = partner.Rating,
            Discount = GetDiscount(partner)
        }).ToList();
        public void CreatePartner() => _mainVM.CurrentView = new PartnerInfoView(_mainVM, _db);
        public void EditPartnerById(int partnerId) => _mainVM.CurrentView = new PartnerInfoView(_mainVM, _db, partnerId);
        
    }
}