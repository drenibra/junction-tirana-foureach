using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Interface;
using RFMoneyMatters.Models;
using RFMoneyMatters.Services;
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
        private readonly JwtSettings _jwtSettings;
        private readonly TokenService _tokenService;

        public AuthController(
            UserManager<Person> userMgr,
            SignInManager<Person> signInMgr,
            TokenService tokenService,
            IConfiguration config,
            IOptions<JwtSettings> jwtOptions)
        {
            _userMgr = userMgr;
            _signInMgr = signInMgr;
            _tokenService = tokenService;
            _config = config;
            _jwtSettings = jwtOptions.Value;
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
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userMgr.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInMgr.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return await CreateUserObject(user);
            }
            ;

            return Unauthorized();
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
            if (string.IsNullOrWhiteSpace(_jwtSettings.Secret))
                throw new InvalidOperationException("JWT Secret is not configured.");

            // Now it’s safe:
            var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var signingKey = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name ?? ""),
                // etc.
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<UserDto> CreateUserObject(Person user)
        {
            var roles = await _userMgr.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };
        }
    }
}
