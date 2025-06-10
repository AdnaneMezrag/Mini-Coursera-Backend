using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICourseRepository : IBaseRepository<clsCourse>
    {
        public Task<List<clsCourse>> GetNewCoursesAsync(int amount = 4);
    }
}
