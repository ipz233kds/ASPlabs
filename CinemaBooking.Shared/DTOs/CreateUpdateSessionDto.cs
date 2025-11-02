using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Shared.DTOs
{
    public class CreateUpdateSessionDto
    {
        [Required(ErrorMessage = "Будь ласка, введіть назву залу")]
        public string Hall { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть ціну")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Будь ласка, введіть позитивну ціну")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть час сеансу")]
        public DateTime SessionTime { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть ID фільму")]
        public long MovieID { get; set; }
    }
}