using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RefreshToken
{
    public class RefreshTokenRequestDTO
    {
        public string RefreshToken { get; set; } = default!;
        public int UserId { get; set; } = default!; 
    }
}
