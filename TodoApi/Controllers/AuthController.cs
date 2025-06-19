using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var dto = new
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                email = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue("email"),
                name = User.Identity?.Name
            };

            return Ok(dto);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var user = new IdentityUser { UserName = dto.UserName, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid Credentials");

            string token = GenerateJwt(user);
            return Ok(new { token });
        }

        private string GenerateJwt(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id), //sub
                new Claim(ClaimTypes.Name, user.UserName!), //name
                new Claim(ClaimTypes.Email, user.Email!) //email
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["JwtSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                claims: claims,
                audience: _cfg["JwtSettings:Audience"],
                issuer: _cfg["JwtSettings:Issuer"],
                expires: DateTime.UtcNow.AddHours(int.Parse(_cfg["JwtSettings:ExpiresHours"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}

public record RegisterDto(string UserName, string Email, string Password);
public record LoginDto(string UserName, string Password);