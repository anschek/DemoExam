
using PartnerInteraction.Models;
using PartnerInteraction.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public PartnerType? PartnerType { get => Partner.PartnerType; set => Partner.PartnerType = value; }
        public Partner Partner { get => _partner; set => this.RaiseAndSetIfChanged(ref _partner, value); }

        public async Task CreateOrUpdatePartner()
        {
            try
            {
                Validation();
                if (_partner.Id == 0) _db.Partners.Add(Partner);
                _db.SaveChanges();
                _partner = _db.Partners.First(partner => partner.Email == Partner.Email);
                _mainVM.SetSuccess("Партнер успешно изменен");
            }
            catch (ArgumentException argEx)
            {
                _mainVM.SetError($"Ошибка валидации: {argEx.Message}");
            }
            catch (Exception ex)
            {
                _mainVM.SetError($"Ошибка создания или обновления партнера. Дополнительно: {ex.Message}");
            }
        }

        private void Validation()
        {
            string?[] stringData = [Partner.Name, Partner.LegalAddress, Partner.Email, Partner.TelephoneNumber, Partner.Inn, Partner.Director];
            if (stringData.Any(string.IsNullOrEmpty)) throw new ArgumentNullException(message:"Все поля должны быть заполнены", null);

            if (PartnerType == null) throw new ArgumentNullException(message:"Выберите тип партнера", null);

            if (Regex.IsMatch(Partner.TelephoneNumber, @"[^0-9]\s")) throw new ArgumentNullException(message:"Номер телефона может содержать только цифры и пробелы", null);
            if (Regex.IsMatch(Partner.Inn, @"[^0-9]")) throw new ArgumentNullException(message:"ИНН должен содержать только цифры", null);
        }
    }
}
