using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Utilities;

namespace Application.Services
{
    public class UserService
    {
        public IUserRepository _userRepository;
        public IMapper _mapper;
        private readonly IImageStorageService _imageStorage;

        public UserService(IUserRepository userRepository, IMapper mapper, IImageStorageService imageStorageService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _imageStorage = imageStorageService;
        }

        public async Task CreateUserAsync(UserCreateDTO userCreateDTO, Stream imageStream)
        {
            // Map UserCreateDTO to User entity
            var imageUrl = await _imageStorage.SaveImageAsync(imageStream);

            var user = _mapper.Map<User>(userCreateDTO);
            user.PhotoUrl = imageUrl;
            await _userRepository.AddAsync(user);
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAndPasswordAsync(email, password);
            //UserCreateDTO userCreateDTO = _mapper.Map<UserCreateDTO>(user);
            return user;
        }
    }
}
