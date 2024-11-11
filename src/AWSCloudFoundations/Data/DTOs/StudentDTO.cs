using System.ComponentModel.DataAnnotations;

namespace AWSCloudFoundations.Data.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [RegularExpression(@"^[a-zA-Z]+\d+$", ErrorMessage = "La matrícula debe comenzar con letras seguidas de al menos un número")]
        public string Matricula { get; set; }
        [Range(0.0, 100.0, ErrorMessage = "El campo 'promedio' debe ser un número decimal mayor o igual a 0 y menor o igual a 100")]
        public double Promedio { get; set; }
    }
}