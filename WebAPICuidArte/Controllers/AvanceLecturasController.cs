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
    public class AvanceLecturasController : ControllerBase
    {
        private readonly BDContexto _context;

        public AvanceLecturasController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/AvanceLecturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvanceLectura>>> GetAvanceLectura()
        {
            return await _context.AvanceLectura.ToListAsync();
        }

        // GET: api/AvanceLecturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AvanceLectura>> GetAvanceLectura(int id)
        {
            var avanceLectura = await _context.AvanceLectura.FindAsync(id);

            if (avanceLectura == null)
            {
                return NotFound();
            }

            return avanceLectura;
        }

        // PUT: api/AvanceLecturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvanceLectura(int id, AvanceLectura avanceLectura)
        {
            if (id != avanceLectura.AvanceId)
            {
                return BadRequest();
            }

            _context.Entry(avanceLectura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvanceLecturaExists(id))
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

        // POST: api/AvanceLecturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AvanceLectura>> PostAvanceLectura(AvanceLectura avanceLectura)
        {
            _context.AvanceLectura.Add(avanceLectura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvanceLectura", new { id = avanceLectura.AvanceId }, avanceLectura);
        }

        // DELETE: api/AvanceLecturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvanceLectura(int id)
        {
            var avanceLectura = await _context.AvanceLectura.FindAsync(id);
            if (avanceLectura == null)
            {
                return NotFound();
            }

            _context.AvanceLectura.Remove(avanceLectura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AvanceLecturaExists(int id)
        {
            return _context.AvanceLectura.Any(e => e.AvanceId == id);
        }
    }
}
