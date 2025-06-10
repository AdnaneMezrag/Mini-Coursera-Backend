namespace Application.DTOs
{
    public class CourseReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        //public clsInstructor Tutor { get; set; } = default!;
    }
}
