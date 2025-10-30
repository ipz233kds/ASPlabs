namespace CinemaBooking.Data.Models
{
    public class BookingLine
    {
        public long BookingLineID { get; set; }
        public MovieSession MovieSession { get; set; } = new();
        public int Quantity { get; set; }
    }
}