using System.ComponentModel.DataAnnotations;

namespace AWSCloudFoundations.Data.DTOs
{
    public class TeacherDTO
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