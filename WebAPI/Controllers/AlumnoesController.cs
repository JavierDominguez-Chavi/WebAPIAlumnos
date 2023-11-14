using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoesController : ControllerBase
    {
        private readonly WebAPIContext _context;

        public AlumnoesController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Alumnoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnoDTO>>> GetAlumno()
        { 
            return await _context.Alumno.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/Alumnoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlumnoDTO>> GetAlumno(int id)
        {
            var alumno = await _context.Alumno.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return ItemToDTO(alumno);
        }

        // PUT: api/Alumnoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, AlumnoDTO alumnoDTO)
        {
            if (id != alumnoDTO.Id)
            {
                return BadRequest();
            }

            var alumno = await _context.Alumno.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            alumno.Nombre = alumnoDTO.Nombre;
            alumno.EstaAprobado = alumnoDTO.EstaAprobado;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AlumnoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Alumnoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(AlumnoDTO alumnoDTO)
        {
            var alumno = new Alumno
            {
                Nombre = alumnoDTO.Nombre,
                EstaAprobado = alumnoDTO.EstaAprobado,
                Secreto = new Guid().ToString()
            };

            _context.Alumno.Add(alumno);
            await _context.SaveChangesAsync();  

            return CreatedAtAction("GetAlumno", new { id = alumno.Id }, ItemToDTO(alumno));
        }

        // DELETE: api/Alumnoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            if (_context.Alumno == null)
            {
                return NotFound();
            }
            var alumno = await _context.Alumno.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumno.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlumnoExists(int id)
        {
            return (_context.Alumno?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static AlumnoDTO ItemToDTO(Alumno alumno) => new AlumnoDTO
        {
            Id = alumno.Id,
            Nombre = alumno.Nombre,
            EstaAprobado = alumno.EstaAprobado
        };

    }
}
