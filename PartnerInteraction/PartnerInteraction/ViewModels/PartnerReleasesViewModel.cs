using Microsoft.EntityFrameworkCore;
using PartnerInteraction.Models;
using PartnerInteraction.Models.DTOs;
using PartnerInteraction.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerInteraction.ViewModels
{
    public class PartnerReleasesViewModel: ViewModelBase
    {
        private DemoContext _db;
        private MainWindowViewModel _mainVM;
        private int _partnerId;

        public PartnerReleasesViewModel(MainWindowViewModel mainVM, DemoContext db, int partnerId)
        {
            _db = db;
            _mainVM = mainVM;
            _partnerId = partnerId;
        }
        public string PartnerName => _db.Partners.Find(_partnerId)?.Name ?? "";
        private List<PartnersProduct> _releaseHistory => _db.PartnersProducts
            .Include(releases => releases.Product)
            .ThenInclude(product => product.Type)
            .Where(releases=> releases.PartnerId == _partnerId).ToList();

        public List<ReleaseDto> ReleaseHistory => _releaseHistory.Select(release => new ReleaseDto
        {
            ProductName = release.Product.Name,
            ProductTypeName = release.Product.Type.TypeName,
            ReleaseDate = release.DateOfSale.ToString("dd.MM.yyyy"),
            Amount = release.Amount
        }).ToList();
        public void GoBack() => _mainVM.CurrentView = new PartnersListView(_mainVM);
    }
}
