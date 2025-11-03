namespace CinemaBooking.Shared.DTOs
{
    // Цей DTO потрібен, щоб передати список залів у Blazor
    public class HallDto
    {
        public long HallID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}