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
    public class ClassesController : ControllerBase
    {
        private readonly Lab2LibraryContext _context;

        public ClassesController(Lab2LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);

            if (@class == null)
            {
                return NotFound();
            }

            return @class;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class @class)
        {
            if (id != @class.Id)
            {
                return BadRequest();
            }

            _context.Entry(@class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @class.Id }, @class);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Class>> DeleteClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.Where(s => s.ClassID == id).ToListAsync();
            foreach(var order in orders)
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
            await _context.SaveChangesAsync();

            return @class;
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
