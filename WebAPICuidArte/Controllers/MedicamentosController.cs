﻿using System;
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
            return await _context.Medicamentos.ToListAsync();
        }

        // GET: api/Medicamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return medicamento;
        }

        // PUT: api/Medicamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
        {
            if (id != medicamento.MedicamentoId)
            {
                return BadRequest();
            }

            _context.Entry(medicamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoExists(id))
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

        // POST: api/Medicamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
        {
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicamento", new { id = medicamento.MedicamentoId }, medicamento);
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
