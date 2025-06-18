using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _cfg;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration cfg)
            => (_userManager, _cfg) = (userManager, cfg);

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var user = new IdentityUser { UserName = dto.UserName, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

    }
}

public record RegisterDto(string UserName, string Email, string Password);
public record LoginDto(string UserName, string Password);