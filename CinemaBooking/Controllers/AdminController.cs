using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaBooking.Controllers
{
    public class AdminController : Controller
    {
        private ICinemaRepository repository;

        public AdminController(ICinemaRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Movies);

        public ViewResult MovieDetails(long movieId)
        {
            Movie? movie = repository.Movies
                .FirstOrDefault(m => m.MovieID == movieId);
            return View(movie);
        }

        public ViewResult CreateMovie() => View("MovieEditor", new Movie());

        public ViewResult EditMovie(long movieId)
        {
            Movie? movie = repository.Movies
                .FirstOrDefault(m => m.MovieID == movieId);
            return View("MovieEditor", movie);
        }

        [HttpPost]
        public IActionResult SaveMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repository.SaveMovie(movie);
                TempData["message"] = $"{movie.Title} було збережено";
                return RedirectToAction("Index");
            }
            else
            {
                return View("MovieEditor", movie);
            }
        }

        public ViewResult DeleteMovieConfirmation(long movieId)
        {
            Movie? movie = repository.Movies
                .FirstOrDefault(m => m.MovieID == movieId);
            return View(movie);
        }

        [HttpPost]
        public IActionResult DeleteMovie(long movieId)
        {
            Movie? movieToDelete = repository.Movies.FirstOrDefault(m => m.MovieID == movieId);
            if (movieToDelete != null)
            {
                repository.DeleteMovie(movieToDelete);
                TempData["message"] = $"{movieToDelete.Title} було видалено";
            }
            return RedirectToAction("Index");
        }

        public ViewResult SessionIndex() => View(repository.MovieSessions);

        public ViewResult SessionDetails(long sessionId)
        {
            MovieSession? session = repository.MovieSessions
                .FirstOrDefault(s => s.MovieSessionID == sessionId);
            return View(session);
        }

        public ViewResult CreateSession()
        {
            ViewBag.Movies = new SelectList(repository.Movies, "MovieID", "Title");
            return View("SessionEditor", new MovieSession());
        }

        public ViewResult EditSession(long sessionId)
        {
            MovieSession? session = repository.MovieSessions
                .FirstOrDefault(s => s.MovieSessionID == sessionId);
            ViewBag.Movies = new SelectList(repository.Movies, "MovieID", "Title", session?.MovieID);
            return View("SessionEditor", session);
        }

        [HttpPost]
        public IActionResult SaveSession(MovieSession session)
        {
            if (ModelState.IsValid)
            {
                repository.SaveSession(session);
                TempData["message"] = $"Сеанс для фільму ID {session.MovieID} на {session.SessionTime:g} було збережено";
                return RedirectToAction("SessionIndex");
            }
            else
            {
                ViewBag.Movies = new SelectList(repository.Movies, "MovieID", "Title", session.MovieID);
                return View("SessionEditor", session);
            }
        }

        public ViewResult DeleteSessionConfirmation(long sessionId)
        {
            MovieSession? session = repository.MovieSessions
                .FirstOrDefault(s => s.MovieSessionID == sessionId);
            return View(session);
        }

        [HttpPost]
        public IActionResult DeleteSession(long sessionId)
        {
            MovieSession? sessionToDelete = repository.MovieSessions.FirstOrDefault(s => s.MovieSessionID == sessionId);
            if (sessionToDelete != null)
            {
                repository.DeleteSession(sessionToDelete);
                TempData["message"] = $"Сеанс на {sessionToDelete.SessionTime:g} було видалено";
            }
            return RedirectToAction("SessionIndex");
        }
    }
}