using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class Activity_MVP_Controller : Controller // A controller class named Activity_MVP_Controller is defined
    {
        private readonly ActivityInterface _activityService;

        public Activity_MVP_Controller(ActivityInterface activityService)
        {
            _activityService = activityService;// here the ActivityInterface is injected into the controller's constructor which allows the controller to use the methods defined in the interface to interact with the underlying data 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var activities = await _activityService.GetAllAsync();
            return View(activities); // returns events to the view for display
        }

        //Dedicated section for role-based access control to  ensure that only users with the appropriate role can access certain actions within the controller.
        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "1") //Only role 1/ Admin can create events, if not redirected to index page
            {
                return RedirectToAction("Index");
                 }

            return View();
               }


        [HttpPost]
        public async Task<IActionResult> Create(ActivityViewModel model) // This method handles the creation of a new event ,it checks the user's role, validates the model, and then calls the service to create the event
        {
            if (HttpContext.Session.GetString("UserRole") != "1")
            {
                return RedirectToAction("Index");
                      
            
            }

            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth"); //Use of a token based authentication mechanism to ensure that the user is authenticated before allowing them to create a new event
            }

            var success = await _activityService.CreateAsync(model, token);

            if (success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create event.");
            return View(model); // error if details are not valid or if the creation fails for any reason
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "1")
            {
               
                return RedirectToAction("Index");
            }

            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
               
                return RedirectToAction("Login", "Auth"); // Use of a token based authentication mechanism to ensure that the user is authenticated before allowing them to edit an event
            }

            var activity = await _activityService.GetByIdAsync(id, token);
            if (activity == null)
            {
               
                return NotFound();
            }

            return View(activity);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ActivityViewModel model) // This method handles the editing of an existing event
        {
            if (HttpContext.Session.GetString("UserRole") != "1")
            {
                
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var token = HttpContext.Session.GetString("JWToken"); // Use of a token based authentication mechanism to ensure that the user is authenticated before allowing them to edit an event
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var success = await _activityService.UpdateAsync(id, model, token);

            if (success)
            {
                
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to update event.");
            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> Delete(int id) // in order to delete an event the user must be an authenticated admin
        {
            if (HttpContext.Session.GetString("UserRole") != "1")
            {
                return RedirectToAction("Index");
            }

            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            await _activityService.DeleteAsync(id, token);
            return RedirectToAction("Index");
        }
         }
}