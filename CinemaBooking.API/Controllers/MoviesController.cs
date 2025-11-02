using CinemaBooking.Shared.DTOs;
using CinemaBooking.Data.Infrastructure;
using CinemaBooking.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ICinemaRepository _repository;

        public MoviesController(ICinemaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _repository.Movies
                .Select(m => new MovieDto
                {
                    MovieID = m.MovieID,
                    Title = m.Title,
                    Description = m.Description,
                    Genre = m.Genre
                })
                .ToListAsync();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(long id)
        {
            var movie = await _repository.Movies
                .Select(m => new MovieDto
                {
                    MovieID = m.MovieID,
                    Title = m.Title,
                    Description = m.Description,
                    Genre = m.Genre
                })
                .FirstOrDefaultAsync(m => m.MovieID == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<MovieDto> CreateMovie(CreateUpdateMovieDto createMovieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = new Movie
            {
                Title = createMovieDto.Title,
                Description = createMovieDto.Description,
                Genre = createMovieDto.Genre
            };

            _repository.SaveMovie(movie);

            var movieDto = new MovieDto
            {
                MovieID = movie.MovieID,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre
            };

            return CreatedAtAction(nameof(GetMovie), new { id = movie.MovieID }, movieDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie(long id, CreateUpdateMovieDto updateMovieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieToUpdate = await _repository.Movies
                                                .FirstOrDefaultAsync(m => m.MovieID == id);

            if (movieToUpdate == null)
            {
                return NotFound();
            }

            movieToUpdate.Title = updateMovieDto.Title;
            movieToUpdate.Description = updateMovieDto.Description;
            movieToUpdate.Genre = updateMovieDto.Genre;

            _repository.SaveMovie(movieToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(long id)
        {
            var movieToDelete = await _repository.Movies
                                                .FirstOrDefaultAsync(m => m.MovieID == id);

            if (movieToDelete == null)
            {
                return NotFound();
            }
            _repository.DeleteMovie(movieToDelete);

            return NoContent();
        }
    }
}

