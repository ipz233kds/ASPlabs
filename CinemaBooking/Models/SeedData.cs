using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

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
            if (!context.Movies.Any())
            {
                var dune = new Movie
                {
                    Title = "Дюна: Частина друга",
                    Description = "Пол Атрід об'єднується з Чані та Фременами, щоб помститися змовникам, які знищили його родину.",
                    Genre = "Фантастика"
                };

                var joker = new Movie
                {
                    Title = "Джокер: Божевілля на двох",
                    Description = "Артур Флек у психіатричній лікарні Аркхем зустрічає Гарлі Квінн.",
                    Genre = "Трилер / Мюзикл"
                };

                var panda = new Movie
                {
                    Title = "Панда Кунг-Фу 4",
                    Description = "Воїн Дракона По має знайти собі наступника, перш ніж стати Духовним лідером Долини Миру.",
                    Genre = "Мультфільм"
                };

                var civilWar = new Movie
                {
                    Title = "Повстання Штатів",
                    Description = "У найближчому майбутньому команда журналістів подорожує Сполученими Штатами під час громадянської війни.",
                    Genre = "Бойовик"
                };

                var mavka = new Movie
                {
                    Title = "Мавка. Лісова пісня",
                    Description = "Мавка, душа Лісу, стикається з неможливим вибором між коханням і обов'язком берегині.",
                    Genre = "Мультфільм"
                };

                context.Movies.AddRange(dune, joker, panda, civilWar, mavka);

                context.MovieSessions.AddRange(
                    new MovieSession
                    {
                        Movie = dune,
                        Hall = "Зал 1 (IMAX)",
                        Price = 220,
                        SessionTime = DateTime.ParseExact("28.10.2025 18:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    },
                    new MovieSession
                    {
                        Movie = joker,
                        Hall = "Зал 2",
                        Price = 180,
                        SessionTime = DateTime.ParseExact("28.10.2025 19:30", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    },
                    new MovieSession
                    {
                        Movie = panda,
                        Hall = "Зал 3 (Дитячий)",
                        Price = 150,
                        SessionTime = DateTime.ParseExact("28.10.2025 16:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    },
                    new MovieSession
                    {
                        Movie = civilWar,
                        Hall = "Зал 1 (IMAX)",
                        Price = 210,
                        SessionTime = DateTime.ParseExact("28.10.2025 21:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    },
                    new MovieSession
                    {
                        Movie = mavka,
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

