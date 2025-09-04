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
    public class CuidadoresController : ControllerBase
    {
        private readonly BDContexto _context;

        public CuidadoresController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/Cuidadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuidador>>> GetCuidadores()
        {
            return await _context.Cuidadores.ToListAsync();
        }

        // GET: api/Cuidadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuidador>> GetCuidador(int id)
        {
            var cuidador = await _context.Cuidadores.FindAsync(id);

            if (cuidador == null)
            {
                return NotFound();
            }

            return cuidador;
        }

        // PUT: api/Cuidadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuidador(int id, Cuidador cuidador)
        {
            if (id != cuidador.CuidadorId)
            {
                return BadRequest();
            }

            var cuidadorBD = await _context.Cuidadores.FindAsync(id);

            if (cuidadorBD.Correo != cuidador.Correo)
            {
                string correo = cuidador.Correo.Trim().ToLower();
                bool correoExistente = _context.Cuidadores.Any(c => c.Correo.ToLower() == correo) || _context.AdultosMayores.Any(a => a.Correo.ToLower() == correo);
                if (correoExistente)
                    return Conflict("El correo ya está registrado.");
            }

            // Desvincular la entidad previamente rastreada
            _context.Entry(cuidadorBD).State = EntityState.Detached;

            _context.Entry(cuidador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuidadorExists(id))
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

        // POST: api/Cuidadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuidador>> PostCuidador(Cuidador cuidador)
        {
            string correo = cuidador.Correo.Trim().ToLower();
            bool correoExistente = _context.Cuidadores.Any(c => c.Correo.ToLower() == correo) || _context.AdultosMayores.Any(a => a.Correo.ToLower() == correo);
            if (correoExistente)
                return Conflict("El correo ya está registrado.");

            cuidador.Correo = correo;

            _context.Cuidadores.Add(cuidador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuidador", new { id = cuidador.CuidadorId }, cuidador);
        }

        // DELETE: api/Cuidadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuidador(int id)
        {
            var cuidador = await _context.Cuidadores.FindAsync(id);
            if (cuidador == null)
            {
                return NotFound();
            }

            _context.Cuidadores.Remove(cuidador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuidadorExists(int id)
        {
            return _context.Cuidadores.Any(e => e.CuidadorId == id);
        }
    }
}
