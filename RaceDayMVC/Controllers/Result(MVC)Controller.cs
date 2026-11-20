using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    // manages the MVC views and actions for race results
    public class Result_MVC_Controller : Controller
    {
        private readonly ResultServices _resultService;

        public Result_MVC_Controller(ResultServices resultService)
        {
            _resultService = resultService;
        }

 
        public async Task<IActionResult> Index() // retrieves all results from the API and returns them to the view for display while also checking if the user is authenticated with a JWT token, if not they are redirected to the login page
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var results = await _resultService.GetAllAsync(token);
            return View(results);
        }

           // displays the form to add a new result to the system, only admins
        [HttpGet]
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken")))
                return RedirectToAction("Login", "Auth");

            return View(new ResultViewModelcs());
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(ResultViewModelcs model)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid) return View(model);

            var error = await _resultService.CreateAsync(model, token);
            if (error != null)
            {
                // shos an error message to the user if the creation of a new result fails
                ModelState.AddModelError("", error);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // loads an existing result into the edit form to allow the user to make changes
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");

            var result = await _resultService.GetByIdAsync(id, token);
            if (result == null) return NotFound();

            return View(result);
        }

        // submits the edited result data to the API
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ResultViewModelcs model)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid) return View(model);

            var error = await _resultService.UpdateAsync(id, model, token);
            if (error != null)
            {
                ModelState.AddModelError("", error);
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}