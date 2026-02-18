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
    public class LecturasController : ControllerBase
    {
        private readonly BDContexto _context;

        public LecturasController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/Lecturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lectura>>> GetLecturas()
        {
            return await _context.Lecturas
                .Include(l => l.Avances)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Lecturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lectura>> GetLectura(int id)
        {
            var lectura = await _context.Lecturas.FindAsync(id);

            if (lectura == null)
            {
                return NotFound();
            }

            return lectura;
        }

        // PUT: api/Lecturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLectura(int id, Lectura lectura)
        {
            if (id != lectura.LecturaId)
            {
                return BadRequest();
            }

            _context.Entry(lectura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturaExists(id))
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

        // POST: api/Lecturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lectura>> PostLectura(Lectura lectura)
        {
            _context.Lecturas.Add(lectura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLectura", new { id = lectura.LecturaId }, lectura);
        }

        // DELETE: api/Lecturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLectura(int id)
        {
            var lectura = await _context.Lecturas.FindAsync(id);
            if (lectura == null)
            {
                return NotFound();
            }

            _context.Lecturas.Remove(lectura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturaExists(int id)
        {
            return _context.Lecturas.Any(e => e.LecturaId == id);
        }

        [HttpPost("{lecturaId}/avances")]
        public async Task<IActionResult> AgregarAvance(int lecturaId, AvanceLectura avance)
        {
            var lectura = await _context.Lecturas
                .Include(l => l.Avances)
                .FirstOrDefaultAsync(l => l.LecturaId == lecturaId);

            if (lectura == null)
                return NotFound("Lectura no encontrada");

            avance.LecturaId = lecturaId;
            lectura.Avances.Add(avance);

            lectura.UltimaPaginaLeida += avance.PaginasLeidas;

            if (lectura.UltimaPaginaLeida > lectura.PaginasTotales)
                lectura.UltimaPaginaLeida = lectura.PaginasTotales;

            await _context.SaveChangesAsync();

            return Ok(lectura);
        }

    }
}
