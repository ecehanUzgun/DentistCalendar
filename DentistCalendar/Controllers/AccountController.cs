using DentistCalendar.Models.Entities;
using DentistCalendar.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentistCalendar.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager; 

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null) 
                { 
                    return View(user);
                }
            }
            return View();
        }

        // Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                //Kullanıcı oluştur
                AppUser appUser = new AppUser()
                {
                    UserName = user.Username,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Color = user.Color,
                    IsDentist = user.IsDentist
                };

                //Veritabanına kaydet
                IdentityResult result = _userManager.CreateAsync(appUser, user.Password).Result;

                if (result.Succeeded)
                {
                    bool roleCheck = user.IsDentist ? AddRole("Dentist") : AddRole("Secretary");

                    if (!roleCheck)
                    {
                        return View("Error");
                    }
                    _userManager.AddToRoleAsync(appUser, user.IsDentist ? "Dentist" : "Secretary").Wait();
                    return RedirectToAction("Index","Home");
                }
                return View("Error");
            }
            else
            {
                return View(user);
            }
        }

        private bool AddRole(string roleName)
        {
            if (!_roleManager.RoleExistsAsync(roleName).Result)
            {
                AppRole role = new AppRole()
                {
                    Name = roleName
                };

                IdentityResult result = _roleManager.CreateAsync(role).Result;
                return result.Succeeded;
            }
            return true;
        }

        //Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByEmailAsync(lvm.Email);
                if (appUser != null) 
                {
                    var result = await _signInManager.PasswordSignInAsync(appUser, lvm.Password, rememberMe, false); //false olduğunda yanlış şifre girilse de herhangi bir kitleme işlemi yapmaz

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Profile");
                    }
                }
            }
            return View(lvm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
