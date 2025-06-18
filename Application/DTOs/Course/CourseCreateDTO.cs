namespace Application.DTOs.Course
{
    public class CourseCreateDTO
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public int InstructorID { get; set; } = default!;
    }
}
