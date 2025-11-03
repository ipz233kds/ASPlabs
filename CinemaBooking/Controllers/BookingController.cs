using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Data.Models;
using CinemaBooking.Data.Infrastructure;
using CinemaBooking.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using CinemaBooking.Hubs;  

namespace CinemaBooking.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private ICinemaRepository repository;
        private Booking booking;
        private readonly CinemaDbContext _context;
        private readonly IHubContext<BookingHub> _hubContext;

        public BookingController(ICinemaRepository repo,
                                 Booking bookingService,
                                 CinemaDbContext context,
                                 IHubContext<BookingHub> hubContext)
        {
            repository = repo;
            booking = bookingService;
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> SelectSeats(long movieSessionID)
        {
            var session = await _context.MovieSessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefaultAsync(s => s.MovieSessionID == movieSessionID);

            if (session == null || session.Hall == null)
            {
                return NotFound();
            }

            var seatStatuses = await _context.SeatStatuses
                .Where(ss => ss.MovieSessionID == movieSessionID)
                .Include(ss => ss.Seat)
                .OrderBy(ss => ss.Seat!.Row)
                .ThenBy(ss => ss.Seat!.SeatNumber)
                .ToListAsync();

            var model = new SeatSelectionViewModel
            {
                Session = session,
                SeatStatuses = seatStatuses,
                MaxRow = session.Hall!.Rows,
                MaxSeatNumber = session.Hall!.SeatsPerRow
            };

            return View("SelectSeats", model);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmSelection(long movieSessionID, string selectedSeatStatusIds)
        {
            if (string.IsNullOrEmpty(selectedSeatStatusIds))
            {
                TempData["message"] = "Помилка: Ви не обрали жодного місця.";
                return RedirectToAction("SelectSeats", new { movieSessionID });
            }

            var selectedIds = selectedSeatStatusIds.Split(',')
                                .Select(long.Parse)
                                .ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var seatsToBook = await _context.SeatStatuses
                .Where(s => s.MovieSessionID == movieSessionID &&
                            selectedIds.Contains(s.SeatStatusID) &&
                            s.Status == SeatState.Available)
                .ToListAsync();

            if (seatsToBook.Count != selectedIds.Count)
            {
                TempData["message"] = "Помилка! Одне або декілька з обраних місць стали недоступними. Спробуйте ще раз.";
                return RedirectToAction("SelectSeats", new { movieSessionID });
            }

            foreach (var seat in seatsToBook)
            {
                seat.Status = SeatState.Sold;
                seat.LockedByUserId = userId;
            }

            await _context.SaveChangesAsync();

            foreach (var seat in seatsToBook)
            {
                await _hubContext.Clients.All.SendAsync("UpdateSeatToSold", seat.SeatStatusID);
            }

            TempData["message"] = $"Бронювання успішне! Ви забронювали {seatsToBook.Count} квит(ка/ів).";
            return RedirectToAction("BookingConfirmation");
        }

        [HttpGet]
        public IActionResult BookingConfirmation()
        {
            return View();
        }

        
    }
}