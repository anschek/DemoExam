using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using StaffQualificationAssessment.Views;

namespace StaffQualificationAssessment.ViewModels
{
	public class AuthViewModel : ViewModelBase
	{
		public readonly MainWindowViewModel MainVm;
        public AuthViewModel(MainWindowViewModel mainVm)
        {
            MainVm = mainVm;
        }
        private string _userCode;
        public string UserCode { get => _userCode; set => this.RaiseAndSetIfChanged(ref _userCode, value); }
        public void GoToRegistrationView() => MainVm.CurrentView = new AdminAuthView(MainVm);
        
        public async Task LogIn()
        {
            var user = await _db.Staff.FirstOrDefaultAsync(s => s.CodStaff.ToString() == UserCode);

            if (user == null) 
            { 
                await MainVm.SetError($"User with code {UserCode} not found"); 
            }
            else
            {
                MainVm.ClearError();
                MainVm.CurrentView = new UserProfileView(MainVm);
            }
        }
    }
}