using API.DTO;
using Application;
using Application.DTOs;
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
        public async Task<IActionResult> CreateUser([FromForm] UserCreateDTO userCreateDTO,[FromForm]IFormFile? image)
        {
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

                return CreatedAtAction(nameof(CreateUser), new { id = userCreateDTO }, userCreateDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the user: {ex.Message}");
            }
        }



    }
}
