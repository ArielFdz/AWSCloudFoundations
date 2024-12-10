using Amazon.S3.Transfer;
using Amazon.S3;
using AWSCloudFoundations.Data;
using AWSCloudFoundations.Helpers.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Amazon;
using Amazon.Runtime;

namespace AWSCloudFoundations.Controllers
{
    [ApiController]
    [Route("alumnos")]
    public class AlumnosController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;

        public AlumnosController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        private static List<Alumno> alumnos = new();

        // GET /alumnos
        [HttpGet]
        public async Task<IActionResult> GetAlumnos()
        {
            var alumnos = await _context.Alumnos.ToListAsync();
            //var response = new ApiResponse<List<Alumno>>(alumnos, 200);

            var response = new ApiResponse<List<AlumnoResponseDTO>>(alumnos.Select(p => new AlumnoResponseDTO
            {
                id = p.id,
                nombres = p.nombres,
                apellidos = p.apellidos,
                matricula = p.matricula,
                promedio = p.promedio,
                fotoPerfilUrl = p.fotoPerfilUrl,
                password = p.password
            }).ToList(), 200);

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAlumnoById(int id)
        {
            var alumno = await _context.Alumnos.FirstOrDefaultAsync(x => x.id == id);
            if (alumno == null)
            {
                var response = new ApiResponse<string>("Alumno no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            var responseProfesor = new AlumnoResponseDTO
            {
                id = alumno.id,
                nombres = alumno.nombres,
                apellidos = alumno.apellidos,
                matricula = alumno.matricula,
                promedio = alumno.promedio,
                fotoPerfilUrl = alumno.fotoPerfilUrl,
                password = alumno.password
            };

            return Ok(responseProfesor);
        }

        //// POST /alumnos
        [HttpPost]
        public async Task<IActionResult> PostAlumno([FromBody] CreateAlumnoDTO alumnoDto)
        {
            var alumno = new Alumno
            {
                nombres = alumnoDto.nombres,
                apellidos = alumnoDto.apellidos,
                matricula = alumnoDto.matricula,
                promedio = alumnoDto.promedio
            };

            await _context.Alumnos.AddAsync(alumno);
            await _context.SaveChangesAsync();

            var response = new AlumnoResponseDTO
            {
                id = alumno.id,
                nombres = alumno.nombres,
                apellidos = alumno.apellidos,
                matricula = alumno.matricula,
                promedio = alumno.promedio,
                password = alumno.password
            };
            return CreatedAtAction(nameof(GetAlumnoById), new { id = alumno.id }, response);
        }

        //// PUT /alumnos/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAlumno(int id, [FromBody] UpdateAlumnoDTO alumnoDto)
        {
            var alumno = await _context.Alumnos.FirstOrDefaultAsync(x => x.id == id);
            if (alumno == null)
            {
                var response = new ApiResponse<string>("Alumno no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            alumno.nombres = alumnoDto.nombres;
            alumno.apellidos = alumnoDto.apellidos;
            alumno.matricula = alumnoDto.matricula;
            alumno.promedio = alumnoDto.promedio;
            alumno.password = alumnoDto.password;

            await _context.SaveChangesAsync();

            var successResponse = new ApiResponse<AlumnoResponseDTO>(new AlumnoResponseDTO
            {
                id = alumno.id,
                nombres = alumno.nombres,
                apellidos = alumno.apellidos,
                matricula = alumno.matricula,
                promedio = alumno.promedio,
                password = alumno.password
            }, 200);

            return new ObjectResult(successResponse) { StatusCode = successResponse.StatusCode };
        }

        //// DELETE /alumnos/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FirstOrDefaultAsync(x => x.id == id);
            if (alumno == null)
            {
                var response = new ApiResponse<string>("Alumno no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            var responseMessage = new ApiResponse<string>("Alumno eliminado exitosamente", 200);
            return new ObjectResult(responseMessage) { StatusCode = responseMessage.StatusCode };
        }

        [HttpPost("{id:int}/fotoPerfil")]
        public async Task<IActionResult> UploadFotoPerfil(int id, IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
            {
                return BadRequest("No se ha proporcionado ninguna imagen.");
            }

            var alumno = await _context.Alumnos.FirstOrDefaultAsync(x => x.id == id);
            if (alumno == null)
            {
                var response = new ApiResponse<string>("Alumno no encontrado", 404);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            var bucketName = configuration["AWS:BucketName"];

            var credentials = new SessionAWSCredentials(
                configuration["AWS:AccessKeyId"],
                configuration["AWS:SecretAccessKey"],
                configuration["AWS:SessionToken"]
            );

            var s3Client = new AmazonS3Client(
                credentials,
                new AmazonS3Config { RegionEndpoint = RegionEndpoint.USEast1 }
            );

            var transferUtility = new TransferUtility(s3Client);

            await transferUtility.UploadAsync(foto.OpenReadStream(), bucketName, fileName);
            var fotoPerfilUrl = $"https://{bucketName}.s3.amazonaws.com/{fileName}";
            alumno.fotoPerfilUrl = fotoPerfilUrl;
            await _context.SaveChangesAsync();

            return Ok(new { FotoPerfilUrl = fotoPerfilUrl });
        }


    }

    public class Alumno
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nombres { get; set; }
        [Required]
        public string apellidos { get; set; }
        [RegularExpression(@"^[a-zA-Z]+\d+$", ErrorMessage = "La matrícula debe comenzar con letras seguidas de al menos un número")]
        public string matricula { get; set; }
        [Range(0.0, 100.0, ErrorMessage = "El campo 'promedio' debe ser un número decimal mayor o igual a 0 y menor o igual a 100")]
        public double promedio { get; set; }
        public string? fotoPerfilUrl { get; set; }
        public string? password { get; set; }
    }

    public class CreateAlumnoDTO
    {
        [Required]
        public string nombres { get; set; }

        [Required]
        public string apellidos { get; set; }

        [RegularExpression(@"^[a-zA-Z]+\d+$", ErrorMessage = "La matrícula debe comenzar con letras seguidas de al menos un número")]
        public string matricula { get; set; }

        [Range(0.0, 100.0, ErrorMessage = "El campo 'promedio' debe ser un número decimal mayor o igual a 0 y menor o igual a 100")]
        public double promedio { get; set; }
        public string? password { get; set; }
    }

    public class UpdateAlumnoDTO
    {
        [Required]
        public string nombres { get; set; }

        [Required]
        public string apellidos { get; set; }

        [RegularExpression(@"^[a-zA-Z]+\d+$", ErrorMessage = "La matrícula debe comenzar con letras seguidas de al menos un número")]
        public string matricula { get; set; }

        [Range(0.0, 100.0, ErrorMessage = "El campo 'promedio' debe ser un número decimal mayor o igual a 0 y menor o igual a 100")]
        public double promedio { get; set; }
        public string? password { get; set; }
    }

    public class AlumnoResponseDTO
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string matricula { get; set; }
        public double promedio { get; set; }
        public string fotoPerfilUrl { get; set; }
        public string? password { get; set; }
    }
}
