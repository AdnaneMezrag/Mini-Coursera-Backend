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

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetNewCoursesAsync();
            // Map entities to DTOs
            var courseDtos = _mapper.Map<List<CourseReadDTO>>(courses);

            return Ok(courseDtos);
        }


        // In the Controller (Presentation Layer)
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CourseCreateDTO request, IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
                return BadRequest("Image is required.");

            using var stream = Image.OpenReadStream();
            await _courseService.CreateCourseAsync(request,stream);

            return Ok("Course created.");
        }

    }
}
