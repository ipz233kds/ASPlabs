﻿using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Models
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }
        public DbSet<MovieSession> MovieSessions => Set<MovieSession>();
    }
}
