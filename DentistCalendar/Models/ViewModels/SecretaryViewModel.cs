using DentistCalendar.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentistCalendar.Models.ViewModels
{
    public class SecretaryViewModel
    {
        public AppUser User { get; set; }
        public IEnumerable<AppUser> Dentists { get; set; }
        public List<SelectListItem> DentistsSelectList { get; internal set; }
    }
}
