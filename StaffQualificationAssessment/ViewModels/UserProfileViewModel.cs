using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using StaffQualificationAssessment.Models;

namespace StaffQualificationAssessment.ViewModels
{
	public class UserProfileViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainVm;
        public MainWindowViewModel MainVm => _mainVm;
        public UserProfileViewModel(MainWindowViewModel mainVm, string userCode)
        {
            _mainVm = mainVm;
            _staff = _db.Staff.Include(s => s.Podst);
            _currentUser = _db.Staff.Include(s => s.Podst).First(s => s.CodStaff.ToString() == userCode);
            _availableUsers = GetAvailableUsers();
        }
        // user data
        private readonly Staff _currentUser;
        public Staff CurrentUser => _currentUser;
        // view any users
        private readonly List<Staff>? _availableUsers;
        public List<Staff>? AvailableUsers => _availableUsers;
        IQueryable<Staff> _staff;
        private List<Staff>? GetAvailableUsers() {
            IQueryable<Staff>? users = _currentUser!.Podst!.Title switch
            {
                "Начальник отдела 1" or "Начальник отдела 2" => _staff.Where(s => s.Podst == _currentUser!.Podst!),
                "Зам. директора по УР" or "Зам. директора по УПР" or "Зам. директора по ВР" => _staff.Where(s => s.Id == _currentUser.Id || s.Podst!.Title == "Преподаватель"),
                "Зам. директора по АХЧ" => _staff.Where(s => s.Id == _currentUser.Id || new string[] { "Дворник", "Грузчик" }.Contains(s.Podst!.Title)),
                "Директор" => _staff,
                _ => null
            };
            return users?.ToList();
        }
        public bool HaveAccessAnotherUsers => _availableUsers!= null && _availableUsers.Count > 0;
    }
}