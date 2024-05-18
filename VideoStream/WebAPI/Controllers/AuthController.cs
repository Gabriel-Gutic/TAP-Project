using Asp.Versioning;
using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IAppLogger _logger;

        public AuthController(IAuthService authService, IConfiguration configuration, IAppLogger logger)
        {
            _authService = authService;
            _configuration = configuration;
            _logger = logger;
        }

        // Get the data for the user from JWTToken
        [Authorize]
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            var currentUser = HttpContext.User;

            string? username = null;

            if (currentUser.HasClaim(c =>
            {
                if (c.Type == ClaimTypes.NameIdentifier)
                {
                    username = c.Value;
                    return true;
                }
                return false;
            }))
            {
                UserDto? user = _authService.FindUser(username);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user);
            }
            return NotFound("User not found");
        }

        // Generate Token (1 year valability) for a valid username login
        [HttpPost("Token")]
        public IActionResult GenerateToken(AuthDto authDto)
        {
            UserDto? userDto = _authService.FindUser(authDto.Username, authDto.Password);

            if (userDto == null)
            {
                return NotFound("Incorrect username or password");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Username),
                new Claim(ClaimTypes.Role, userDto.IsAdmin ? "Admin" : "Client" ),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(365),
                signingCredentials: creds);

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
