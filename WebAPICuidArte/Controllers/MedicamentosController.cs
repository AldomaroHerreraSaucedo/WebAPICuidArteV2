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
    public class MedicamentosController : ControllerBase
    {
        private readonly BDContexto _context;

        public MedicamentosController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/Medicamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos()
        {
            return await _context.Medicamentos
                .Include(m => m.Horarios)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Medicamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos
                .Include(m => m.Horarios)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MedicamentoId == id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return medicamento;
        }

        // PUT: api/Medicamentos/5
        // Reemplaza cabecera y sincroniza horarios:
        // - actualiza campos principales
        // - agrega horarios nuevos
        // - elimina horarios que ya no vienen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
        {
            if (DateTime.Parse(medicamento.FechaFin) < DateTime.Parse(medicamento.FechaInicio))
                return BadRequest("Fecha fin no puede ser menor que fecha inicio.");

            if (id != medicamento.MedicamentoId) return BadRequest();

            var dbMedicamento = await _context.Medicamentos
                .Include(m => m.Horarios)
                .FirstOrDefaultAsync(m => m.MedicamentoId == id);

            if (dbMedicamento == null) return NotFound();

           
            // Validación: al menos 1 horario
            if (medicamento.Horarios == null || !medicamento.Horarios.Any())
                return BadRequest("Debe enviar al menos 1 horario.");

            foreach (var h in medicamento.Horarios)
            {
                h.MedicamentoHorarioId = 0;
                h.MedicamentoId = 0;
            }

            // Actualizar cabecera
            dbMedicamento.NombreMedicamento = medicamento.NombreMedicamento;
            dbMedicamento.Dosis = medicamento.Dosis;
            dbMedicamento.FechaInicio = medicamento.FechaInicio;
            dbMedicamento.FechaFin = medicamento.FechaFin;
            dbMedicamento.Descripcion = medicamento.Descripcion;
            dbMedicamento.DiasSemana = medicamento.DiasSemana;
            dbMedicamento.Estado = medicamento.Estado;

            // Normalizar horarios del input (solo Hora, sin ids)
            var horasInput = medicamento.Horarios
                .Where(h => h != null && !string.IsNullOrWhiteSpace(h.Hora))
                .Select(h => h.Hora.Trim().ToUpperInvariant())
                .Distinct()
                .ToList();

            if (!horasInput.Any())
                return BadRequest("Debe enviar al menos 1 horario válido.");

            // Horas existentes en DB
            var horasDb = dbMedicamento.Horarios.Select(h => h.Hora).ToList();

            // Eliminar los que ya no están
            var toRemove = dbMedicamento.Horarios.Where(h => !horasInput.Contains(h.Hora)).ToList();
            _context.MedicamentoHorarios.RemoveRange(toRemove);

            // Agregar los nuevos
            var toAdd = horasInput
                .Where(h => !horasDb.Contains(h))
                .Select(h => new MedicamentoHorario
                {
                    MedicamentoId = id,
                    Hora = h
                })
                .ToList();

            _context.MedicamentoHorarios.AddRange(toAdd);

            await _context.SaveChangesAsync();
            return NoContent();
            //if (id != medicamento.MedicamentoId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(medicamento).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MedicamentoExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/Medicamentos
        // Body esperado:
        // {
        //   "nombreMedicamento":"Paracetamol",
        //   "dosis":"500 mg",
        //   "fechaInicio":"02/07/2026",
        //   "fechaFin":"07/07/2026",
        //   "descripcion":"...",
        //   "diasSemana":"Lunes,Martes",
        //   "estado":1,
        //   "horarios":[ {"hora":"08:00 AM"}, {"hora":"12:00 PM"} ]
        // }
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
        {
            if (DateTime.Parse(medicamento.FechaFin) < DateTime.Parse(medicamento.FechaInicio))
                return BadRequest("Fecha fin no puede ser menor que fecha inicio.");

            // Validación: al menos 1 horario
            if (medicamento.Horarios == null || !medicamento.Horarios.Any())
                return BadRequest("Debe enviar al menos 1 horario.");

            // Normalizar/filtrar horarios inválidos y duplicados por Hora
            medicamento.Horarios = medicamento.Horarios
                .Where(h => h != null && !string.IsNullOrWhiteSpace(h.Hora))
                .GroupBy(h => h.Hora.Trim().ToUpperInvariant())
                .Select(g => new MedicamentoHorario { Hora = g.Key })
                .ToList();

            if (!medicamento.Horarios.Any())
                return BadRequest("Debe enviar al menos 1 horario válido.");

            // IMPORTANTE: evitar que te manden ids en POST
            medicamento.MedicamentoId = 0;
            foreach (var h in medicamento.Horarios)
            {
                h.MedicamentoHorarioId = 0;
                h.MedicamentoId = 0; // lo asignamos luego
            }

            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync(); // genera MedicamentoId

            // Asignar FK a cada horario y guardar
            foreach (var h in medicamento.Horarios)
                h.MedicamentoId = medicamento.MedicamentoId;

            _context.MedicamentoHorarios.AddRange(medicamento.Horarios);
            await _context.SaveChangesAsync();

            // devolver con horarios ya persistidos
            var result = await _context.Medicamentos
                .Include(m => m.Horarios)
                .AsNoTracking()
                .FirstAsync(m => m.MedicamentoId == medicamento.MedicamentoId);

            return CreatedAtAction(nameof(GetMedicamento), new { id = result.MedicamentoId }, result);

            //_context.Medicamentos.Add(medicamento);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetMedicamento", new { id = medicamento.MedicamentoId }, medicamento);
        }

        // DELETE: api/Medicamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        private bool MedicamentoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.MedicamentoId == id);
        }
    }
}
