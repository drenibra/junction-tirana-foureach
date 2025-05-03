using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RFMoneyMatters.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Person> _userMgr;
        private readonly SignInManager<Person> _signInMgr;
        private readonly IConfiguration _config;

        public AuthController(
            UserManager<Person> userMgr,
            SignInManager<Person> signInMgr,
            IConfiguration config)
        {
            _userMgr = userMgr;
            _signInMgr = signInMgr;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest(new { error = "Passwords do not match." });

            var user = new Person { UserName = dto.Email, Email = dto.Email, Name = dto.Name };
            var result = await _userMgr.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userMgr.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized(new { error = "Invalid credentials." });

            var check = await _signInMgr.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!check.Succeeded)
                return Unauthorized(new { error = "Invalid credentials." });

            var token = GenerateJwtToken(user);
            var expiresIn = _config.GetValue<int>("Jwt:DurationInMinutes");

            return Ok(new
            {
                token,
                expiresIn
            });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<CurrentUserDto>> Me()
        {
            var user = await _userMgr.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            return Ok(new CurrentUserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
                // add other props as needed
            });
        }

        // === Helpers ===
        private string GenerateJwtToken(Person user)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:DurationInMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
