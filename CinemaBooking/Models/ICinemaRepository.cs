using System.Linq;

namespace CinemaBooking.Models
{
    public interface ICinemaRepository
    {
        IQueryable<MovieSession> MovieSessions { get; }

        IQueryable<Movie> Movies { get; }

        void SaveMovie(Movie m);

        void DeleteMovie(Movie m);

        void SaveSession(MovieSession s);

        void DeleteSession(MovieSession s);
    }
}