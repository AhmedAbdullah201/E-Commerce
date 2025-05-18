using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.AuthenticationDto
{
    public record RegisterRequest(string Email, string DispalyName , string Password , string? UserName = "MMM", string? PhoneNumber = "MMM");
}
