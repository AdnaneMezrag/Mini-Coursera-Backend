using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.ModuleContent;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Utilities;

namespace Application.Services
{
    public class ModuleContentService
    {
        private IModuleContentRepository _moduleContentRepository;
        private IMapper _mapper;
        private IVideoService _videoService;
        public ModuleContentService(
            IModuleContentRepository moduleContentRepository, IMapper mapper
            ,IVideoService videoService)
        {
            _moduleContentRepository = moduleContentRepository;
            _mapper = mapper;
            _videoService = videoService;
        }

        public async Task<int> AddModuleContentAsync(ModuleContentCreateDTO moduleContentCreateDTO, Stream videoStream, string fileName)
        {
            ModuleContent moduleContent = _mapper.Map<ModuleContent>(moduleContentCreateDTO);
            if (moduleContent == null)
            {
                throw new ArgumentNullException(nameof(moduleContent), "Module content cannot be null.");
            }
            if (videoStream != null)
            {
                string videoUrl = await _videoService.UploadVideoAsync(videoStream, fileName);
                moduleContent.VideoUrl = videoUrl;
            }
            await _moduleContentRepository.AddAsync(moduleContent);
            return moduleContent.Id;
        }

        public async Task UpdateModuleContentAsync(ModuleContentUpdateDTO dto, Stream? videoStream, string? fileName)
        {
            var moduleContent = _mapper.Map<ModuleContent>(dto);
            var original = await _moduleContentRepository.GetByIdAsync(moduleContent.Id);
            if (original == null) return;
            moduleContent.VideoUrl = original.VideoUrl;
            if (dto.DeleteVideo)
            {
                await _videoService.DeleteVideoAsync(original.VideoUrl);
                moduleContent.VideoUrl = null;
                if (videoStream != null && !string.IsNullOrEmpty(fileName))
                {
                    string newUrl = await _videoService.UploadVideoAsync(videoStream, fileName);
                    moduleContent.VideoUrl = newUrl;
                }
            }

             await _moduleContentRepository.UpdateAsync(moduleContent);
        }

        public async Task DeleteModuleContentAsync(int id)
        {
            var moduleContent = await _moduleContentRepository.GetByIdAsync(id);
            if (moduleContent == null)
            {
                throw new KeyNotFoundException($"Module content with ID {id} not found.");
            }

            // Optional: delete the associated video from cloud storage if it exists
            if (!string.IsNullOrEmpty(moduleContent.VideoUrl))
            {
                await _videoService.DeleteVideoAsync(moduleContent.VideoUrl);
            }

            await _moduleContentRepository.DeleteAsync(id);
        }


    }
}
