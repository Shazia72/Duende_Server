using CMS.Model;
using CMS.Repository;
using CMS.ViewModels;
using CMSApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CMSApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Register1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUserViewModel user)
        {
            if (ModelState.IsValid)
            {
               await _context.ApplicationUsers.AddAsync(GetUser(user));
                _context.SaveChanges();
                return View("Login");
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index),"Post");
            //var model = new ApplicationUser();
            //return View(model);
        }
        private ApplicationUser GetUser(ApplicationUserViewModel vm)
        {
            var user = new ApplicationUser();
            user.UserName = vm.FirstName+ " "+vm.LastName;
            user.Password = vm.Password;
            user.Email = vm.Email;
            user.AccessToken = null;
            user.UserPhone = vm.UserPhone;
            user.IsAdmin = false;
            return user;
        }

        [Route("PostLogin")]
        [HttpPost]
        public async Task<IActionResult> PostLogin(ApplicationUser _userData) // [FromBody] to check on Postman
        {
            if (_userData != null && HttpContext.Session.GetString("Token") == null)
            {
                var resultLoginCheck = await _context.ApplicationUsers
                    .Where(e => e.Email == _userData.Email && e.Password == _userData.Password)
                    .FirstOrDefaultAsync();
                if (resultLoginCheck == null)
                {
                    return BadRequest("Invalid Credentials");
                }
                else
                {
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", resultLoginCheck.Id.ToString()),
                    new Claim("UserName", resultLoginCheck.UserName),
                    new Claim("Email", resultLoginCheck.Email)
                };


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddSeconds(10),
                        signingCredentials: signIn);


                    _userData.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    HttpContext.Session.SetString("Token", _userData.AccessToken);
                    HttpContext.Session.SetString("Email", _userData.Email);
                    return RedirectToAction("Index", "Post");
                }
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}