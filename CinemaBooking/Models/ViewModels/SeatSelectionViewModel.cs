using CinemaBooking.Data.Models;

namespace CinemaBooking.Models.ViewModels
{
    public class SeatSelectionViewModel
    {
        public MovieSession? Session { get; set; }

        public List<SeatStatus>? SeatStatuses { get; set; }

        public int MaxRow { get; set; }
        public int MaxSeatNumber { get; set; }
    }
}