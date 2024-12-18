namespace DentistCalendar.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public string UserId { get; set; }
        public string Dentist { get; set; }
        public string Patient { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
