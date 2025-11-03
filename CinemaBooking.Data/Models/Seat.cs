namespace CinemaBooking.Data.Models
{
    public class Seat
    {
        public long SeatID { get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }

        public long HallID { get; set; }
        public Hall? Hall { get; set; }
    }
}