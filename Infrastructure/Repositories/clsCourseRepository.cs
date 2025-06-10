using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class clsCourseRepository : ICourseRepository
    {
        MiniCourseraContext _miniCourseraContext { get; set; }
        public clsCourseRepository(MiniCourseraContext miniCourseraContext)
        {
            _miniCourseraContext = miniCourseraContext;
        }
        public async Task<List<clsCourse>> GetNewCoursesAsync(int amount = 4)
        {
            return await _miniCourseraContext.Courses
                .OrderByDescending(course => course.CreatedAt)
                .Take(amount)
                .Include(course => course.Instructor)
                .ToListAsync();
        }
        public async Task<List<clsCourse>> GetPopularCoursesAsync(int amount = 4)
        {
            return await _miniCourseraContext.Courses
                .OrderByDescending(course => course.EnrollmentsCount)
                .Take(amount)
                .Include(course => course.Instructor)
                .ToListAsync();
        }
        public async Task<List<clsCourse>> GetDiscoverCoursesAsync(int amount = 4)
        {
            return await _miniCourseraContext.Courses
                .OrderBy(c => Guid.NewGuid()) // Randomize
                .Take(amount)                 // Limit the number
                .ToListAsync();
        }
        public async Task<List<clsCourse>> GetSearchedCoursesAsync(string searchTerm,int amount)
        {
            List<clsCourse> Courses = await _miniCourseraContext.Courses
                .Where(course => course.Title.Contains(searchTerm) || course.Description.Contains(searchTerm))
                .Take(amount)                 // Limit the number
                .ToListAsync();
            return Courses;
        }

        public Task<clsCourse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<clsCourse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(clsCourse entity)
        {
            _miniCourseraContext.Courses.Add(entity);
            return _miniCourseraContext.SaveChangesAsync();
        }

        public Task UpdateAsync(clsCourse entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
