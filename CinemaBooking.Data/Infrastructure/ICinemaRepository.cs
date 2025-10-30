using CinemaBooking.Data.Models;
using System.Linq;

namespace CinemaBooking.Data.Infrastructure
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