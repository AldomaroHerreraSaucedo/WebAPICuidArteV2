using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICuidArte.Data;
using WebAPICuidArte.Models;

namespace WebAPICuidArte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionEmergenciaController : Controller
    {
        private readonly BDContexto _context;

        public InformacionEmergenciaController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/InformacionEmergencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InformacionEmergencia>>> GetInformacionEmergencias()
        {
            return await _context.InformacionEmergencias
                .AsNoTracking()
                .ToListAsync();
        }


        // GET: api/InformacionEmergencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformacionEmergencia>> GetInformacionEmergencia(int id)
        {
            var informacionEmergencia = await _context.InformacionEmergencias
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.IdInfoEmergencia == id);

            if (informacionEmergencia == null)
            {
                return NotFound();
            }

            return informacionEmergencia;
        }

        // GET: api/InformacionEmergencia/adultomayor/5
        [HttpGet("adultomayor/{idAdultoMayor}")]
        public async Task<ActionResult<InformacionEmergencia>> GetInformacionEmergenciaPorAdultoMayor(int idAdultoMayor)
        {
            var informacionEmergencia = await _context.InformacionEmergencias
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.IdAdultoMayor == idAdultoMayor);

            if (informacionEmergencia == null)
            {
                return NotFound(new { mensaje = "No se encontró información de emergencia para este adulto mayor." });
            }

            return informacionEmergencia;
        }

        // PUT: api/InformacionEmergencia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformacionEmergencia(int id, InformacionEmergencia informacionEmergencia)
        {
            if (id != informacionEmergencia.IdInfoEmergencia)
            {
                return BadRequest("El ID no coincide.");
            }

            // Validar que el adulto mayor existe
            var adultoMayorExiste = await _context.AdultosMayores
                .AnyAsync(a => a.AdultoMayorId == informacionEmergencia.IdAdultoMayor);

            if (!adultoMayorExiste)
            {
                return BadRequest("El Adulto Mayor no existe.");
            }

            // Validar tipos de sangre válidos
            var tiposSangreValidos = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            if (!tiposSangreValidos.Contains(informacionEmergencia.TipoSangre))
            {
                return BadRequest("Tipo de sangre no válido. Debe ser: A+, A-, B+, B-, AB+, AB-, O+ u O-");
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

        // POST: api/InformacionEmergencia
        [HttpPost]
        public async Task<ActionResult<InformacionEmergencia>> PostInformacionEmergencia(InformacionEmergencia informacionEmergencia)
        {
            // Validar que el adulto mayor existe
            var adultoMayorExiste = await _context.AdultosMayores
                .AnyAsync(a => a.AdultoMayorId == informacionEmergencia.IdAdultoMayor);

            if (!adultoMayorExiste)
            {
                return BadRequest("El Adulto Mayor no existe.");
            }

            // Validar que no exista ya información de emergencia para este adulto mayor
            var yaExiste = await _context.InformacionEmergencias
                .AnyAsync(i => i.IdAdultoMayor == informacionEmergencia.IdAdultoMayor);

            if (yaExiste)
            {
                return BadRequest("Ya existe información de emergencia para este adulto mayor. Use PUT para actualizar.");
            }

            // Validar tipos de sangre válidos
            var tiposSangreValidos = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            if (!tiposSangreValidos.Contains(informacionEmergencia.TipoSangre))
            {
                return BadRequest("Tipo de sangre no válido. Debe ser: A+, A-, B+, B-, AB+, AB-, O+ u O-");
            }

            // Evitar que envíen ID en POST
            informacionEmergencia.IdInfoEmergencia = 0;

            _context.InformacionEmergencias.Add(informacionEmergencia);
            await _context.SaveChangesAsync();

            // Devolver con ID generado
            var resultado = await _context.InformacionEmergencias
                .AsNoTracking()
                .FirstAsync(i => i.IdInfoEmergencia == informacionEmergencia.IdInfoEmergencia);

            return CreatedAtAction(
                nameof(GetInformacionEmergencia),
                new { id = resultado.IdInfoEmergencia },
                resultado
            );
        }

        // DELETE: api/InformacionEmergencia/5
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
