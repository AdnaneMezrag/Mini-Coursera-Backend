﻿using API.DTO;
using Application.DTOs.ModuleContent;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers
{
    [Route("api/moduleContent")]
    [ApiController]
    public class ModuleContentController : ControllerBase
    {
        private readonly ModuleContentService _moduleContentService;

        public ModuleContentController(ModuleContentService moduleContentService)
        {
            _moduleContentService = moduleContentService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddModuleContentAsync([FromForm] ModuleContentDTO moduleContentDTO)
        {
            if (moduleContentDTO == null || moduleContentDTO.moduleContentCreateDTO == null)
            {
                return BadRequest("Module content data is null.");
            }

            try
            {
                Stream? videoStream = null;

                if (moduleContentDTO.videoFile != null)
                {
                    videoStream = moduleContentDTO.videoFile.OpenReadStream();
                }
                string fileName = moduleContentDTO?.videoFile?.Name;
                int moduleContentId = await _moduleContentService.AddModuleContentAsync(moduleContentDTO.moduleContentCreateDTO,videoStream,fileName);
                return Ok(moduleContentId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding module content: {ex.Message}");
            }
        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateModuleContentAsync([FromForm] ModuleContentDTOUpdate moduleContentDTOUpdate)
        {
            if (moduleContentDTOUpdate == null || moduleContentDTOUpdate.ModuleContentUpdateDTO == null)
            {
                return BadRequest("Module content data is null.");
            }

            Stream? videoStream = null;

            try
            {
                if (moduleContentDTOUpdate.videoFile != null)
                {
                    videoStream = moduleContentDTOUpdate.videoFile.OpenReadStream();
                }

                string fileName = moduleContentDTOUpdate?.videoFile?.FileName;

                await _moduleContentService.UpdateModuleContentAsync(
                    moduleContentDTOUpdate.ModuleContentUpdateDTO,
                    videoStream,fileName);

                return Ok("Module content updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating module content: {ex.Message}");
            }
            finally
            {
                videoStream?.Dispose();
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteModuleContentAsync(int id)
        {
            try
            {
                await _moduleContentService.DeleteModuleContentAsync(id);
                return Ok($"Module content with ID {id} deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Module content with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting module content: {ex.Message}");
            }
        }

    }
}
