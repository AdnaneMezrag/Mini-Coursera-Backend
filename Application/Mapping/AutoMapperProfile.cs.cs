using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<clsCourse, CourseReadDTO>();
            // Add other mappings as needed
        }
    }
}
