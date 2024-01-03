using CMS.Model;
using CMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public LoginController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken(ApplicationUser user)
        {
            if (user.AccessToken != null) {
                return Ok(user.Email);
            }
            return BadRequest();
        }

        [Route("PostLogin")]
        [HttpPost]
        public async Task<IActionResult> PostLogin([FromForm] ApplicationUser _userData) // [FromBody] to check on Postman
        {
            if(_userData!=null && HttpContext.Session.GetString("Token") == null)
            {
                var resultLoginCheck = _context.ApplicationUsers
                    .Where(e => e.Email == _userData.Email && e.Password == _userData.Password)
                    .FirstOrDefault();
                if(resultLoginCheck==null)
                {
                    return BadRequest("Invalid Credentials");
                }
                else
                {
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", _userData.Id.ToString()),
                    new Claim("UserName", _userData.UserName),
                    new Claim("Email", _userData.Email)
                };


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddSeconds(60),
                        signingCredentials: signIn);


                    _userData.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    HttpContext.Session.SetString("Token", _userData.AccessToken);
                    HttpContext.Session.SetString("Email", _userData.Email);
                    return Ok(_userData);
                }
            }
            else
            {
                return NoContent();
            }
        }
    }
}
