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
    public class AdultosMayoresController : ControllerBase
    {
        private readonly BDContexto _context;

        public AdultosMayoresController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/AdultosMayores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdultoMayor>>> GetAdultosMayores()
        {
            return await _context.AdultosMayores.ToListAsync();
        }

        // GET: api/AdultosMayores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdultoMayor>> GetAdultoMayor(int id)
        {
            var adultoMayor = await _context.AdultosMayores.FindAsync(id);

            if (adultoMayor == null)
            {
                return NotFound();
            }

            return adultoMayor;
        }

        // PUT: api/AdultosMayores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdultoMayor(int id, AdultoMayor adultoMayor)
        {
            if (id != adultoMayor.AdultoMayorId)
            {
                return BadRequest();
            }

            var adultoMayorBD = await _context.AdultosMayores.FindAsync(id);

            if (adultoMayorBD.Correo != adultoMayor.Correo)
            {
                string correo = adultoMayor.Correo.Trim().ToLower();
                bool correoExistente = _context.Cuidadores.Any(c => c.Correo.ToLower() == correo) || _context.AdultosMayores.Any(a => a.Correo.ToLower() == correo);
                if (correoExistente)
                    return Conflict("El correo ya está registrado.");
            }

            // Desvincular la entidad previamente rastreada
            _context.Entry(adultoMayorBD).State = EntityState.Detached;

            _context.Entry(adultoMayor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdultoMayorExists(id))
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

        // POST: api/AdultosMayores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdultoMayor>> PostAdultoMayor(AdultoMayor adultoMayor)
        {
            string correo = adultoMayor.Correo.Trim().ToLower();
            bool correoExistente = _context.Cuidadores.Any(c => c.Correo.ToLower() == correo) || _context.AdultosMayores.Any(a => a.Correo.ToLower() == correo);
            if (correoExistente)
                return Conflict("El correo ya está registrado.");

            adultoMayor.Correo = correo;

            _context.AdultosMayores.Add(adultoMayor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdultoMayor", new { id = adultoMayor.AdultoMayorId }, adultoMayor);
        }

        // DELETE: api/AdultosMayores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdultoMayor(int id)
        {
            var adultoMayor = await _context.AdultosMayores.FindAsync(id);
            if (adultoMayor == null)
            {
                return NotFound();
            }

            _context.AdultosMayores.Remove(adultoMayor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdultoMayorExists(int id)
        {
            return _context.AdultosMayores.Any(e => e.AdultoMayorId == id);
        }
    }
}
