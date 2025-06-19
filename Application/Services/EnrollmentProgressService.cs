using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.EnrollmentProgress;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class EnrollmentProgressService
    {
        private readonly IEnrollmentProgressRepository _enrollmentProgressRepository;
        private readonly IMapper _mapper;
        public EnrollmentProgressService(IEnrollmentProgressRepository enrollmentProgressRepository, IMapper mapper)
        {
            _enrollmentProgressRepository = enrollmentProgressRepository;
            _mapper = mapper;
        }

        public async Task CreateEnrollmentProgress(EnrollmentProgressCreateDTO enrollmentProgressCreateDTO)
        {
            EnrollmentProgress enrollmentProgress = _mapper.Map<EnrollmentProgress>(enrollmentProgressCreateDTO);        
            await _enrollmentProgressRepository.AddAsync(enrollmentProgress);
        }


    }
}