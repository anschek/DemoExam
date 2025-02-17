
using PartnerInteraction.Models;
using PartnerInteraction.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerInteraction.ViewModels
{
    public class PartnerInfoViewModel : ViewModelBase
    {
        private DemoContext _db;
        private MainWindowViewModel _mainVM;
        private Partner _partner;

        public PartnerInfoViewModel(MainWindowViewModel mainVM, DemoContext db, int? partnerId)
        {
            _db = db;
            _mainVM = mainVM;
            _partner = db.Partners.Find(partnerId) ?? new Partner();
        }

        public string Title => _partner.Id == 0 ? "Создание партнера" : "Изменение данных партнера";
        public void GoBack() => _mainVM.CurrentView = new PartnersListView(_mainVM);
        public List<PartnerType> PartnerTypes => _db.PartnerTypes.ToList();
        public PartnerType? PartnerType {  get => Partner.PartnerType ; set => Partner.PartnerType = value; }
        public Partner Partner {  get => _partner; set => this.RaiseAndSetIfChanged(ref _partner, value); }


        public async Task CreateOrUpdatePartner()
        {
            try
            {
                if (_partner.Id == 0) _db.Partners.Add(Partner);
                _db.SaveChanges();
                _partner = _db.Partners.First(partner => partner.Email == Partner.Email);
                _mainVM.SetSuccess("Партнер успешно изменен");
            }
            catch(Exception ex)
            {
                _mainVM.SetError($"Ошибка создания или обновления партнера. Дополнительно: {ex.Message}");
            }
        }
    }
}
