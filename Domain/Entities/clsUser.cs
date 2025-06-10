using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class clsUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        // Email should be unique 
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string PhotoUrl { get; set; } = default!;
        public string FullName => $"{FirstName} {LastName}".Trim();

    }
}
