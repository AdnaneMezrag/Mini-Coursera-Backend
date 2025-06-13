using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        public Task<List<Course>> GetNewCoursesAsync(int amount = 4);
        public Task<List<Course>> GetPopularCoursesAsync(int amount = 4);
        public Task<List<Course>> GetDiscoverCoursesAsync(int amount = 4);
        public Task<List<Course>> GetSearchedCoursesAsync(string searchTerm, int amount = 4);
        public Task<List<Course>> GetCoursesByFilterAsync(FilterCoursesModel filterCoursesModel);
    }
}
