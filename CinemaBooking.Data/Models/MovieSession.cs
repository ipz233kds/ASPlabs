using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Data.Models
{
    public class MovieSession
    {
        public long MovieSessionID { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть назву залу")]
        public string Hall { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть ціну")]
        [Column(TypeName = "decimal(8, 2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Будь ласка, введіть позитивну ціну")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть час сеансу")]
        public DateTime SessionTime { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть фільм")]
        public long? MovieID { get; set; }

        public Movie? Movie { get; set; }
    }
}