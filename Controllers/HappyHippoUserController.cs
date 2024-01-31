using HappyHippoAPI.Data;
using HappyHippoAPI.DTO;
using HappyHippoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HappyHippoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HappyHippoUserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public HappyHippoUserController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: HappyHippoUser/register
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Username }, user);
        }

        // POST: HappyHippoUser/login
        [HttpPost("login")]
        [ActionName("GetUser")]
        public async Task<ActionResult<AccessedUser>> GetUser(User request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null || user.Password != request.Password)
            {
                return BadRequest("Username or password is wrong");
            }

            var token = CreateToken(user);

            var accessedUser = new AccessedUser
            {
                Username = request.Username,
                Token = token
            };

            return Ok(accessedUser);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
