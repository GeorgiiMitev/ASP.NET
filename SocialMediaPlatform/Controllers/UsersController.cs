using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Dtos;
using SocialMediaPlatform.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(AppDbContext _context, IConfiguration _configuration) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            //EXAMPLE
            //Without Include: Blog will be null
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            // С Include: EF Core will make a Join and it will fill Blog
            //var user = await _context.Users
            //   .Include(u => u.Blog)
            //    .FirstOrDefaultAsync(u => u.Id == id);

            return await _context.Users
                //.Include(u => u.Blog) -> here it is not required
                .Select(u => new UserReadDto(u.Id, u.Username, u.Blog != null ? u.Blog.Name : null))
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto dto)
        {
            var user = new User { Username = dto.Username, Password = dto.Password };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id },
                new UserReadDto(user.Id, user.Username, null));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCreateDto login)
        {
            //Irl the password is hashed and it checks the login user
            if (login.Username == "admin" && login.Password == "password")
            {
                var token = GenerateJwtToken(login.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                //this logic can be extended by getting from the login user the Role and adding it as a claim
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
