using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class Enrollment_MVC_Controller : Controller // Enrollment (MVC) controller class is defined to handle event enrollment management functions such as creating, retrieving, and deleting enrollments in the RaceDayMVC application
    {
        private readonly EventEnrollmentService _enrollmentService;

        public Enrollment_MVC_Controller(EventEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken"); //Uses the jwt token to check if the user is authenticated before allowing them to view the list of enrollments, if not they are redirected to the login page


            if (string.IsNullOrEmpty(token))

            {
                return RedirectToAction("Login", "Auth");
            }

            var enrollments = await _enrollmentService.GetAllAsync(token);
            return View(enrollments);
        }

        [HttpGet]
        public IActionResult Create() //Create enrollment if user is logged in and authenticated with a JWT token, if not they are redirected to the login page
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken")))

                return RedirectToAction("Login", "Auth");




            return View(new EventEnrollmentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventEnrollmentViewModel model) // asynchronously handles the creation of a new enrollment and checks if the user is authenticated with a JWT token 
        {
            var token = HttpContext.Session.GetString("JWToken");


            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");


            if (!ModelState.IsValid) return View(model);


            var error = await _enrollmentService.CreateAsync(model, token);


            if (error != null)

                   {
                ModelState.AddModelError("", error);
                return View(model);
               }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) //asynchronously handles the deletion of an enrollment by its ID and checks if the user is authenticated with a JWT token, if not they are redirected to the login page
        {
            var token = HttpContext.Session.GetString("JWToken");



            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");

            await _enrollmentService.DeleteAsync(id, token);




            return RedirectToAction("Index");
                }
    }
}