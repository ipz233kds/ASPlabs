using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;
using CinemaBooking.Models.ViewModels;

namespace CinemaBooking.Controllers
{
    public class BookingController : Controller
    {
        private ICinemaRepository repository;
        private Booking booking;

        public BookingController(ICinemaRepository repo, Booking bookingService)
        {
            repository = repo;
            booking = bookingService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new BookingIndexViewModel
            {
                Booking = booking,
                ReturnUrl = returnUrl ?? "/"
            });
        }

        [HttpPost]
        public IActionResult AddToBooking(long movieSessionID, string returnUrl)
        {
            MovieSession? session = repository.MovieSessions
                .FirstOrDefault(p => p.MovieSessionID == movieSessionID);

            if (session != null)
            {
                booking.AddItem(session, 1);
            }
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        public IActionResult RemoveFromBooking(long movieSessionID, string returnUrl)
        {
            MovieSession? session = repository.MovieSessions
                .FirstOrDefault(p => p.MovieSessionID == movieSessionID);

            if (session != null)
            {
                booking.RemoveLine(session);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}