using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase 
    {
        protected string GetUserEmailFromToken() => User.FindFirstValue(ClaimTypes.Email)!;
    }
}
