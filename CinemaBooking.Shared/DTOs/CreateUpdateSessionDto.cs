using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Shared.DTOs
{
    public class CreateUpdateSessionDto
    {
        // ⛔️ ВЛАСТИВІСТЬ 'Hall' ТИПУ STRING БУЛО ВИДАЛЕНО.

        // ✅ ДОДАНО 'HallID' ТИПУ LONG
        [Required(ErrorMessage = "Будь ласка, вкажіть ID залу")]
        [Range(1, long.MaxValue, ErrorMessage = "Будь ласка, вкажіть ID залу")]
        public long HallID { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть ціну")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Будь ласка, введіть позитивну ціну")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть час сеансу")]
        public DateTime SessionTime { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть ID фільму")]
        public long MovieID { get; set; }
    }
}