using Application.DTOs.Course;
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



 //       [HttpGet("search")]
 //       [ProducesResponseType(typeof(List<CourseReadDTO>), StatusCodes.Status200OK)]
 //       [ProducesResponseType(StatusCodes.Status500InternalServerError)]
 //       [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
 //       public async Task<ActionResult<List<CourseReadDTO>>> GetSearchedCourses(
 //string? searchTerm="", int limit = 12)
 //       {
 //           try
 //           {
 //               var courses = await _courseService.GetSearchedCoursesAsync(searchTerm,limit);
 //               if (!courses.Any())
 //               {
 //                   return NotFound("No courses available");
 //               }

 //               var courseDtos = _mapper.Map<List<CourseReadDTO>>(courses);
 //               return Ok(courseDtos);
 //           }
 //           catch (Exception ex)
 //           {
 //               return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
 //           }



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
        public async Task<IActionResult> Create([FromForm] CourseCreateDTO request, IFormFile Image)
        {
            if (Image == null || Image.Length == 0 || request.InstructorID <= 0 || request.Price < 0)
                return BadRequest("Verify the data you have entered");

            using var stream = Image.OpenReadStream();
            await _courseService.CreateCourseAsync(request,stream);
            return Ok("Course created.");
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No video uploaded.");

            Stream FileStream = file.OpenReadStream();
            string FileName = file.FileName;


            var url = await _videoService.UploadVideoAsync(FileStream,FileName);
            return Ok(new { videoUrl = url });
        }



        [HttpGet("modules")]
        [ProducesResponseType(typeof(List<CourseReadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseReadDTO>>> GetCourseModulesContents(
[FromQuery] int courseId)
        {
            try
            {
                var courseModules = await _courseService.GetCourseModulesContentsAsync(courseId);
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

    }
}
