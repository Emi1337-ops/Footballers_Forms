using Footballers.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Footballers.Controllers
{
    public class AuthController : Controller
    {
        ApplicationContext db;
        public AuthController(ApplicationContext db) 
        { 
            this.db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, Person person)
        {
            if (person.Email is null || person.Password is null)
            {
                return BadRequest("Email и/или пароль не установлены");
            }

            var dbPerson = db.Persons.FirstOrDefault(x => x.Email == person.Email && x.Password == person.Password);
            if (dbPerson == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.Email) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        
            HttpContext.Response.Cookies.Append("Auth",  $"{token}");
            return Redirect(returnUrl ?? "/");
            
        }

        [HttpGet]
        public IActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reg(string? returnUrl, Person person)
        {
            if (person.Email is null || person.Password is null)
            {
                return BadRequest("Email и/или пароль не установлены");
            }
            if (db.Persons.Any(x => x.Email == person.Email))
            {
                return BadRequest("Email уже зарегистрирован");
            }

            db.Persons.Add(person);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("Auth");
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
