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

    }
}