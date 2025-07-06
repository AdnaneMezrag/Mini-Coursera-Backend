using Application.DTOs.Course;
using Application.DTOs.CourseModule;
using Application.DTOs.Other;
using Application.Services;
using AutoMapper;
using Domain.Interfaces.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CourseService _courseService;
        private readonly IVideoService _videoService;

        public CoursesController(CourseService courseService, IMapper mapper,
            IVideoService videoService
            )
        {
            _courseService = courseService;
            _mapper = mapper;
            _videoService = videoService;
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




        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CourseReadFullDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseReadFullDTO>> GetCourseByID(
    int id)
        {
            try
            {
                var course = await _courseService.GetByIdAsync(id);
                if (course == null)
                {
                    return NotFound("No available course");
                }

                var CourseDto = _mapper.Map<CourseReadFullDTO>(course);
                return Ok(CourseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CourseCreateDTO request, IFormFile Image)
        {
            try
            {
                if (Image == null || Image.Length == 0 || request.InstructorID <= 0
                    || request.Price < 0 || request.LanguageID <= 0
                    || request?.SubjectID <= 0 || request.Level == null)
                    return BadRequest("Verify the data you have entered");

                using var stream = Image.OpenReadStream();
                int courseId = await _courseService.CreateCourseAsync(request, stream);
                return Ok(courseId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



        [HttpGet("modules")]
        [ProducesResponseType(typeof(List<CourseModuleReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseModuleReadDTO>>> GetCourseModulesContents(
[FromQuery] int courseId)
        {
            try
            {
                List<CourseModuleReadDTO> courseModules = await _courseService.GetCourseModulesContentsAsync(courseId);
                if (!courseModules.Any())
                {
                    return NotFound("No modules available");
                }

                return Ok(courseModules);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int Id, [FromForm] CourseCreateDTO request, IFormFile? Image)
        {
            try
            {
                if (request.Price < 0 || request.LanguageID <= 0
                    || request?.SubjectID <= 0 || request.Level == null)
                    return BadRequest("Verify the data you have entered");

                using var stream = Image?.OpenReadStream();
                await _courseService.UpdateCourseAsync(Id,request, stream);
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }


    }
}
