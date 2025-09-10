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
    public class Lectura2Controller : ControllerBase
    {
        private readonly BDContexto _context;

        public Lectura2Controller(BDContexto context)
        {
            _context = context;
        }

        // GET: api/Lectura2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lectura2>>> GetLectura2()
        {
            return await _context.Lectura2.ToListAsync();
        }

        // GET: api/Lectura2/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lectura2>> GetLectura2(int id)
        {
            var lectura2 = await _context.Lectura2.FindAsync(id);

            if (lectura2 == null)
            {
                return NotFound();
            }

            return lectura2;
        }

        // PUT: api/Lectura2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLectura2(int id, Lectura2 lectura2)
        {
            if (id != lectura2.LecturaId)
            {
                return BadRequest();
            }

            _context.Entry(lectura2).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lectura2Exists(id))
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

        // POST: api/Lectura2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lectura2>> PostLectura2(Lectura2 lectura2)
        {
            _context.Lectura2.Add(lectura2);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLectura2", new { id = lectura2.LecturaId }, lectura2);
        }

        // DELETE: api/Lectura2/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLectura2(int id)
        {
            var lectura2 = await _context.Lectura2.FindAsync(id);
            if (lectura2 == null)
            {
                return NotFound();
            }

            _context.Lectura2.Remove(lectura2);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Lectura2Exists(int id)
        {
            return _context.Lectura2.Any(e => e.LecturaId == id);
        }
    }
}
