using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        MiniCourseraContext _miniCourseraContext { get; set; }
        public CourseRepository(MiniCourseraContext miniCourseraContext)
        {
            _miniCourseraContext = miniCourseraContext;
        }
        public async Task<List<Course>> GetNewCoursesAsync(int amount = 4)
        {
            return await _miniCourseraContext.Courses
                .OrderByDescending(course => course.CreatedAt)
                .Take(amount)
                .Include(course => course.Instructor)
                .ToListAsync();
        }
        public async Task<List<Course>> GetPopularCoursesAsync(int amount = 4)
        {
            return await _miniCourseraContext.Courses
                .OrderByDescending(course => course.EnrollmentsCount)
                .Take(amount)
                .Include(course => course.Instructor)
                .ToListAsync();
        }
        public async Task<List<Course>> GetDiscoverCoursesAsync(int amount = 4)
        {
            return await _miniCourseraContext.Courses
                .OrderBy(c => Guid.NewGuid()) // Randomize
                .Take(amount)                 // Limit the number
                .ToListAsync();
        }
        public async Task<List<Course>> GetSearchedCoursesAsync(string searchTerm, int amount)
        {
            List<Course> Courses = await _miniCourseraContext.Courses
                .Where(course => course.Title.Contains(searchTerm) || course.Description.Contains(searchTerm))
                .Take(amount)                 // Limit the number
                .ToListAsync();
            return Courses;
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            var course = _miniCourseraContext.Courses
                .Include(course => course.Instructor)
                .Include(course => course.Subject)
                .Include(course => course.Language)
                .Include(course => course.CourseModules)
                .FirstOrDefaultAsync(course => course.Id == id);

            return await course;
        }

        public Task<List<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Course entity)
        {
            _miniCourseraContext.Courses.Add(entity);
            return _miniCourseraContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Course entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Course>> GetCoursesByFilterAsync(FilterCoursesModel filterCoursesModel)
        {
            var query = _miniCourseraContext.Courses.AsQueryable();
            if (filterCoursesModel.LanguageIDs != null && filterCoursesModel.LanguageIDs.Any())
            {
                query = query
                    .Where(c => filterCoursesModel.LanguageIDs.Contains(c.LanguageID));
            }
            if (filterCoursesModel.SubjectIDs != null && filterCoursesModel.SubjectIDs.Any())
            {
                query = query
                .Where(course => filterCoursesModel.SubjectIDs.Contains(course.SubjectID));
            }
            if (filterCoursesModel.Levels != null && filterCoursesModel.Levels.Any())
            {
                query = query.Where(c => filterCoursesModel.Levels.Contains(c.Level));
            }
            // The search functionality (Can be refactored)
            query = query
            .Where(course => course.Title.Contains(filterCoursesModel.SearchTerm)
            || course.Description.Contains(filterCoursesModel.SearchTerm));


            return await query.ToListAsync();
        }

        public async Task<List<CourseModule>> GetCourseModulesContentsAsync(int courseId)
        {
            try
            {
                var courseModules = await _miniCourseraContext.CourseModules
                 .Where(cm => cm.CourseId == courseId)
                 .Include(cm => cm.ModuleContents)
                 .ToListAsync();
                return courseModules;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                throw new Exception("An error occurred while retrieving course modules and contents.", ex);

            }

        }
    }
}
