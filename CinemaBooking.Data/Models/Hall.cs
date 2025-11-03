namespace CinemaBooking.Data.Models
{
    public class Hall
    {
        public long HallID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Rows { get; set; }
        public int SeatsPerRow { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}