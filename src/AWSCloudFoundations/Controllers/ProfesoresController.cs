using AWSCloudFoundations.Data;
using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Helpers.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AWSCloudFoundations.Controllers
{

    [ApiController]
    [Route("profesores")]
    public class ProfesoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfesoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /profesores
        [HttpGet]
        public async Task<IActionResult> GetProfesores()
        {
            var profesores = await _context.Profesores.ToListAsync();
            var response = new ApiResponse<List<ProfesorResponseDTO>>(profesores.Select(p => new ProfesorResponseDTO
            {
                Id = p.Id,
                NumeroEmpleado = p.NumeroEmpleado,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                HorasClase = p.HorasClase
            }).ToList(), 200);

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        // GET /profesores/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProfesorById(int id)
        {
            var profesor = await _context.Profesores.FirstOrDefaultAsync(p => p.Id == id);
            if (profesor == null)
            {
                var response = new ApiResponse<string>("Profesor no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            var responseProfesor = new ProfesorResponseDTO
            {
                Id = profesor.Id,
                NumeroEmpleado = profesor.NumeroEmpleado,
                Nombres = profesor.Nombres,
                Apellidos = profesor.Apellidos,
                HorasClase = profesor.HorasClase
            };

            return Ok(responseProfesor);
        }

        // POST /profesores
        [HttpPost]
        public async Task<IActionResult> PostProfesor([FromBody] CreateProfesorDTO profesorDto)
        {
            var profesor = new Profesor
            {
                NumeroEmpleado = profesorDto.NumeroEmpleado,
                Nombres = profesorDto.Nombres,
                Apellidos = profesorDto.Apellidos,
                HorasClase = profesorDto.HorasClase
            };

            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();

            var response = new ProfesorResponseDTO
            {
                Id = profesor.Id,
                NumeroEmpleado = profesor.NumeroEmpleado,
                Nombres = profesor.Nombres,
                Apellidos = profesor.Apellidos,
                HorasClase = profesor.HorasClase
            };

            return CreatedAtAction(nameof(GetProfesorById), new { id = profesor.Id }, response);
        }

        // PUT /profesores/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProfesor(int id, [FromBody] UpdateProfesorDTO profesorUpdateDto)
        {
            var profesor = await _context.Profesores.FirstOrDefaultAsync(p => p.Id == id);
            if (profesor == null)
            {
                var resp = new ApiResponse<string>("Profesor no encontrado", 404);
                return new ObjectResult(resp) { StatusCode = resp.StatusCode };
            }

            if (profesorUpdateDto == null) return BadRequest();

            profesor.Nombres = profesorUpdateDto.Nombres;
            profesor.Apellidos = profesorUpdateDto.Apellidos;
            profesor.HorasClase = profesorUpdateDto.HorasClase;

            await _context.SaveChangesAsync();

            var response = new ApiResponse<ProfesorResponseDTO>(new ProfesorResponseDTO
            {
                Id = profesor.Id,
                NumeroEmpleado = profesor.NumeroEmpleado,
                Nombres = profesor.Nombres,
                Apellidos = profesor.Apellidos,
                HorasClase = profesor.HorasClase
            }, 200);

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        // DELETE /profesores/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            var profesor = await _context.Profesores.FirstOrDefaultAsync(p => p.Id == id);
            if (profesor == null)
            {
                var resp = new ApiResponse<string>("Profesor no encontrado", 404);
                return new ObjectResult(resp) { StatusCode = resp.StatusCode };
            }

            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();

            var response = new ApiResponse<string>("Profesor eliminado exitosamente", 200);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }

    public class Profesor
    {
        [Key]
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "El campo 'numeroEmpleado' debe ser un número entero mayor a 0")]
        public int NumeroEmpleado { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El campo 'nombres' debe tener entre 1 y 100 caracteres.")]
        public string Nombres { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El campo 'nombres' debe tener entre 1 y 100 caracteres.")]
        public string Apellidos { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "El campo 'horasClase' debe ser un número entero mayor a 0")]
        public int HorasClase { get; set; }
    }

    public class CreateProfesorDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "El campo 'numeroEmpleado' debe ser un número entero mayor a 0")]
        public int NumeroEmpleado { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El campo 'nombres' debe tener entre 1 y 100 caracteres.")]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El campo 'apellidos' debe tener entre 1 y 100 caracteres.")]
        public string Apellidos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El campo 'horasClase' debe ser un número entero mayor a 0")]
        public int HorasClase { get; set; }
    }

    public class UpdateProfesorDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El campo 'nombres' debe tener entre 1 y 100 caracteres.")]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El campo 'apellidos' debe tener entre 1 y 100 caracteres.")]
        public string Apellidos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El campo 'horasClase' debe ser un número entero mayor a 0")]
        public int HorasClase { get; set; }
    }

    public class ProfesorResponseDTO
    {
        public int Id { get; set; }
        public int NumeroEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int HorasClase { get; set; }
    }

}
