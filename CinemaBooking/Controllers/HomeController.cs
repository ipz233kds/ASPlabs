using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;
using CinemaBooking.Models.ViewModels;

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

        public IActionResult Index(int page = 1)
        {
            var viewModel = new SessionListViewModel
            {
                MovieSessions = repository.MovieSessions
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.MovieSessions.Count()
                }
            };

            return View(viewModel);
        }
    }
}