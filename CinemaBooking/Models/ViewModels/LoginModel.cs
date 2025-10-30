using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Будь ласка, введіть ім'я користувача")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть пароль")]
        [UIHint("password")]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = "/";
    }
}