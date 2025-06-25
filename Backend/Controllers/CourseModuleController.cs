using Application.DTOs.CourseModule;
using Application.DTOs.Enrollment;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseModuleController : ControllerBase
    {
        private CourseModuleService _courseModuleService;
        public CourseModuleController(CourseModuleService courseModuleService)
        {
            this._courseModuleService = courseModuleService;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCourseModuleAsync([FromBody] CourseModuleCreateDTO courseModuleCreateDTO)
        {
            if (courseModuleCreateDTO == null)
            {
                return BadRequest("Course module data is null.");
            }
            try
            {
                int courseModuleId = await _courseModuleService.CreateCourseModuleAsync(courseModuleCreateDTO);
                return Ok(courseModuleId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding course module: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCourseModuleAsync(int id, [FromBody] CourseModuleCreateDTO courseModuleUpdateDTO)
        {
            if (courseModuleUpdateDTO == null)
            {
                return BadRequest("Invalid course module data.");
            }
            try
            {
                await _courseModuleService.UpdateCourseModuleAsync(id,courseModuleUpdateDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating course module: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCourseModuleAsync(int id)
        {
            try
            {
                await _courseModuleService.DeleteCourseModuleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting course module: {ex.Message}");
            }
        }



    }
}
