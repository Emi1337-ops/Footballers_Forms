using Footballers.Abstractions;
using Footballers.Models;
using Footballers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Footballers.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayersService _playersService;
        private readonly IFormViewService _formViewService;

        public PlayerController(IPlayersService playersService, IFormViewService formViewService)
        {
            _playersService = playersService;
            _formViewService = formViewService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _playersService.GetAllFootballer();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            var formViewModel = _formViewService.GetFormViewModel();
            return View(formViewModel);
        }

        public IActionResult Create(Footballer footballer) 
        {
            if (ModelState.IsValid)
            {
                _playersService.CreateFootballer(footballer);
                return RedirectToAction(nameof(GetAll));
            }

            string errorMessages = String.Empty;
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

        public IActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                _playersService.DeleteFootballer(Id.Value);
                return RedirectToAction(nameof(GetAll));
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var viewmodel = _formViewService.GetFormViewModel(id.Value);
                if (viewmodel.Footballer != null) return View(viewmodel);
            }
            return NotFound();
        }

        public IActionResult Edit(Footballer footballer)
        {
            if (ModelState.IsValid)
            {
                _playersService.EditFootballer(footballer);
                return RedirectToAction(nameof(GetAll));
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
