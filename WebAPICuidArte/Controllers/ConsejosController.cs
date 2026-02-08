using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICuidArte.Data;
using WebAPICuidArte.Models;

namespace WebAPICuidArte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsejosController : ControllerBase
    {
        private readonly BDContexto _context;

        public ConsejosController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/Consejos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consejo>>> GetConsejos()
        {
            return await _context.Consejos.ToListAsync();
        }

        // GET: api/Consejos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consejo>> GetConsejo(int id)
        {
            var consejo = await _context.Consejos.FindAsync(id);

            if (consejo == null)
            {
                return NotFound();
            }

            return consejo;
        }

        // PUT: api/Consejos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsejo(int id, Consejo consejo)
        {
            if (id != consejo.ConsejoId)
            {
                return BadRequest();
            }

            _context.Entry(consejo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsejoExists(id))
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

        // POST: api/Consejos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consejo>> PostConsejo(Consejo consejo)
        {
            _context.Consejos.Add(consejo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsejo", new { id = consejo.ConsejoId }, consejo);
        }

        // DELETE: api/Consejos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsejo(int id)
        {
            var consejo = await _context.Consejos.FindAsync(id);
            if (consejo == null)
            {
                return NotFound();
            }

            _context.Consejos.Remove(consejo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsejoExists(int id)
        {
            return _context.Consejos.Any(e => e.ConsejoId == id);
        }
    }
}
