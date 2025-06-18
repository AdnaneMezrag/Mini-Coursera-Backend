using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTOs.Enrollment
{
    public class EnrollmentCreateDTO
    {
        public int StudentId { get; set; } = default!;
        public int CourseId { get; set; } = default!;
    }
}
