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
                .ToListAsync();
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
