using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.API.DTOs
{
    public class CreateUpdateMovieDto
    {
        [Required(ErrorMessage = "Будь ласка, введіть назву фільму")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть опис")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть жанр")]
        public string Genre { get; set; } = string.Empty;
    }
}