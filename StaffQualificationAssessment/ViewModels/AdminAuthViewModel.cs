using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReactiveUI;
using StaffQualificationAssessment.Views;

namespace StaffQualificationAssessment.ViewModels
{
	public class AdminAuthViewModel : ReactiveObject
	{
		private readonly MainWindowViewModel _mainVm;        
		public MainWindowViewModel MainVm => _mainVm;
		public AdminAuthViewModel(MainWindowViewModel mainVm)
		{
			_mainVm = mainVm;
			_login = string.Empty;
			_password = string.Empty;
		}
		private string _login;
		private string _password;
        public string Login { get => _login; set =>this.RaiseAndSetIfChanged(ref _login, value); }
        public string Password { get => _password; set =>this.RaiseAndSetIfChanged(ref _password, value); }

        public async Task LogIn()
		{
			if(Login == "admin" && Password == "admin")
			{
				MainVm.ClearError();
				MainVm.CurrentView = new RegistrationView(MainVm);
			}
			else
			{
				await MainVm.SetError("Admin with this credentials not found");
			}
		}
    }
}