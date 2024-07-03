using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CargoPay.Data;
using CargoPay.Models;
using Microsoft.AspNetCore.Authorization;
using CargoPay.Helpers;

namespace CargoPay.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DbSource _context;

        public AuthController(IConfiguration configuration, DbSource context)
        {
            _configuration = configuration;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Users? user = _context.Users.Single(u => u.Username == request.Username);
            if (user == null || user.Password != PasswordHelper.HashPassword(request.Password))
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Users user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Username)
                ]),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet("checkAuth")]
        public IActionResult CheckAuth()
        {
            return Ok(new { Message = "Token is valid" });
        }
    }
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}