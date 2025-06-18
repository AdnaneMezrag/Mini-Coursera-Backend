using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; } = default!;
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; } = false;
        public int StudentId { get; set; } = default!;
        public User Student { get; set; }
        public int CourseId { get; set; } = default!;
        public Course Course { get; set; }
    }
}
