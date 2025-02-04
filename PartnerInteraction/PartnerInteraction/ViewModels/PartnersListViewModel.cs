using System;
using System.Collections.Generic;
using PartnerInteraction.Models;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Linq;
using PartnerInteraction.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace PartnerInteraction.ViewModels
{
    public class PartnersListViewModel : ViewModelBase
    {
        private DemoContext _db;
        private List<Partner> _partners => _db.Partners
            .Include(partner => partner.PartnerType)
            .Include(partner => partner.PartnersProducts)
            .ToList(); 
        public string Cat => "cat name";
        public PartnersListViewModel()
        {
            _db = new DemoContext();
        }

        public ushort GetDiscount(Partner partner)
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
        public List<PartnerDto> Partners => _partners.Select(partner => new PartnerDto
        {
            Name = partner.Name,
            Type = partner.PartnerType.Name,
            Director = partner.Director ?? "Нет информации о директоре",
            TelephoneNumber = $"+7 {partner.TelephoneNumber}",
            Rating = partner.Rating,
            Discount = GetDiscount(partner)
        }).ToList();
    }
}