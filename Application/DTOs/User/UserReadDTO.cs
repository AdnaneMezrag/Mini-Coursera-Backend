using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

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
    }
}
