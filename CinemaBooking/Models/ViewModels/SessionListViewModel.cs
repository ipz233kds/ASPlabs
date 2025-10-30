using CinemaBooking.Data.Models;

namespace CinemaBooking.Models.ViewModels
{
    public class SessionListViewModel
    {
        public IEnumerable<MovieSession> MovieSessions { get; set; } = Enumerable.Empty<MovieSession>();

        public PagingInfo PagingInfo { get; set; } = new();

        public string? CurrentGenre { get; set; }
    }
}