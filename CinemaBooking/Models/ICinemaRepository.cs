namespace CinemaBooking.Models
{
    public interface ICinemaRepository
    {
        IQueryable<MovieSession> MovieSessions { get; }
    }
}
