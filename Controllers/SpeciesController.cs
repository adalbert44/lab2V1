using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab2V1.Models;

namespace lab2V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly Lab2LibraryContext _context;

        public SpeciesController(Lab2LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Species
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Species>>> GetSpecieses()
        {
            return await _context.Specieses.ToListAsync();
        }

        // GET: api/Species/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Species>> GetSpecies(int id)
        {
            var species = await _context.Specieses.FindAsync(id);

            if (species == null)
            {
                return NotFound();
            }

            return species;
        }

        // PUT: api/Species/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecies(int id, Species species)
        {
            if (id != species.Id)
            {
                return BadRequest();
            }

            _context.Entry(species).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeciesExists(id))
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

        // POST: api/Species
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Species>> PostSpecies(Species species)
        {
            _context.Specieses.Add(species);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecies", new { id = species.Id }, species);
        }

        // DELETE: api/Species/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Species>> DeleteSpecies(int id)
        {
            var species = await _context.Specieses.FindAsync(id);
            if (species == null)
            {
                return NotFound();
            }

            _context.Specieses.Remove(species);
            await _context.SaveChangesAsync();

            return species;
        }

        private bool SpeciesExists(int id)
        {
            return _context.Specieses.Any(e => e.Id == id);
        }
    }
}
