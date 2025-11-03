using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Data.Models
{
    public class SeatStatus
    {
        public long SeatStatusID { get; set; }

        [Required]
        public long MovieSessionID { get; set; } 
        public MovieSession? MovieSession { get; set; }

        [Required]
        public long SeatID { get; set; }
        public Seat? Seat { get; set; }

        [Required]
        public SeatState Status { get; set; }

        public string? LockedByUserId { get; set; }
        public DateTime? LockedUntil { get; set; } 
    }
}