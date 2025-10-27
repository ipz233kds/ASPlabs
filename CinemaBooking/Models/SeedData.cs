using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CinemaBooking.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            CinemaDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<CinemaDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.MovieSessions.Any())
            {
                context.MovieSessions.AddRange(
                    new MovieSession
                    {
                        Title = "Дюна: Частина друга",
                        Genre = "Фантастика",
                        Hall = "Зал 1 (IMAX)",
                        Price = 220,
                        SessionTime = DateTime.ParseExact("28.10.2025 18:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    },
                    new MovieSession
                    {
                        Title = "Джокер: Божевілля на двох",
                        Genre = "Трилер / Мюзикл",
                        Hall = "Зал 2",
                        Price = 180,
                        SessionTime = DateTime.ParseExact("28.10.2025 19:30", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    },
                    new MovieSession
                    {
                        Title = "Панда Кунг-Фу 4",
                        Genre = "Мультфільм",
                        Hall = "Зал 3 (Дитячий)",
                        Price = 150,
                        SessionTime = DateTime.ParseExact("28.10.2025 16:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    },
                    new MovieSession
                    {
                        Title = "Повстання Штатів",
                        Genre = "Бойовик",
                        Hall = "Зал 1 (IMAX)",
                        Price = 210,
                        SessionTime = DateTime.ParseExact("28.10.2025 21:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    },
                    new MovieSession
                    {
                        Title = "Мавка. Лісова пісня",
                        Genre = "Мультфільм",
                        Hall = "Зал 3 (Дитячий)",
                        Price = 140,
                        SessionTime = DateTime.ParseExact("28.10.2025 14:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}