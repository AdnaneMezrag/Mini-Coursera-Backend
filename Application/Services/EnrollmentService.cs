using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces.Utilities;
using Domain.Interfaces;
using Domain.Entities;
using Application.DTOs.Enrollment;

namespace Application.Services
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task AddEnrollmentAsync(EnrollmentCreateDTO enrollmentCreateDTO)
        {
            var enrollment = _mapper.Map<Enrollment>(enrollmentCreateDTO);
            await _enrollmentRepository.AddAsync(enrollment);
        }

    }
}
