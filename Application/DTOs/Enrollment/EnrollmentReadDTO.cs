using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Enrollment
{
    public class EnrollmentReadDTO
    {
        public int Id { get; set; }
        public int CourseID { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string CourseTitle { get; set; } = default!;
    }
}
