using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Utilities;

namespace Application
{
    public class clsCourseService 
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IImageStorageService _imageStorage;

        public clsCourseService(ICourseRepository courseRepository, IImageStorageService imageStorage)
        {
            _courseRepository = courseRepository;
            _imageStorage = imageStorage;
        }
        
        public async Task<List<clsCourse>> GetNewCoursesAsync(int amount = 4)
        {
            return await _courseRepository.GetNewCoursesAsync(amount);
        }

        public async Task CreateCourseAsync(CourseCreateDTO courseCreateDTO, Stream imageStream)
        {
            var imageUrl = await _imageStorage.SaveImageAsync(imageStream);
            var course = new clsCourse();
            course.Title = courseCreateDTO.Title;
            course.Description = courseCreateDTO.Description;
            course.ImageUrl = imageUrl;
            course.Price = courseCreateDTO.Price;
            course.InstructorID = courseCreateDTO.InstructorID;
            await _courseRepository.AddAsync(course);
        }

    }
}
