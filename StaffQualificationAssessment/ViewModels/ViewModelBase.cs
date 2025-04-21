using ReactiveUI;
using StaffQualificationAssessment.Models;

namespace StaffQualificationAssessment.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        protected readonly PmContext _db;
        protected ViewModelBase() => _db = new();
    }
}
