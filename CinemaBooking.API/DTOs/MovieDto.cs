namespace CinemaBooking.API.DTOs
{
    public class MovieDto
    {
        public long MovieID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
    }
}

