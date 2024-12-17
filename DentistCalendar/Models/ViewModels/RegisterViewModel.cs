using System.ComponentModel.DataAnnotations;

namespace DentistCalendar.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez!")]
        public string Username { get; set; }

        [Display(Name = "Adınız:")]
        [Required(ErrorMessage = "Lütfen ad belirtiniz.")]
        public string Name { get; set; }

        [Display(Name = "Soyadınız:")]
        [Required(ErrorMessage = "Lütfen soyad belirtiniz.")]
        public string Surname { get; set; }

        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Eposta boş geçilemez!")]
        [EmailAddress(ErrorMessage = "Lütfen eposta formatına uygun değer girin!")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        public string Password { get; set; }

        [Display(Name = "Şifre (Tekrar)")]
        [Required(ErrorMessage = "Şifre (Tekrar) boş geçilemez!")]
        [Compare("Password",ErrorMessage ="Şifreler aynı değil!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Randevu Rengi")]
        public string Color { get; set; }

        [Display(Name = "Doktorum")]
        public bool IsDentist { get; set; }
    }
}
