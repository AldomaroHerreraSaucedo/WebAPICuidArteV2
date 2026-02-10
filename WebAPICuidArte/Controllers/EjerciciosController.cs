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
    public class EjerciciosController : ControllerBase
    {
        private readonly BDContexto _context;

        public EjerciciosController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/Ejercicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ejercicio>>> GetEjercicio()
        {
            return await _context.Ejercicio.ToListAsync();
        }

        // GET: api/Ejercicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ejercicio>> GetEjercicio(int id)
        {
            var ejercicio = await _context.Ejercicio.FindAsync(id);

            if (ejercicio == null)
            {
                return NotFound();
            }

            return ejercicio;
        }

        // PUT: api/Ejercicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEjercicio(int id, Ejercicio ejercicio)
        {
            if (id != ejercicio.EjercicioId)
            {
                return BadRequest();
            }

            _context.Entry(ejercicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EjercicioExists(id))
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

        // POST: api/Ejercicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ejercicio>> PostEjercicio(Ejercicio ejercicio)
        {
            _context.Ejercicio.Add(ejercicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEjercicio", new { id = ejercicio.EjercicioId }, ejercicio);
        }

        // DELETE: api/Ejercicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEjercicio(int id)
        {
            var ejercicio = await _context.Ejercicio.FindAsync(id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            _context.Ejercicio.Remove(ejercicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EjercicioExists(int id)
        {
            return _context.Ejercicio.Any(e => e.EjercicioId == id);
        }

        [HttpGet("adultomayor/{adultoMayorId}")]
        public async Task<ActionResult<IEnumerable<Ejercicio>>> GetEjerciciosFavoritos(int adultoMayorId)
        {
            var favoritos = await _context.Ejercicio
                .Where(e => e.AdultoMayorId == adultoMayorId && e.IsFavorito == true)
                .ToListAsync();

            if (favoritos == null || favoritos.Count == 0)
            {
                return Ok(new List<Ejercicio>());
            }

            return Ok(favoritos);
        }

    }
}
