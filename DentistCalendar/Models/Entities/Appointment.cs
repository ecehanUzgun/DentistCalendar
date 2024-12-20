namespace DentistCalendar.Models.Entities
{
    public class Appointment
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set;}
        public string? Description { get; set; }
    }
}
