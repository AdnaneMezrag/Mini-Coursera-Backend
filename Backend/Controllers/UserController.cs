using API.DTO;
using Application.DTOs.Course;
using Application.DTOs.User;
using Application.Services;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IMapper _mapper;
        UserService _userService;
        public UserController(IMapper mapper, UserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromForm]UserDTO userDTO)
        {
            UserCreateDTO userCreateDTO = userDTO.userCreateDTO;
            IFormFile? image = userDTO.image;
            if (userCreateDTO == null)
            {
                return BadRequest("User data is null");
            }

            if (string.IsNullOrWhiteSpace(userCreateDTO.FirstName))
            {
                return BadRequest("First name cannot be empty");
            }

            try
            {
                Stream? imageStream = null;

                if (image != null)
                {
                    imageStream = image.OpenReadStream();
                }

                await _userService.CreateUserAsync(userCreateDTO, imageStream);

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the user: {ex.Message}");
            }
        }




        [HttpGet("login")]
        [ProducesResponseType(typeof(UserReadDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDTO>> GetUserByEmailAndPassword(
    string email,string password)
        {
            try
            {
                var user = await _userService.GetByEmailAndPasswordAsync(email,password);
                if (user == null)
                {
                    return NotFound("No available user");
                }

                var userDTO = _mapper.Map<UserReadDTO>(user);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }



    }
}
