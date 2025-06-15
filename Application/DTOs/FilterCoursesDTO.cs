using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    public class FilterCoursesDTO
    {
        public string? SearchTerm { get; set; } = string.Empty;
        public List<int?>? SubjectIDs { get; set; } = new List<int?>();
        public List<int> LanguageIDs { get; set; } = new List<int>();
        public List<CourseLevel>? Levels { get; set; } = new();  // 🔽 Enum type

    }
}
