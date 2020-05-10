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
    public class KingdomsController : ControllerBase
    {
        private readonly Lab2LibraryContext _context;

        public KingdomsController(Lab2LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Kingdoms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kingdom>>> GetKingdoms()
        {
            return await _context.Kingdoms.ToListAsync();
        }

        // GET: api/Kingdoms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kingdom>> GetKingdom(int id)
        {
            var kingdom = await _context.Kingdoms.FindAsync(id);

            if (kingdom == null)
            {
                return NotFound();
            }

            return kingdom;
        }

        // PUT: api/Kingdoms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKingdom(int id, Kingdom kingdom)
        {
            if (id != kingdom.Id)
            {
                return BadRequest();
            }

            _context.Entry(kingdom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KingdomExists(id))
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

        // POST: api/Kingdoms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Kingdom>> PostKingdom(Kingdom kingdom)
        {
            _context.Kingdoms.Add(kingdom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKingdom", new { id = kingdom.Id }, kingdom);
        }

        // DELETE: api/Kingdoms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kingdom>> DeleteKingdom(int id)
        {
            var kingdom = await _context.Kingdoms.FindAsync(id);
            if (kingdom == null)
            {
                return NotFound();
            }

            var phylumes = await _context.Phylumes.Where(s => s.KingdomID == id).ToListAsync();
            foreach (var phylum in phylumes)
            {
                var classes = await _context.Classes.Where(s => s.PhylumID == phylum.Id).ToListAsync();
                foreach (var @class in classes)
                {
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
            }

            _context.Kingdoms.Remove(kingdom);
            await _context.SaveChangesAsync();

            return kingdom;
        }

        private bool KingdomExists(int id)
        {
            return _context.Kingdoms.Any(e => e.Id == id);
        }
    }
}
