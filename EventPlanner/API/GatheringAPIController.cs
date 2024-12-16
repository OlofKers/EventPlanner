using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatheringAPIController : ControllerBase
    {
        private readonly EventplannerContext _context;

        public GatheringAPIController(EventplannerContext context)
        {
            _context = context;
        }

        // GET: api/GatheringAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gathering>>> GetGatherings()
        {
            return await _context.Gatherings.ToListAsync();
        }

        // GET: api/GatheringAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gathering>> GetGathering(int id)
        {
            var gathering = await _context.Gatherings.FindAsync(id);

            if (gathering == null)
            {
                return NotFound();
            }

            return gathering;
        }

        // PUT: api/GatheringAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGathering(int id, Gathering gathering)
        {
            if (id != gathering.GatheringId)
            {
                return BadRequest();
            }

            _context.Entry(gathering).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatheringExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GatheringAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gathering>> PostGathering(Gathering gathering)
        {
            _context.Gatherings.Add(gathering);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGathering", new { id = gathering.GatheringId }, gathering);
        }

        // DELETE: api/GatheringAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGathering(int id)
        {
            var gathering = await _context.Gatherings.FindAsync(id);
            if (gathering == null)
            {
                return NotFound();
            }

            _context.Gatherings.Remove(gathering);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GatheringExists(int id)
        {
            return _context.Gatherings.Any(e => e.GatheringId == id);
        }
    }
}
