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
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var response = new ResponseData<List<StudentDTO>>();
                response.data = service.GetStudents();
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
        public async Task<IActionResult> GetStudentById(int id) 
        {
            try
            {
                var response = new ResponseData<StudentDTO>();
                response.data = service.GetStudentById(id);
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
        public async Task<IActionResult> PostStudent([FromBody] StudentCreateDTO studentCreate)
        {
            try
            {
                service.PostStudent(studentCreate);
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
        public async Task<IActionResult> PutStudent(int id, [FromBody] StudentCreateDTO studentUpdate)
        {
            try
            {
                var bDelete = service.PutStudent(id, studentUpdate);
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
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var bDelete = service.DeleteStudent(id);
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