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
    public class AdultoMayorMedicamentosController : ControllerBase
    {
        private readonly BDContexto _context;

        public AdultoMayorMedicamentosController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/AdultoMayorMedicamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdultoMayorMedicamento>>> GetAdultoMayorMedicamentos()
        {
            return await _context.AdultoMayorMedicamentos.ToListAsync();
        }

        // GET: api/AdultoMayorMedicamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdultoMayorMedicamento>> GetAdultoMayorMedicamento(int id)
        {
            var adultoMayorMedicamento = await _context.AdultoMayorMedicamentos.FindAsync(id);

            if (adultoMayorMedicamento == null)
            {
                return NotFound();
            }

            return adultoMayorMedicamento;
        }

        // PUT: api/AdultoMayorMedicamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdultoMayorMedicamento(int id, AdultoMayorMedicamento adultoMayorMedicamento)
        {
            if (id != adultoMayorMedicamento.AdultoMayorId)
            {
                return BadRequest();
            }

            _context.Entry(adultoMayorMedicamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdultoMayorMedicamentoExists(id))
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

        // POST: api/AdultoMayorMedicamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdultoMayorMedicamento>> PostAdultoMayorMedicamento(AdultoMayorMedicamento adultoMayorMedicamento)
        {
            _context.AdultoMayorMedicamentos.Add(adultoMayorMedicamento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdultoMayorMedicamentoExists(adultoMayorMedicamento.AdultoMayorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdultoMayorMedicamento", new { id = adultoMayorMedicamento.AdultoMayorId }, adultoMayorMedicamento);
        }

        // DELETE: api/AdultoMayorMedicamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdultoMayorMedicamento(int id)
        {
            var adultoMayorMedicamento = await _context.AdultoMayorMedicamentos.FindAsync(id);
            if (adultoMayorMedicamento == null)
            {
                return NotFound();
            }

            _context.AdultoMayorMedicamentos.Remove(adultoMayorMedicamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdultoMayorMedicamentoExists(int id)
        {
            return _context.AdultoMayorMedicamentos.Any(e => e.AdultoMayorId == id);
        }
    }
}
