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
    public class InformacionEmergenciasController : ControllerBase
    {
        private readonly BDContexto _context;

        public InformacionEmergenciasController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/InformacionEmergencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InformacionEmergencia>>> GetInformacionEmergencias()
        {
            return await _context.InformacionEmergencias.ToListAsync();
        }

        // GET: api/InformacionEmergencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformacionEmergencia>> GetInformacionEmergencia(int id)
        {
            var informacionEmergencia = await _context.InformacionEmergencias.FindAsync(id);

            if (informacionEmergencia == null)
            {
                return NotFound();
            }

            return informacionEmergencia;
        }

        // PUT: api/InformacionEmergencias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformacionEmergencia(int id, InformacionEmergencia informacionEmergencia)
        {
            if (id != informacionEmergencia.IdInfoEmergencia)
            {
                return BadRequest();
            }

            _context.Entry(informacionEmergencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformacionEmergenciaExists(id))
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

        // POST: api/InformacionEmergencias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InformacionEmergencia>> PostInformacionEmergencia(InformacionEmergencia informacionEmergencia)
        {
            _context.InformacionEmergencias.Add(informacionEmergencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformacionEmergencia", new { id = informacionEmergencia.IdInfoEmergencia }, informacionEmergencia);
        }

        // DELETE: api/InformacionEmergencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformacionEmergencia(int id)
        {
            var informacionEmergencia = await _context.InformacionEmergencias.FindAsync(id);
            if (informacionEmergencia == null)
            {
                return NotFound();
            }

            _context.InformacionEmergencias.Remove(informacionEmergencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InformacionEmergenciaExists(int id)
        {
            return _context.InformacionEmergencias.Any(e => e.IdInfoEmergencia == id);
        }
    }
}
