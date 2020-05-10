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
    public class PhylaController : ControllerBase
    {
        private readonly Lab2LibraryContext _context;

        public PhylaController(Lab2LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Phyla
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phylum>>> GetPhylumes()
        {
            return await _context.Phylumes.ToListAsync();
        }

        // GET: api/Phyla/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phylum>> GetPhylum(int id)
        {
            var phylum = await _context.Phylumes.FindAsync(id);

            if (phylum == null)
            {
                return NotFound();
            }

            return phylum;
        }

        // PUT: api/Phyla/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhylum(int id, Phylum phylum)
        {
            if (id != phylum.Id)
            {
                return BadRequest();
            }

            _context.Entry(phylum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhylumExists(id))
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

        // POST: api/Phyla
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Phylum>> PostPhylum(Phylum phylum)
        {
            _context.Phylumes.Add(phylum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhylum", new { id = phylum.Id }, phylum);
        }

        // DELETE: api/Phyla/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Phylum>> DeletePhylum(int id)
        {
            var phylum = await _context.Phylumes.FindAsync(id);
            if (phylum == null)
            {
                return NotFound();
            }

            var classes = await _context.Classes.Where(s => s.PhylumID == id).ToListAsync();
            foreach(var @class in classes) {
                var orders = await _context.Orders.Where(s => s.ClassID == @class.Id).ToListAsync();
                foreach (var order in orders)
                {
                    var families = await _context.Famlies.Where(s => s.OrderID == order.Id).ToListAsync();
                    foreach (var family in families)
                    {
                        var genuses = await _context.Genuses.Where(s => s.FamilyID == family.Id).ToListAsync();
                        foreach (var genus in genuses)
                        {
                            var specieses = await _context.Specieses.Where(s => s.GenusID == genus.Id).ToListAsync();
                            foreach (var species in specieses)
                            {
                                _context.Specieses.Remove(species);
                            }
                            _context.Genuses.Remove(genus);
                        }
                        _context.Famlies.Remove(family);
                    }
                    _context.Orders.Remove(order);
                }
                _context.Classes.Remove(@class);
            }

            _context.Phylumes.Remove(phylum);
            await _context.SaveChangesAsync();

            return phylum;
        }

        private bool PhylumExists(int id)
        {
            return _context.Phylumes.Any(e => e.Id == id);
        }
    }
}
