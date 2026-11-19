using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class Activity_MVP_Controller : Controller
    {
        private readonly ActivityInterface _activityService;

        public Activity_MVP_Controller(ActivityInterface activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var activities = await _activityService.GetAllAsync();
            return View(activities);
        }
    }
}
