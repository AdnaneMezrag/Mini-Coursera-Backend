using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public int InstructorID { get; set; } = default!;
        public Instructor Instructor { get; set; } = default!;
        public List<Student> Students { get; set; } = new List<Student>();
        public int EnrollmentsCount { get; set; }
        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public int LanguageID { get; set; }
        public Language Language { get; set; }
        //public string Level { get; set; } = default!;
        //public string Duration { get; set; } = default!;
        public Course()
        {
            // Default constructor for EF Core
            CreatedAt = DateTime.UtcNow;
        }

    }
}
