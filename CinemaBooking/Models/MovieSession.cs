using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBooking.Models
{
    public class MovieSession
    {
        public long? MovieSessionID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Hall { get; set; } = string.Empty;

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public DateTime SessionTime { get; set; }
    }
}