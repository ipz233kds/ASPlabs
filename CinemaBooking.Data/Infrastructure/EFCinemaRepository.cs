using CinemaBooking.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CinemaBooking.Data.Infrastructure
{
    public class EFCinemaRepository : ICinemaRepository
    {
        private CinemaDbContext context;

        public EFCinemaRepository(CinemaDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<MovieSession> MovieSessions => context.MovieSessions
                                                            .Include(s => s.Movie);

        public IQueryable<Movie> Movies => context.Movies;

        public void SaveMovie(Movie m)
        {
            if (m.MovieID == 0)
            {
                context.Movies.Add(m);
            }
            else
            {
                context.Movies.Update(m);
            }
            context.SaveChanges();
        }

        public void DeleteMovie(Movie m)
        {
            context.Movies.Remove(m);
            context.SaveChanges();
        }

        public void SaveSession(MovieSession s)
        {
            if (s.MovieSessionID == 0)
            {
                context.MovieSessions.Add(s);
            }
            else
            {
                context.MovieSessions.Update(s);
            }
            context.SaveChanges();
        }

        public void DeleteSession(MovieSession s)
        {
            context.MovieSessions.Remove(s);
            context.SaveChanges();
        }
    }
}

