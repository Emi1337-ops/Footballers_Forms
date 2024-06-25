using Footballers.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Footballers.Controllers
{
    public class AuthController : Controller
    {
        ApplicationContext db;
        public AuthController(ApplicationContext db ) 
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
            // создаем объект ClaimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
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

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.Email) };
            // создаем объект ClaimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Redirect(returnUrl ?? "/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
