using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.API.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Ім'я користувача є обов'язковим")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль є обов'язковим")]
        public string Password { get; set; } = string.Empty;
    }
}

