using System.Runtime.CompilerServices;
using Application.DTOs.Course;
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




        [HttpGet("student/{studentId}")]
        [ProducesResponseType(typeof(List<EnrollmentReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EnrollmentReadDTO>>> GetEnrolledCoursesByStudentId(
int studentId)
        {
            try
            {
                var enrollmentReadDTOs = await _enrollmentService.GetEnrolledCoursesByStudentId(studentId);
                if (!enrollmentReadDTOs.Any())
                {
                    return NotFound("No enrolled courses available");
                }

                return Ok(enrollmentReadDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



        [HttpGet("by-course-and-student")]
        [ProducesResponseType(typeof(List<EnrollmentReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EnrollmentReadDTO>> GetEnrollmentByCourseIdAndStudentId(
     int courseId , int studentId)
        {
            if(courseId <= 0 ||  studentId <= 0)
            {
                return BadRequest("CourseId and/or StudentId should be > 0");
            }
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByCourseIdAndStudentId(courseId,studentId);
                if (enrollment == null)
                {
                    return NotFound("No enrollment is available");
                }

                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }


    }
}
