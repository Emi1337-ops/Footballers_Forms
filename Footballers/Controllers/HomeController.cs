using Footballers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Footballers.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace Footballers.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;

        public HomeController(ApplicationContext db)
        {
            this.db = db;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            return View(db.Footballers.Include(x => x.Team).Include(y => y.Country).ToList());
        }


        [HttpGet]
        public IActionResult Teams()
        {
            return View(db.Teams.ToList());
        }

        [HttpPost]
        public IActionResult DeleteTeam(int? Id)
        {
            if (Id != null)
            {
                Team? team = db.Teams.FirstOrDefault(x => x.Id == Id);
                if (team != null)
                {
                    db.Teams.Remove(team);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Teams));
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult CreateFootballer()
        {
            var model = new FormViewModel()
            {
                Teams = db.Teams.ToList(),
                Countries = db.Countries.ToList(),
                Genders = new string[] {"Мужской", "Женский"}
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateFootballer(Footballer footballer)
        {
            if (ModelState.IsValid)
            {
                db.Add(footballer);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            string errorMessages = "";
            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                    }
                }
            }
            return Content(errorMessages);
        }

        [HttpPost]
        public IActionResult CreateTeam(Team Team)
        {
            if (db.Teams.FirstOrDefault(x => x.Name == Team.Name) == null)
            {
                db.Add(Team);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(CreateFootballer));
        }

        [HttpPost]
        public IActionResult DeleteFootballer(int? Id)
        {
            if (Id != null)
            {
                Footballer? footballer = db.Footballers.FirstOrDefault(x => x.Id == Id);
                if (footballer != null)
                {
                    db.Footballers.Remove(footballer);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }

        public IActionResult EditFootballer(int? id)
        {
            if (id != null)
            {
                Footballer? footballer = db.Footballers.FirstOrDefault(p => p.Id == id);
                var viewmodel = new FormViewModel()
                {
                    Footballer = footballer,
                    Teams = db.Teams.ToList(),
                    Countries = db.Countries.ToList(),
                    Genders = new string[] { "Мужской", "Женский" }
                };

                if (footballer != null) return View(viewmodel);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult EditFootballer(Footballer footballer)
        {
            if (ModelState.IsValid)
            {
                db.Footballers.Update(footballer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string errorMessages = "";
            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                    }
                }
            }
            return Content(errorMessages);
        }
    }
}