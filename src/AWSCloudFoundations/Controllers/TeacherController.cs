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
        public async Task<IActionResult> GetTeachers()
        {
            try
            {
                var response = new ResponseData<List<TeacherDTO>>();
                response.data = service.GetTeachers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseError();
                response.message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            try
            {
                var response = new ResponseData<TeacherDTO>();
                response.data = service.GetTeacherById(id);
                if (response.data == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseError();
                response.message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostTeacher([FromBody] TeacherCreateDTO teacherCreate)
        {
            try
            {
                service.PostTeacher(teacherCreate);
                return NoContent();
            }
            catch (Exception ex)
            {
                var response = new ResponseError();
                response.message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutTeacher(int id, [FromBody] TeacherCreateDTO teacherUpdate)
        {
            try
            {
                var bDelete = service.PutTeacher(id, teacherUpdate);
                if (!bDelete) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                var response = new ResponseError();
                response.message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var bDelete = service.DeleteTeacher(id);
                if (!bDelete) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                var response = new ResponseError();
                response.message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
