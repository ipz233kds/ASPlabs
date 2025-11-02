namespace CinemaBooking.Shared.DTOs
{
    public class MovieSessionDto
    {
        public long MovieSessionID { get; set; }
        public string Hall { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime SessionTime { get; set; }

        public long? MovieID { get; set; }

        public string? MovieTitle { get; set; }
        public string? MovieGenre { get; set; }
    }
}