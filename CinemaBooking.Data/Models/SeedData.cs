using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBooking.Data.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetRequiredService<CinemaDbContext>();

                
                if (!context.Halls.Any())
                {

                    var hall1_imax = new Hall { Name = "Зал 1 (IMAX)", Rows = 10, SeatsPerRow = 15 };
                    var hall2 = new Hall { Name = "Зал 2", Rows = 8, SeatsPerRow = 12 };
                    var hall3_child = new Hall { Name = "Зал 3 (Дитячий)", Rows = 5, SeatsPerRow = 8 };

                    for (int r = 1; r <= hall1_imax.Rows; r++)
                    {
                        for (int s = 1; s <= hall1_imax.SeatsPerRow; s++)
                        {
                            hall1_imax.Seats.Add(new Seat { Row = r, SeatNumber = s });
                        }
                    }

                    for (int r = 1; r <= hall2.Rows; r++)
                    {
                        for (int s = 1; s <= hall2.SeatsPerRow; s++)
                        {
                            hall2.Seats.Add(new Seat { Row = r, SeatNumber = s });
                        }
                    }

                    for (int r = 1; r <= hall3_child.Rows; r++)
                    {
                        for (int s = 1; s <= hall3_child.SeatsPerRow; s++)
                        {
                            hall3_child.Seats.Add(new Seat { Row = r, SeatNumber = s });
                        }
                    }
                    context.Halls.AddRange(hall1_imax, hall2, hall3_child);


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

                    joker = new Movie
                    {
                        Title = "Джокер: Божевілля на двох",
                        Description = "Артур Флек у психіатричній лікарні Аркхем зустрічає Гарлі Квінн.",
                        Genre = "Трилер / Мюзикл"
                    };
                    panda = new Movie
                    {
                        Title = "Панда Кунг-Фу 4",
                        Description = "Воїн Дракона По має знайти собі наступника, перш ніж стати Духовним лідером Долини Миру.",
                        Genre = "Мультфільм"
                    };
                    civilWar = new Movie
                    {
                        Title = "Повстання Штатів",
                        Description = "У найближчому майбутньому команда журналістів подорожує Сполученими Штатами під час громадянської війни.",
                        Genre = "Бойовик"
                    };
                    mavka = new Movie
                    {
                        Title = "Мавка. Лісова пісня",
                        Description = "Мавка, душа Лісу, стикається з неможливим вибором між коханням і обов'язком берегині.",
                        Genre = "Мультфільм"
                    };

                    context.Movies.AddRange(dune, joker, panda, civilWar, mavka);

                    var session1 = new MovieSession
                    {
                        Movie = dune,
                        Hall = hall1_imax,
                        Price = 220,
                        SessionTime = DateTime.ParseExact("28.10.2025 18:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    };
                    foreach (var seat in hall1_imax.Seats)
                    {
                        session1.SeatStatuses.Add(new SeatStatus { Seat = seat, Status = SeatState.Available });
                    }

                    var session2 = new MovieSession
                    {
                        Movie = joker,
                        Hall = hall2,
                        Price = 180,
                        SessionTime = DateTime.ParseExact("28.10.2025 19:30", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    };
                    foreach (var seat in hall2.Seats)
                    {
                        session2.SeatStatuses.Add(new SeatStatus { Seat = seat, Status = SeatState.Available });
                    }

                    var session3 = new MovieSession
                    {
                        Movie = panda,
                        Hall = hall3_child,
                        Price = 150,
                        SessionTime = DateTime.ParseExact("28.10.2025 16:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    };
                    foreach (var seat in hall3_child.Seats)
                    {
                        session3.SeatStatuses.Add(new SeatStatus { Seat = seat, Status = SeatState.Available });
                    }

                    var session4 = new MovieSession
                    {
                        Movie = civilWar,
                        Hall = hall1_imax,
                        Price = 210,
                        SessionTime = DateTime.ParseExact("28.10.2025 21:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    };
                    foreach (var seat in hall1_imax.Seats)
                    {
                        session4.SeatStatuses.Add(new SeatStatus { Seat = seat, Status = SeatState.Available });
                    }

                    var session5 = new MovieSession
                    {
                        Movie = mavka,
                        Hall = hall3_child,
                        Price = 140,
                        SessionTime = DateTime.ParseExact("28.10.2025 14:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
                    };
                    foreach (var seat in hall3_child.Seats) 
                    {
                        session5.SeatStatuses.Add(new SeatStatus { Seat = seat, Status = SeatState.Available });
                    }

                    context.MovieSessions.AddRange(session1, session2, session3, session4, session5);

                    context.SaveChanges();
                }
            }
        }
    }
}