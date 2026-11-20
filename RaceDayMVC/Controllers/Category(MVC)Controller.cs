using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class Category_MVC_Controller : Controller // Category (MVC) controller class is defined to handle category management functions such as creating, editing, and deleting categories in the RaceDayMVC application
    {

        private readonly CategoryServices _categoryService;

        public Category_MVC_Controller(CategoryServices categoryService)
        {
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index() // This method retrieves all categories from the API and returns them to the view for display, it also checks if the user is an admin and sets a ViewBag property accordingly
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.IsAdmin = HttpContext.Session.GetString("UserRole") == "1";
            var categories = await _categoryService.GetAllAsync(token);

            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken"))) // Chceks if user is authenticated by checking if the JWT token is present in the session, if not redirects to login page
                return RedirectToAction("Login", "Auth");

            if (HttpContext.Session.GetString("UserRole") != "1")
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModelcs model)
        {
            var token = HttpContext.Session.GetString("JWToken"); // Checks authentication of user with JWT in order for them to create a new category
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");
            if (HttpContext.Session.GetString("UserRole") != "1") return RedirectToAction("Index");

            if (!ModelState.IsValid) return View(model);

            var success = await _categoryService.CreateAsync(model, token);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to create category.");
                return View(model);
            }

            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id) // checks if the user is authenticated and authorized to edit a category, retrieves the category by its ID from the API, and returns it to the view for editing
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");
            if (HttpContext.Session.GetString("UserRole") != "1") return RedirectToAction("Index");

            var category = await _categoryService.GetByIdAsync(id, token);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryViewModelcs model)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");
            if (HttpContext.Session.GetString("UserRole") != "1") return RedirectToAction("Index");

            if (!ModelState.IsValid) return View(model);

            model.CategoryId = id;
            var error = await _categoryService.UpdateAsync(id, model, token);
            if (error != null)
            {
                ModelState.AddModelError("", error); // error message is added to the model state if the update fails, and the user is returned to the edit view with the model data to correct any issues
                return View(model);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id) // checks if the user is authenticated and auhtorized with a JWT token to delete a category, and sends a request to the API to delete the category by its ID
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");
            if (HttpContext.Session.GetString("UserRole") != "1") return RedirectToAction("Index");

            await _categoryService.DeleteAsync(id, token);
            return RedirectToAction("Index");
        }


    }

}