using AWSCloudFoundations.Helpers.Utils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AWSCloudFoundations.Controllers
{

    [ApiController]
    [Route("profesoresPruebas")]
    public class ProfesoresController : ControllerBase
    {
        private static List<Profesor> profesores = new();

        // GET /alumnos
        [HttpGet]
        public IActionResult GetProfesores()
        {
            var response = new ApiResponse<List<Profesor>>(profesores, 200);
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        // GET /alumnos/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetProfesorById(int id)
        {
            var profesor = profesores.FirstOrDefault(a => a.Id == id);

            if (profesor == null)
            {
                var response = new ApiResponse<string>("Profesor no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }
            Console.WriteLine($"Profesor recuperado: {profesor.Id}, {profesor.Nombres}, {profesor.HorasClase}");
            var successResponse = new ApiResponse<Profesor>(profesor, 200);
            return Ok(profesor);
        }

        // POST /alumnos
        [HttpPost]
        public IActionResult PostAlumno([FromBody] Profesor profesor)
        {
            profesores.Add(profesor);
            var response = new ApiResponse<Profesor>(profesor, 201);
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        // PUT /alumnos/{id}
        [HttpPut("{id:int}")]
        public IActionResult PutAlumno(int id, [FromBody] Profesor profesorUpdate)
        {
            var profesor = profesores.FirstOrDefault(a => a.Id == id);
            if (profesor == null)
            {
                var response = new ApiResponse<string>("Alumno no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            if (string.IsNullOrEmpty(profesorUpdate.Nombres) || string.IsNullOrEmpty(profesorUpdate.Nombres))
            {
                var response = new ApiResponse<string>("Campos inválidos.", 400);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            profesor.Nombres = profesorUpdate.Nombres;
            profesor.Apellidos = profesorUpdate.Apellidos;
            profesor.HorasClase = profesorUpdate.HorasClase;

            var successResponse = new ApiResponse<Profesor>(profesor, 200);
            return new ObjectResult(successResponse) { StatusCode = successResponse.StatusCode };
        }

        // DELETE /alumnos/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProfesor(int id)
        {
            var profesor = profesores.FirstOrDefault(a => a.Id == id);
            if (profesor == null)
            {
                var response = new ApiResponse<string>("Profesor no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            profesores.Remove(profesor);
            var responseMessage = new ApiResponse<string>("Profesor eliminado exitosamente", 200);
            return new ObjectResult(responseMessage) { StatusCode = responseMessage.StatusCode };
        }

    }

    public class Profesor
    {
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
}
