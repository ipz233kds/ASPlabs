using CinemaBooking.Data.Models;
namespace CinemaBooking.Models.ViewModels
{
    public class BookingIndexViewModel
    {
        public Booking Booking { get; set; } = new();
        public string ReturnUrl { get; set; } = "/";
    }
}