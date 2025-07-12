﻿using Domain.Enums;

namespace Application.DTOs.User
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public UserTypeEnum UserType { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        // Email should be unique 
        public string Email { get; set; } = default!;
        public string? PhotoUrl { get; set; }
        public string? Token { get; set; }
    }
}
