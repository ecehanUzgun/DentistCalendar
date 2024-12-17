using DentistCalendar.Models.Entities;

namespace DentistCalendar.Models.ViewModels
{
    public class SecretaryViewModel
    {
        public AppUser User { get; set; }
        public IEnumerable<AppUser> Dentists { get; set; }
    }
}
