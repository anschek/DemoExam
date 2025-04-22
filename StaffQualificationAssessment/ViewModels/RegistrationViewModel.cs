using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using StaffQualificationAssessment.Models;

namespace StaffQualificationAssessment.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainVm;
        public MainWindowViewModel MainVm => _mainVm;


        public RegistrationViewModel(MainWindowViewModel mainVm)
        {
            _mainVm = mainVm;
            _name = string.Empty;
            _surname = string.Empty;
            _patronimyc = string.Empty;
            _yearOfBirth = string.Empty;
            _userCode = string.Empty;
        }
        public List<Post> Posts => _db.Posts.ToList();
        public List<Department> Departments =>  [ new(), .. _db.Departments ]; // new() - empty department with id=0
        private string _message;
        private string _name;
        private string _surname;
        private string _patronimyc;
        private string _yearOfBirth;
        private string _userCode;
        private Post? _selectedPost;
        private Department? _selectedDepartment;
        public string Message { get => _message; set => this.RaiseAndSetIfChanged(ref _message, value); }
        public string Name { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }
        public string Surname { get => _surname; set => this.RaiseAndSetIfChanged(ref _surname, value); }
        public string Patronimyc { get => _patronimyc; set => this.RaiseAndSetIfChanged(ref _patronimyc, value); }
        public string YearOfBirth { get => _yearOfBirth; set => this.RaiseAndSetIfChanged(ref _yearOfBirth, value); }
        public string UserCode { get => _userCode; set => this.RaiseAndSetIfChanged(ref _userCode, value); }
        public Post? SelectedPost { get => _selectedPost; set => this.RaiseAndSetIfChanged(ref _selectedPost, value); }
        public Department? SelectedDepartment { get => _selectedDepartment; set => this.RaiseAndSetIfChanged(ref _selectedDepartment, value); }

        public async Task Register()
        {
            try
            {
                if (new string[] { _name, _surname, _patronimyc }.Any(string.IsNullOrWhiteSpace))
                {
                    await _mainVm.SetError("Full name must be filled");
                    return;
                }
                if(!Int32.TryParse(_yearOfBirth, out var yearOfBirth) || !Int32.TryParse(_userCode, out var userCode))
                {
                    await _mainVm.SetError("Numeric field invalid (year of birth or user code)");
                    return;
                }
                if (yearOfBirth < 1900 || userCode < 1000)
                {
                    await _mainVm.SetError("year of birth and user code must contain 4 digits");
                    return;
                }
                if(SelectedPost == null || SelectedPost?.Id == 0)
                {
                    await _mainVm.SetError("Post must be filled");
                    return;
                }

                _mainVm.ClearError();
                await _db.Staff.AddAsync(new Staff
                {
                     Name = _name,
                     Surname = _surname,
                     Patronymic = _patronimyc,
                     YearBirthday = yearOfBirth,
                     CodStaff = userCode,
                     DepartmentId = SelectedDepartment?.Id,
                     PodstId = SelectedPost?.Id
                });

                var result = await _db.SaveChangesAsync();
                if(result != 0)
                {
                    Message = "Employee successfully added";
                    await Task.Delay(3000);
                    Message = "";
                }
            }
            catch
            {
                await _mainVm.SetError("Employee cannot be registered");
            }
        }
    }
}