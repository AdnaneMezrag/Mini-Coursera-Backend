using Application.DTOs.Enrollment;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        public EnrollmentService _enrollmentService { get; set; }
        public EnrollmentController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEnrollmentAsync([FromBody] EnrollmentCreateDTO enrollmentCreateDTO)
        {
            if (enrollmentCreateDTO == null)
            {
                return BadRequest("Enrollment data is null.");
            }
            try
            {
                await _enrollmentService.AddEnrollmentAsync(enrollmentCreateDTO);
                return Ok("Enrollment added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding enrollment: {ex.Message}");
            }
        }


    }
}
