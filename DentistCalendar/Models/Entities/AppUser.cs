using Microsoft.AspNetCore.Identity;

namespace DentistCalendar.Models.Entities
{
    public class AppUser:IdentityUser   
    {
        public bool IsDentist { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Color { get; set; }

        //Relational Properties
        public List<Appointment> Appointments { get; set; }
    }
}
