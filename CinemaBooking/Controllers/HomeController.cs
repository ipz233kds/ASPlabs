using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Data.Models;
using CinemaBooking.Data.Infrastructure;
using CinemaBooking.Models.ViewModels;
using System.Linq;

namespace CinemaBooking.Controllers
{
    public class HomeController : Controller
    {
        private ICinemaRepository repository;
        public int PageSize = 3;

        public HomeController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index(string genre, int page = 1)
        {
            var viewModel = new SessionListViewModel
            {
                MovieSessions = repository.MovieSessions
                    .Where(p => genre == null || (p.Movie != null && p.Movie.Genre == genre))
                    .OrderBy(p => p.MovieSessionID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,

                    TotalItems = (genre == null)
                        ? repository.MovieSessions.Count()
                        : repository.MovieSessions.Where(p => p.Movie != null && p.Movie.Genre == genre).Count()
                },

                CurrentGenre = genre
            };

            return View(viewModel);
        }
    }
}

