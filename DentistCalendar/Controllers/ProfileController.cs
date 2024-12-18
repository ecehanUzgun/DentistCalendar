using DentistCalendar.Models.Entities;
using DentistCalendar.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentistCalendar.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            AppUser appUser = _userManager.Users.SingleOrDefault(x => x.UserName == HttpContext.User.Identity.Name);

            if (appUser == null)
            {
                return View("Error");
            }

            if (_userManager.IsInRoleAsync(appUser, "Secretary").Result)
            {
                var dentists = _userManager.Users.Where(x => x.IsDentist);
                SecretaryViewModel svm = new SecretaryViewModel()
                {
                    User = appUser,
                    Dentists = dentists,
                    DentistsSelectList = dentists.Select(n => new SelectListItem 
                    { 
                        Value = n.Id,
                        Text = $"Dt. {n.Name} {n.Surname}"
                    }).ToList()
                };
                return View("Secretary", svm);
            }
            else
            {
                return View("Dentist");
            }
        }

        public IActionResult Secretary() 
        {
            var appUser = _userManager.Users.SingleOrDefault(x => x.UserName == HttpContext.User.Identity.Name);

            if (appUser == null)
            {
                return View("Error");
            }

            SecretaryViewModel svm = new SecretaryViewModel()
            {
                User = appUser,
                Dentists = _userManager.Users.Where(x => x.IsDentist)
            };

            return View(svm);
        }

        public IActionResult Dentist() 
        {
            return View();
        }
    }
}
