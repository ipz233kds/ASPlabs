using Microsoft.AspNetCore.Identity;

namespace CinemaBooking.Models.ViewModels
{
    public class ProfileViewModel
    {
        public IdentityUser? User { get; set; }

    }
}