using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Helpers.Utils;
using AWSCloudFoundations.Services;
using Microsoft.AspNetCore.Mvc;

namespace AWSCloudFoundations.Controllers
{
    [ApiController]
    [Route("profesores")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService service;

        public TeacherController(ITeacherService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetTeachers()
        {
            var listTeachers = service.GetTeachers();
            return Ok(listTeachers);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = service.GetTeacherById(id);
            if (teacher == null) return NotFound();
            return Ok(teacher);
        }

        [HttpPost]
        public IActionResult PostTeacher([FromBody] TeacherDTO teacherCreate)
        {
            service.PostTeacher(teacherCreate);
            return CreatedAtAction(nameof(GetTeacherById), new { id = teacherCreate.Id }, new ResponseData<TeacherDTO> { data = teacherCreate });
        }

        [HttpPut("{id:int}")]
        public IActionResult PutTeacher(int id, [FromBody] TeacherDTO teacherUpdate)
        {
            var bDelete = service.PutTeacher(id, teacherUpdate);
            if (!bDelete) return NotFound();
            return Ok(new { message = "Profesor actualizado exitosamente" });
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTeacher(int id)
        {
            var bDelete = service.DeleteTeacher(id);
            if (!bDelete) return NotFound();
            return Ok();
        }
    }
}
