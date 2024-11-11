using AWSCloudFoundations.Helpers.Utils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AWSCloudFoundations.Controllers
{
    [ApiController]
    [Route("alumnosPruebas")]
    public class AlumnosController : ControllerBase
    {
        private static List<Alumno> alumnos = new();

        // GET /alumnos
        [HttpGet]
        public IActionResult GetAlumnos()
        {
            var response = new ApiResponse<List<Alumno>>(alumnos, 200);
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        // GET /alumnos/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetAlumnoById(int id)
        {
            var alumno = alumnos.FirstOrDefault(a => a.id == id);
            if (alumno == null)
            {
                var response = new ApiResponse<string>("Alumno no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }
            Console.WriteLine($"Alumno recuperado: {alumno.id}, {alumno.nombres}, {alumno.matricula}");
            return Ok(alumno);
        }

        // POST /alumnos
        [HttpPost]
        public IActionResult PostAlumno([FromBody] Alumno alumno)
        {
            alumnos.Add(alumno);
            var response = new ApiResponse<Alumno>(alumno, 201);
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        // PUT /alumnos/{id}
        [HttpPut("{id:int}")]
        public IActionResult PutAlumno(int id, [FromBody] Alumno alumnoUpdate)
        {
            var alumno = alumnos.FirstOrDefault(a => a.id == id);
            if (alumno == null)
            {
                var response = new ApiResponse<string>("Alumno no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            if (string.IsNullOrEmpty(alumnoUpdate.nombres) || string.IsNullOrEmpty(alumnoUpdate.nombres))
            {
                var response = new ApiResponse<string>("Campos inválidos.", 400);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            alumno.nombres = alumnoUpdate.nombres;
            alumno.matricula = alumnoUpdate.matricula;
            alumno.promedio = alumnoUpdate.promedio;

            var successResponse = new ApiResponse<Alumno>(alumno, 200);
            return new ObjectResult(successResponse) { StatusCode = successResponse.StatusCode };
        }

        // DELETE /alumnos/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAlumno(int id)
        {
            var alumno = alumnos.FirstOrDefault(a => a.id == id);
            if (alumno == null)
            {
                var response = new ApiResponse<string>("Alumno no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            alumnos.Remove(alumno);
            var responseMessage = new ApiResponse<string>("Alumno eliminado exitosamente", 200);
            return new ObjectResult(responseMessage) { StatusCode = responseMessage.StatusCode };
        }
        
    }

    public class Alumno
    {
        public int id { get; set; }
        [Required]
        public string nombres { get; set; }
        [Required]
        public string apellidos { get; set; }
        [RegularExpression(@"^[a-zA-Z]+\d+$", ErrorMessage = "La matrícula debe comenzar con letras seguidas de al menos un número")]
        public string matricula { get; set; }
        [Range(0.0, 100.0, ErrorMessage = "El campo 'promedio' debe ser un número decimal mayor o igual a 0 y menor o igual a 100")]
        public double promedio { get; set; }
    }
}
