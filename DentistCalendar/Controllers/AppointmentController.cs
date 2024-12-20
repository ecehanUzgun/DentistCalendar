using DentistCalendar.Models.Context;
using DentistCalendar.Models.Entities;
using DentistCalendar.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentistCalendar.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public AppointmentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public JsonResult GetAppointments()
        {
            var model = _dbContext.Appointments
                .Include(x => x.User).Select(x => new AppointmentViewModel()
                {
                    Id = x.ID,
                    Dentist = x.User.Name + " " + x.User.Surname,
                    PatientName = x.PatientName, 
                    PatientSurname = x.PatientSurname,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    Color = x.User.Color,
                    UserId = x.User.Id
                });
            return Json(model);
        }

        public JsonResult GetAppointmentsByDentist(string userId = "")
        {
            var model = _dbContext.Appointments.Where(x => x.UserId == userId)
                .Include(x => x.User).Select(x => new AppointmentViewModel()
                {
                    Id = x.ID,
                    Dentist = x.User.Name + " " + x.User.Surname,
                    PatientName = x.PatientName,
                    PatientSurname = x.PatientSurname,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    Color = x.User.Color,
                    UserId = x.User.Id
                });

            return Json(model);
        }

        [HttpPost]
        public JsonResult AddOrUpdateAppointment(AddOrUpdateAppointmentVM model)
        {
            if (model.Id == 0)
            {
                Appointment entity = new Appointment()
                {
                    CreatedDate = DateTime.Now,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    PatientName = model.PatientName,
                    PatientSurname = model.PatientSurname,
                    Description = model.Description,
                    UserId = model.UserId
                };

                _dbContext.Add(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                var entity = _dbContext.Appointments.SingleOrDefault(x => x.ID == model.Id);
                if (entity == null) 
                { 
                    return Json("Güncellenecek veri bulunamadı.");
                }
                entity.UpdatedDate = DateTime.Now;
                entity.PatientName = model.PatientName;
                entity.PatientSurname = model.PatientSurname;
                entity.Description = model.Description;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.UserId = model.UserId;

                _dbContext.Update(entity);
                _dbContext.SaveChanges();
            }
            return Json("200");
        }

        public JsonResult DeleteAppointment(int id = 0)
        {
            var entity = _dbContext.Appointments.SingleOrDefault(x => x.ID == id);
            if (entity == null) 
            {
                return Json("Kayıt bulunamadı.");
            };
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return Json("200");
        }


    }
}
