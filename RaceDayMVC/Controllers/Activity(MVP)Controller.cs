using Microsoft.AspNetCore.Mvc;
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
    }
}
