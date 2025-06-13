using Application;
using Application.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly clsCourseService _courseService;

        public CoursesController(clsCourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet("new")]
        [ProducesResponseType(typeof(List<CourseReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseReadDTO>>> GetNewCourses(
            [FromQuery] int limit = 4)
        {
            try
            {
                var courses = await _courseService.GetNewCoursesAsync(limit);
                if (!courses.Any())
                {
                    return NotFound("No courses available");
                }

                var courseDtos = _mapper.Map<List<CourseReadDTO>>(courses);
                return Ok(courseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



        [HttpGet("popular")]
        [ProducesResponseType(typeof(List<CourseReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseReadDTO>>> GetPopularCourses(
    [FromQuery] int limit = 4)
        {
            try
            {
                var courses = await _courseService.GetPopularCoursesAsync(limit);
                if (!courses.Any())
                {
                    return NotFound("No courses available");
                }

                var courseDtos = _mapper.Map<List<CourseReadDTO>>(courses);
                return Ok(courseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



        [HttpGet("discover")]
        [ProducesResponseType(typeof(List<CourseReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseReadDTO>>> GetDiscoverCourses(
[FromQuery] int limit = 4)
        {
            try
            {
                var courses = await _courseService.GetDiscoverCoursesAsync(limit);
                if (!courses.Any())
                {
                    return NotFound("No courses available");
                }

                var courseDtos = _mapper.Map<List<CourseReadDTO>>(courses);
                return Ok(courseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



        [HttpGet("search")]
        [ProducesResponseType(typeof(List<CourseReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseReadDTO>>> GetSearchedCourses(
 string? searchTerm="", int limit = 12)
        {
            try
            {
                var courses = await _courseService.GetSearchedCoursesAsync(searchTerm,limit);
                if (!courses.Any())
                {
                    return NotFound("No courses available");
                }

                var courseDtos = _mapper.Map<List<CourseReadDTO>>(courses);
                return Ok(courseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



        [HttpGet("filter")]
        [ProducesResponseType(typeof(List<CourseReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseReadDTO>>> GetCoursesByFilterAsync(
[FromQuery] FilterCoursesDTO filterCoursesDTO)
        {
            try
            {
                var courses = await _courseService.GetCoursesByFilterAsync(filterCoursesDTO);
                if (!courses.Any())
                {
                    return NotFound("No courses available");
                }

                var courseDtos = _mapper.Map<List<CourseReadDTO>>(courses);
                return Ok(courseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CourseCreateDTO request, IFormFile Image)
        {
            if (Image == null || Image.Length == 0 || request.InstructorID <= 0 || request.Price < 0)
                return BadRequest("Verify the data you have entered");

            using var stream = Image.OpenReadStream();
            await _courseService.CreateCourseAsync(request,stream);
            return Ok("Course created.");
        }


    }
}
