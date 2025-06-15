using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<clsCourse, CourseReadDTO>();
            CreateMap<Course, CourseReadDTO>()
        .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName))
        .ForMember(dest => dest.InstructorImageUrl, opt => opt.MapFrom(src => src.Instructor.PhotoUrl));

            CreateMap<FilterCoursesDTO,FilterCoursesModel >();
            CreateMap<CourseModule, CourseModuleDTO>();

            CreateMap<Course, CourseReadFullDTO>()
        .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName))
        .ForMember(dest => dest.InstructorImageUrl, opt => opt.MapFrom(src => src.Instructor.PhotoUrl))
        .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.Name))
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Subject.Name))
        .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.CourseModules));

        }
    }
}
