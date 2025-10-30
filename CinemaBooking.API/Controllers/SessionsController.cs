using CinemaBooking.API.DTOs;
using CinemaBooking.Data.Infrastructure;
using CinemaBooking.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ICinemaRepository _repository;
        private readonly CinemaDbContext _context;

        public SessionsController(ICinemaRepository repository, CinemaDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieSessionDto>>> GetSessions()
        {
            var sessions = await _repository.MovieSessions
                .OrderBy(s => s.SessionTime)
                .Select(s => new MovieSessionDto
                {
                    MovieSessionID = s.MovieSessionID,
                    Hall = s.Hall,
                    Price = s.Price,
                    SessionTime = s.SessionTime,
                    MovieID = s.MovieID,
                    MovieTitle = s.Movie != null ? s.Movie.Title : "N/A",
                    MovieGenre = s.Movie != null ? s.Movie.Genre : "N/A"
                })
                .ToListAsync();

            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieSessionDto>> GetSession(long id)
        {
            var session = await _repository.MovieSessions
                .Where(s => s.MovieSessionID == id)
                .Select(s => new MovieSessionDto
                {
                    MovieSessionID = s.MovieSessionID,
                    Hall = s.Hall,
                    Price = s.Price,
                    SessionTime = s.SessionTime,
                    MovieID = s.MovieID,
                    MovieTitle = s.Movie != null ? s.Movie.Title : "N/A",
                    MovieGenre = s.Movie != null ? s.Movie.Genre : "N/A"
                })
                .FirstOrDefaultAsync();

            if (session == null)
            {
                return NotFound($"Сеанс з ID {id} не знайдено.");
            }

            return Ok(session);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MovieSessionDto>> CreateSession(CreateUpdateSessionDto createSessionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieExists = await _repository.Movies.AnyAsync(m => m.MovieID == createSessionDto.MovieID);
            if (!movieExists)
            {
                ModelState.AddModelError(nameof(createSessionDto.MovieID), $"Фільм з ID {createSessionDto.MovieID} не знайдено.");
                return BadRequest(ModelState);
            }

            var session = new MovieSession
            {
                Hall = createSessionDto.Hall,
                Price = createSessionDto.Price,
                SessionTime = createSessionDto.SessionTime,
                MovieID = createSessionDto.MovieID
            };

            _context.MovieSessions.Add(session);
            _repository.SaveSession(session);

            await _context.Entry(session).Reference(s => s.Movie).LoadAsync();

            var sessionDto = new MovieSessionDto
            {
                MovieSessionID = session.MovieSessionID,
                Hall = session.Hall,
                Price = session.Price,
                SessionTime = session.SessionTime,
                MovieID = session.MovieID,
                MovieTitle = session.Movie != null ? session.Movie.Title : "N/A",

                MovieGenre = session.Movie != null ? session.Movie.Genre : "N/A"
            };

            return CreatedAtAction(nameof(GetSession), new { id = session.MovieSessionID }, sessionDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSession(long id, CreateUpdateSessionDto updateSessionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieExists = await _repository.Movies.AnyAsync(m => m.MovieID == updateSessionDto.MovieID);
            if (!movieExists)
            {
                ModelState.AddModelError(nameof(updateSessionDto.MovieID), $"Фільм з ID {updateSessionDto.MovieID} не знайдено.");
                return BadRequest(ModelState);
            }

            var sessionExists = await _context.MovieSessions
                                            .AsNoTracking()
                                            .AnyAsync(s => s.MovieSessionID == id);

            if (!sessionExists)
            {
                return NotFound($"Сеанс з ID {id} не знайдено.");
            }

            var updatedSession = new MovieSession
            {
                MovieSessionID = id,
                Hall = updateSessionDto.Hall,
                Price = updateSessionDto.Price,
                SessionTime = updateSessionDto.SessionTime,
                MovieID = updateSessionDto.MovieID
            };

            _context.MovieSessions.Update(updatedSession);
            _repository.SaveSession(updatedSession);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSession(long id)
        {
            var sessionToDelete = await _context.MovieSessions
                                        .FirstOrDefaultAsync(s => s.MovieSessionID == id);

            if (sessionToDelete == null)
            {
                return NotFound($"Сеанс з ID {id} не знайдено.");
            }

            _repository.DeleteSession(sessionToDelete);

            return NoContent();
        }
    }
}

