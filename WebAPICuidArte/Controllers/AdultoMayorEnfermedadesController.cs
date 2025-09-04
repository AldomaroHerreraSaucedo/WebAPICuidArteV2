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
    public class AdultoMayorEnfermedadesController : ControllerBase
    {
        private readonly BDContexto _context;

        public AdultoMayorEnfermedadesController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/AdultoMayorEnfermedades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdultoMayorEnfermedad>>> GetAdultoMayorEnfermedades()
        {
            return await _context.AdultoMayorEnfermedades.ToListAsync();
        }

        // GET: api/AdultoMayorEnfermedades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdultoMayorEnfermedad>> GetAdultoMayorEnfermedad(int id)
        {
            var adultoMayorEnfermedad = await _context.AdultoMayorEnfermedades.FindAsync(id);

            if (adultoMayorEnfermedad == null)
            {
                return NotFound();
            }

            return adultoMayorEnfermedad;
        }

        // PUT: api/AdultoMayorEnfermedades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdultoMayorEnfermedad(int id, AdultoMayorEnfermedad adultoMayorEnfermedad)
        {
            if (id != adultoMayorEnfermedad.AdultoMayorId)
            {
                return BadRequest();
            }

            _context.Entry(adultoMayorEnfermedad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdultoMayorEnfermedadExists(id))
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

        // POST: api/AdultoMayorEnfermedades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdultoMayorEnfermedad>> PostAdultoMayorEnfermedad(AdultoMayorEnfermedad adultoMayorEnfermedad)
        {
            _context.AdultoMayorEnfermedades.Add(adultoMayorEnfermedad);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdultoMayorEnfermedadExists(adultoMayorEnfermedad.AdultoMayorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdultoMayorEnfermedad", new { id = adultoMayorEnfermedad.AdultoMayorId }, adultoMayorEnfermedad);
        }

        // DELETE: api/AdultoMayorEnfermedades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdultoMayorEnfermedad(int id)
        {
            var adultoMayorEnfermedad = await _context.AdultoMayorEnfermedades.FindAsync(id);
            if (adultoMayorEnfermedad == null)
            {
                return NotFound();
            }

            _context.AdultoMayorEnfermedades.Remove(adultoMayorEnfermedad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdultoMayorEnfermedadExists(int id)
        {
            return _context.AdultoMayorEnfermedades.Any(e => e.AdultoMayorId == id);
        }
    }
}
