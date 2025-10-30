using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Data.Models
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }
        public DbSet<MovieSession> MovieSessions => Set<MovieSession>();
        public DbSet<Movie> Movies => Set<Movie>();
    }
}