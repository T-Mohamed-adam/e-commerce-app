using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos;
using TagerProject.Models.Entities;

namespace TagerProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly PasswordHashingHelper _passwordHashing;

        public AuthController(ApplicationDbContext dbContext, IConfiguration configuration, PasswordHashingHelper passwordHashing)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _passwordHashing = passwordHashing;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            // Validate input
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Email and Password are required.");
            }

            // Check user and password is matched
            var user = _dbContext.Owners.FirstOrDefault(x => x.Email == loginDto.Email);
            if (user == null || !_passwordHashing.VerifyPassword(user.Password!, loginDto.Password))
            {
                return Unauthorized(new { Message = "Bad credentials" });
            }

            // Generate token
            string? token = GenerateToken(user);

            return Ok(new { User = user, Token = token });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok(new { Message = "Logged out successfully" });
        }


        // Genrate jwt token 
        private string? GenerateToken(IUser user)
        {
            // Check configuration settings
            var jwtKey = _configuration["JwtSettings:Key"];
            var jwtIssuer = _configuration["JwtSettings:Issuer"];
            var jwtAudience = _configuration["JwtSettings:Audience"];

            if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
            {
                return null;
            }

            // Create claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwtIssuer),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("MembershipNumber", user.MembershipNumber)

            };

            // Create token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }

}
