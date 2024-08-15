using Microsoft.AspNetCore.Mvc;
using Footballers.Models;
using Footballers.ViewModels;
using Footballers.Abstractions;

namespace Footballers.Controllers
{ 
    public class TeamController : Controller
    {
        private readonly ITeamsService _teamsService;

        public TeamController(ITeamsService teamsService) 
        {
            _teamsService = teamsService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return View(_teamsService.GetAllTeams());
        }

        public IActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                _teamsService.DeleteTeam(Id);
                return RedirectToAction(nameof(GetAll));
            }
            return NotFound();
        }

        public IActionResult Create(Team Team)
        {
            _teamsService.CreateTeam(Team);
            return RedirectToAction("Create", "Player");
            //return Redirect(nameof(Create));
        }
    }
}
