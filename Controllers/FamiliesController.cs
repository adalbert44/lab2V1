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
    public class FamiliesController : ControllerBase
    {
        private readonly Lab2LibraryContext _context;

        public FamiliesController(Lab2LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Families
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Family>>> GetFamlies()
        {
            return await _context.Famlies.ToListAsync();
        }

        // GET: api/Families/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Family>> GetFamily(int id)
        {
            var family = await _context.Famlies.FindAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            return family;
        }

        // PUT: api/Families/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamily(int id, Family family)
        {
            if (id != family.Id)
            {
                return BadRequest();
            }

            _context.Entry(family).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(id))
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

        // POST: api/Families
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Family>> PostFamily(Family family)
        {
            _context.Famlies.Add(family);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamily", new { id = family.Id }, family);
        }

        // DELETE: api/Families/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Family>> DeleteFamily(int id)
        {
            var family = await _context.Famlies.FindAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            var genuses = await _context.Genuses.Where(s => s.FamilyID == id).ToListAsync();
            foreach(var genus in genuses) 
            {
                var specieses = await _context.Specieses.Where(s => s.GenusID == genus.Id).ToListAsync();
                foreach (var species in specieses)
                {
                    _context.Specieses.Remove(species);
                }
                _context.Genuses.Remove(genus);
            }

            _context.Famlies.Remove(family);
            await _context.SaveChangesAsync();

            return family;
        }

        private bool FamilyExists(int id)
        {
            return _context.Famlies.Any(e => e.Id == id);
        }
    }
}
