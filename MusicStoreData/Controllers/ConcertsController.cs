using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStoreData.Controllers
{
    [ApiController]
    public class ConcertsController : Controller
    {
        private readonly MusicStoreContext _context;
        public ConcertsController(MusicStoreContext context)
        {
            _context = context;
        }

        [HttpGet("concerts")]
        public async Task<ActionResult<IEnumerable<Concert>>> ShowConcerts()
        {
            var concerts = await _context.Concert
               .Where(t => t.isActive)
               .ToListAsync();

            if (concerts == null || !concerts.Any())
            {
                return NotFound(new { message = "No active concerts found." });
            }

            var concertDtos = concerts.Select(c => new Concert
            {
                Id = c.Id,
                Name = c.Name,
                OpeningTime = c.OpeningTime,
                City = c.City,
                Location = c.Location,
                MusicType = c.MusicType,
                Image = c.Image,
                isActive = c.isActive
            }).ToList();

            return Ok(concertDtos);
        }
        // show scpecific concert
        [HttpGet("concerts/{id}")]
        public async Task<ActionResult<Concert>> ShowConcert(int id)
        {
             var concert = await _context.Concert
                .Where(c => c.isActive && c.Id == id)
                .FirstOrDefaultAsync();

             if (concert == null)
            {
                return NotFound(new { message = "Concert not found." });
            }

             var concertDto = new Concert
            {
                Id = concert.Id,
                Name = concert.Name,
                OpeningTime = concert.OpeningTime,
                City = concert.City,
                Location = concert.Location,
                MusicType = concert.MusicType,
                Image = concert.Image,
                isActive = concert.isActive
            };

            return Ok(concertDto);
        }

        // edit concert
        [HttpPut("editconcerts/{id}")]
        public async Task<ActionResult<Concert>> EditConcert(int id, [FromBody] Concert concert)
        {
            concert.Id = id;

            if (ModelState.IsValid)
            {
                try
                {
                    var currentConcert = await _context.Concert.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                    if (currentConcert == null)
                    {
                        return NotFound(new { message = "Concert not found" });
                    }

                    currentConcert.Name = concert.Name;
                    currentConcert.City = concert.City;
                    currentConcert.Location = concert.Location;
                    currentConcert.MusicType = concert.MusicType;
                    currentConcert.OpeningTime = concert.OpeningTime;
                    currentConcert.Descriptions = concert.Descriptions;
                    currentConcert.Image = concert.Image;

                    currentConcert.ModificationDate = DateTime.Now;

                    _context.Update(currentConcert);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Concert updated successfully", currentConcert });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertExists(id))
                    {
                        return NotFound(new { message = "Concert no longer exists" });
                    }
                    else
                    {
                        throw;  
                    }
                }
            }

            return BadRequest(ModelState);
        }

        private bool ConcertExists(int id)
        {
            return _context.Concert.Any(c => c.Id == id);
        }

        [HttpDelete("deleteconcert/{id}")]
        public async Task<IActionResult> DeleteConcert(int id)
        {
             var concert = await _context.Concert.FindAsync(id);

            if (concert != null)
            {
                 concert.isActive = false;
                concert.DateOfDelete = DateTime.Now;

                 await _context.SaveChangesAsync();

                return Ok(new { message = "Concert marked as inactive.", concert });
            }

             return NotFound(new { message = "Concert not found." });
        }

    }
}
