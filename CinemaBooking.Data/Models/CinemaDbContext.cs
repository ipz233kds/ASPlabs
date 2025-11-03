using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Data.Models
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }
        public DbSet<MovieSession> MovieSessions => Set<MovieSession>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Hall> Halls => Set<Hall>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<SeatStatus> SeatStatuses => Set<SeatStatus>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SeatStatus>()
                .HasOne(ss => ss.Seat)
                .WithMany() 
                .HasForeignKey(ss => ss.SeatID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SeatStatus>()
                .HasOne(ss => ss.MovieSession)
                .WithMany(ms => ms.SeatStatuses) 
                .HasForeignKey(ss => ss.MovieSessionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}