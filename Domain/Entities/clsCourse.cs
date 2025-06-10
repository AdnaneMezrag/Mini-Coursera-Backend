using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class clsCourse
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public int InstructorID { get; set; } = default!;
        public clsInstructor Instructor { get; set; } = default!;
        public List<clsStudent> Students { get; set; } = new List<clsStudent>();
        public int EnrollmentsCount { get; set; }
        public clsCourse()
        {
            // Default constructor for EF Core
            CreatedAt = DateTime.UtcNow;
        }

    }
}
