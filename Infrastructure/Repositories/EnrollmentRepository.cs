using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        MiniCourseraContext _miniCourseraContext { get; set; }
        public EnrollmentRepository(MiniCourseraContext miniCourseraContext)
        {
            _miniCourseraContext = miniCourseraContext;
        }

        public async Task AddAsync(Enrollment entity)
        {
            await _miniCourseraContext.AddAsync(entity);
            _miniCourseraContext.SaveChanges();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Enrollment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Enrollment?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Enrollment entity)
        {
            throw new NotImplementedException();
        }
    }
}
