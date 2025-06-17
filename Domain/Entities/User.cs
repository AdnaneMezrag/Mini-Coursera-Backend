using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public UserTypeEnum UserType { get; set; } = default!; 
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        // Email should be unique 
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? PhotoUrl { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();

    }
}
