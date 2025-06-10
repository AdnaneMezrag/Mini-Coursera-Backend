using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<clsCourse, CourseReadDTO>();
            CreateMap<clsCourse, CourseReadDTO>()
    .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName))
    .ForMember(dest => dest.InstructorImageUrl, opt => opt.MapFrom(src => src.Instructor.PhotoUrl));

            // Add other mappings as needed
        }
    }
}
