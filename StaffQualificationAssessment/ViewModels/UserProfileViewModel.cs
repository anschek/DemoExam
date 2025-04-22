using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private List<Staff>? GetAvailableUsers()
        {
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
        public bool HaveAccessAnotherUsers => _availableUsers != null && _availableUsers.Count > 0;
        private Staff? _selectedUser;
        public Staff? SelectedUser { get => _selectedUser; set => this.RaiseAndSetIfChanged(ref _selectedUser, value); }
        private List<EmployeeMetric> _activities;
        public List<EmployeeMetric> Activities { get => _activities; private set => this.RaiseAndSetIfChanged(ref _activities, value); }

        public async Task GetActivities()
        {
            var activities = _db.EmployeeMetrics.Include(em => em.Metric.Criteria);
            if (new string[] { "Дворник", "Грузчик", "Преподаватель" }.Contains(_currentUser!.Podst!.Title))
            {
                Activities = activities.Where(em => em.StaffId == _currentUser!.Id).ToList();
                return;
            }

            if (_selectedUser == null)
            {
                await _mainVm.SetError("User not selected");
                return;
            }
            _mainVm.ClearError();
            Activities = activities.Where(em => em.StaffId == _selectedUser!.Id).ToList();
        }
    }
}