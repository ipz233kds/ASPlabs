using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;
using System.Linq;

namespace CinemaBooking.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ICinemaRepository repository;

        public NavigationMenuViewComponent(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenre = RouteData?.Values["genre"];

            var genres = repository.MovieSessions
                .Select(x => x.Genre)
                .Distinct()
                .OrderBy(x => x);

            return View(genres);
        }
    }
}