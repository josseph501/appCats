using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.DTOs
{
    public record RegisterUserDto(string Email, string Password);
    public record LoginUserDto(string Email, string Password);
}
