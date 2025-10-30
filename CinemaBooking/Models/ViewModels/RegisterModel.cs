using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Будь ласка, введіть ім'я користувача")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть Email")]
        [EmailAddress(ErrorMessage = "Будь ласка, введіть коректний Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть пароль")]
        [UIHint("password")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        [UIHint("password")]
        [Display(Name = "Підтвердіть пароль")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = "/";
    }
}