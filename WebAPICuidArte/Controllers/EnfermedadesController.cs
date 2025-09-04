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
    public class EnfermedadesController : ControllerBase
    {
        private readonly BDContexto _context;

        public EnfermedadesController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/Enfermedades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enfermedad>>> GetEnfermedades()
        {
            return await _context.Enfermedades.ToListAsync();
        }

        // GET: api/Enfermedades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enfermedad>> GetEnfermedad(int id)
        {
            var enfermedad = await _context.Enfermedades.FindAsync(id);

            if (enfermedad == null)
            {
                return NotFound();
            }

            return enfermedad;
        }

        // PUT: api/Enfermedades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnfermedad(int id, Enfermedad enfermedad)
        {
            if (id != enfermedad.EnfermedadId)
            {
                return BadRequest();
            }

            _context.Entry(enfermedad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnfermedadExists(id))
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

        // POST: api/Enfermedades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enfermedad>> PostEnfermedad(Enfermedad enfermedad)
        {
            _context.Enfermedades.Add(enfermedad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnfermedad", new { id = enfermedad.EnfermedadId }, enfermedad);
        }

        // DELETE: api/Enfermedades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnfermedad(int id)
        {
            var enfermedad = await _context.Enfermedades.FindAsync(id);
            if (enfermedad == null)
            {
                return NotFound();
            }

            _context.Enfermedades.Remove(enfermedad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnfermedadExists(int id)
        {
            return _context.Enfermedades.Any(e => e.EnfermedadId == id);
        }
    }
}
