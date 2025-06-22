using Application.DTOs.Course;
using Application.DTOs.EnrollmentProgress;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentProgressController : ControllerBase
    {
        private EnrollmentProgressService _enrollmentProgressService;
        public EnrollmentProgressController(EnrollmentProgressService enrollmentProgressService)
        {
            this._enrollmentProgressService = enrollmentProgressService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentProgressCreateDTO request)
        {
            if ( request.ModuleContentId <= 0 || request.EnrollmentId< 0)
                return BadRequest("Verify the data you have entered");

            await _enrollmentProgressService.CreateEnrollmentProgress(request);
            return Ok("Enrollment progress created.");
        }

    }
}
