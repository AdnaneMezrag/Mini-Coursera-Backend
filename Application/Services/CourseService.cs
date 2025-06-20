﻿using Application.DTOs;
using Application.DTOs.Course;
using Application.DTOs.Other;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Utilities;
using Domain.Models;

namespace Application.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IImageStorageService _imageStorage;
        private readonly IMapper _mapper;
        private readonly IVideoService _videoService;

        public CourseService(ICourseRepository courseRepository,
            IImageStorageService imageStorage, IMapper mapper, IVideoService videoService)
        {
            _courseRepository = courseRepository;
            _imageStorage = imageStorage;
            _mapper = mapper;
            _videoService = videoService;
        }

        public async Task<List<Course>> GetNewCoursesAsync(int amount = 4)
        {
            return await _courseRepository.GetNewCoursesAsync(amount);
        }

        public async Task<List<Course>> GetPopularCoursesAsync(int amount = 4)
        {
            return await _courseRepository.GetPopularCoursesAsync(amount);
        }
        public async Task<List<Course>> GetDiscoverCoursesAsync(int amount = 4)
        {
            return await _courseRepository.GetDiscoverCoursesAsync(amount);
        }
        public async Task<List<Course>> GetSearchedCoursesAsync(string searchTerm, int amount)
        {
            return await _courseRepository.GetSearchedCoursesAsync(searchTerm, amount);
        }
        public async Task CreateCourseAsync(CourseCreateDTO courseCreateDTO, Stream imageStream)
        {
            var imageUrl = await _imageStorage.SaveImageAsync(imageStream);
            var course = new Course();
            course.Title = courseCreateDTO.Title;
            course.Description = courseCreateDTO.Description;
            course.ImageUrl = imageUrl;
            course.Price = courseCreateDTO.Price;
            course.InstructorID = courseCreateDTO.InstructorID;
            await _courseRepository.AddAsync(course);
        }

        public async Task<List<Course>> GetCoursesByFilterAsync(FilterCoursesDTO filterCoursesDTO)
        {
            var filterCoursesModel = _mapper.Map<FilterCoursesModel>(filterCoursesDTO);
            return await _courseRepository.GetCoursesByFilterAsync(filterCoursesModel);
        }

        public async Task<Course>? GetByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<List<CourseModuleReadDTO>> GetCourseModulesContentsAsync(int courseId)
        {
            List<CourseModule> courseModules = await _courseRepository.GetCourseModulesContentsAsync(courseId);
            List<CourseModuleReadDTO> courseModuleReadDTOs = _mapper.Map<List<CourseModuleReadDTO>>(courseModules);
            return courseModuleReadDTOs;
        }

    }
}
