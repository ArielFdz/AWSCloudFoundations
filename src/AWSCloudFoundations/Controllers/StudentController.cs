using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Helpers.Utils;
using AWSCloudFoundations.Services;
using Microsoft.AspNetCore.Mvc;

namespace AWSCloudFoundations.Controllers
{
    [ApiController]
    [Route("alumnos")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService service;

        public StudentController(IStudentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var listStudents = service.GetStudents();
            return Ok(listStudents);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            var student = service.GetStudentById(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public IActionResult PostStudent([FromBody] StudentDTO studentCreate)
        {
            service.PostStudent(studentCreate);
            return CreatedAtAction(nameof(GetStudentById), new { id = studentCreate.Id }, new ResponseData<StudentDTO> { data = studentCreate });
        }

        [HttpPut("{id:int}")]
        public IActionResult PutStudent(int id, [FromBody] StudentDTO studentUpdate)
        {
            var bDelete = service.PutStudent(id, studentUpdate);
            if (!bDelete) return NotFound();
            return Ok(new { message = "Alumno actualizado exitosamente" });
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            var bDelete = service.DeleteStudent(id);
            if (!bDelete) return NotFound();
            return Ok();
        }

    }
}