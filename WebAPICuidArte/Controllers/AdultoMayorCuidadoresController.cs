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
    public class AdultoMayorCuidadoresController : ControllerBase
    {
        private readonly BDContexto _context;

        public AdultoMayorCuidadoresController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/AdultoMayorCuidadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdultoMayorCuidador>>> GetAdultoMayorCuidadores()
        {
            return await _context.AdultoMayorCuidadores.ToListAsync();
        }

        // GET: api/AdultoMayorCuidadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdultoMayorCuidador>> GetAdultoMayorCuidador(int id)
        {
            var adultoMayorCuidador = await _context.AdultoMayorCuidadores.FindAsync(id);

            if (adultoMayorCuidador == null)
            {
                return NotFound();
            }

            return adultoMayorCuidador;
        }

        // PUT: api/AdultoMayorCuidadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdultoMayorCuidador(int id, AdultoMayorCuidador adultoMayorCuidador)
        {
            if (id != adultoMayorCuidador.AdultoMayorId)
            {
                return BadRequest();
            }

            _context.Entry(adultoMayorCuidador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdultoMayorCuidadorExists(id))
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

        // POST: api/AdultoMayorCuidadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdultoMayorCuidador>> PostAdultoMayorCuidador(AdultoMayorCuidador adultoMayorCuidador)
        {
            _context.AdultoMayorCuidadores.Add(adultoMayorCuidador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdultoMayorCuidadorExists(adultoMayorCuidador.AdultoMayorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdultoMayorCuidador", new { id = adultoMayorCuidador.AdultoMayorId }, adultoMayorCuidador);
        }

        // DELETE: api/AdultoMayorCuidadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdultoMayorCuidador(int id)
        {
            var adultoMayorCuidador = await _context.AdultoMayorCuidadores.FindAsync(id);
            if (adultoMayorCuidador == null)
            {
                return NotFound();
            }

            _context.AdultoMayorCuidadores.Remove(adultoMayorCuidador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdultoMayorCuidadorExists(int id)
        {
            return _context.AdultoMayorCuidadores.Any(e => e.AdultoMayorId == id);
        }
    }
}
