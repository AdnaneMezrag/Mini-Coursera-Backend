﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEnrollmentRepository:IBaseRepository<Enrollment>
    {
        public Task <List<Enrollment>> GetEnrolledCoursesByStudentId(int studentId);
        public Task <Enrollment> GetEnrollmentByCourseIdAndStudentId(int courseId, int studentId);
        public Task<Enrollment?> GetEnrollmentWithProgressAndCourse(int EnrollmentId);
    }
}
