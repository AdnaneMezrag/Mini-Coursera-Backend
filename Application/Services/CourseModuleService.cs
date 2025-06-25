using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.CourseModule;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CourseModuleService
    {
        private readonly ICourseModuleRepository _courseModuleRepository;
        private readonly IMapper _mapper;

        public CourseModuleService(ICourseModuleRepository courseModuleRepository, IMapper mapper)
        {
            _courseModuleRepository = courseModuleRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateCourseModuleAsync(CourseModuleCreateDTO courseModuleCreateDTO)
        {
            CourseModule courseModule = _mapper.Map<CourseModule>(courseModuleCreateDTO);
            await _courseModuleRepository.AddAsync(courseModule);
            return courseModule.Id;
        }

        public async Task UpdateCourseModuleAsync(int id, CourseModuleCreateDTO courseModuleCreateDTO)
        {
            CourseModule courseModule = await _courseModuleRepository.GetByIdAsync(id);
            if (courseModule == null)
            {
                throw new Exception($"CourseModule with ID {id} not found.");
            }

            // Map the updated fields from the DTO to the existing entity

            courseModule.Description = courseModuleCreateDTO.Description;
            courseModule.Name = courseModuleCreateDTO.Name;
            // Save the changes
            await _courseModuleRepository.UpdateAsync(courseModule);
        }

        public async Task DeleteCourseModuleAsync(int id)
        {
            var courseModule = await _courseModuleRepository.GetByIdAsync(id);
            if (courseModule == null)
            {
                throw new Exception($"CourseModule with ID {id} not found.");
            }

            await _courseModuleRepository.DeleteAsync(id);
        }


    }
}
