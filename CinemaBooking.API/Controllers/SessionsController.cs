using CinemaBooking.Shared.DTOs;
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
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .OrderBy(s => s.SessionTime)
                .Select(s => new MovieSessionDto
                {
                    MovieSessionID = s.MovieSessionID,
                    Hall = s.Hall != null ? s.Hall.Name : "N/A",
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
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .Where(s => s.MovieSessionID == id)
                .Select(s => new MovieSessionDto
                {
                    MovieSessionID = s.MovieSessionID,
                    Hall = s.Hall != null ? s.Hall.Name : "N/A",
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

            var hallExists = await _context.Halls.AnyAsync(h => h.HallID == createSessionDto.HallID);
            if (!hallExists)
            {
                ModelState.AddModelError(nameof(createSessionDto.HallID), $"Зал з ID {createSessionDto.HallID} не знайдено.");
                return BadRequest(ModelState);
            }

            var session = new MovieSession
            {
                HallID = createSessionDto.HallID,
                Price = createSessionDto.Price,
                SessionTime = createSessionDto.SessionTime,
                MovieID = createSessionDto.MovieID
            };

            var seatsInHall = await _context.Seats.Where(s => s.HallID == createSessionDto.HallID).ToListAsync();
            foreach (var seat in seatsInHall)
            {
                session.SeatStatuses.Add(new SeatStatus
                {
                    SeatID = seat.SeatID,
                    Status = SeatState.Available
                });
            }

            _context.MovieSessions.Add(session);
            await _context.SaveChangesAsync();

            await _context.Entry(session).Reference(s => s.Movie).LoadAsync();
            await _context.Entry(session).Reference(s => s.Hall).LoadAsync();

            var sessionDto = new MovieSessionDto
            {
                MovieSessionID = session.MovieSessionID,
                Hall = session.Hall != null ? session.Hall.Name : "N/A",
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

            var hallExists = await _context.Halls.AnyAsync(h => h.HallID == updateSessionDto.HallID);
            if (!hallExists)
            {
                ModelState.AddModelError(nameof(updateSessionDto.HallID), $"Зал з ID {updateSessionDto.HallID} не знайдено.");
                return BadRequest(ModelState);
            }

            var sessionToUpdate = await _context.MovieSessions.FindAsync(id);

            if (sessionToUpdate == null)
            {
                return NotFound($"Сеанс з ID {id} не знайдено.");
            }

            sessionToUpdate.HallID = updateSessionDto.HallID;
            sessionToUpdate.Price = updateSessionDto.Price;
            sessionToUpdate.SessionTime = updateSessionDto.SessionTime;
            sessionToUpdate.MovieID = updateSessionDto.MovieID;


            _context.MovieSessions.Update(sessionToUpdate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSession(long id)
        {
            var sessionToDelete = await _context.MovieSessions
                .Include(s => s.SeatStatuses)
                .FirstOrDefaultAsync(s => s.MovieSessionID == id);

            if (sessionToDelete == null)
            {
                return NotFound($"Сеанс з ID {id} не знайдено.");
            }

            _context.SeatStatuses.RemoveRange(sessionToDelete.SeatStatuses);

            _context.MovieSessions.Remove(sessionToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}