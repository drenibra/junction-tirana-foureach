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
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userMgr.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();

            var result = await _signInMgr.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();

            // 1) create the JWT
            var token = _tokenService.CreateToken(user);

            // 2) create secure, HTTP-only cookie options
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,                                    // set false for HTTP (dev), true for HTTPS (prod)
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Path = "/"
            };

            // 3) append it
            Response.Cookies.Append("token", token, cookieOptions);

            // 4) return whatever you need in the body (you can omit the raw token now)
            return Ok(new
            {
                id = user.Id,
                email = user.Email,
                username = user.UserName,
                role = (await _userMgr.GetRolesAsync(user)).FirstOrDefault()
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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInMgr.SignOutAsync();

            Response.Cookies.Delete(
                "token",
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Path = "/"
                }
            );


            return Ok(new { message = "Logout successful." });
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
