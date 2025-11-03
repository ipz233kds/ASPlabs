using CinemaBooking.Data.Models;
using CinemaBooking.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsController : ControllerBase
    {
        private readonly CinemaDbContext _context;

        public HallsController(CinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallDto>>> GetHalls()
        {
            var halls = await _context.Halls
                .Select(h => new HallDto
                {
                    HallID = h.HallID,
                    Name = h.Name
                })
                .ToListAsync();

            return Ok(halls);
        }
    }
}