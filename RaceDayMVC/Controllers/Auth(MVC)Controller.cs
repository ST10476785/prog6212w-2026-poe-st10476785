using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class AuthController : Controller // Auth (MVP) controller class is defined to handle user authentication and registration functions
    {
        private readonly AuthenticationInterface _authService;

        public AuthController(AuthenticationInterface authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("LoginAuth"); // return the login view to the user when they navigate to the login page
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginAuth", model); // return the login view with the model data if the model state is invalid to show a user incorrectly filled out the login form
            }

            var token = await _authService.LoginAsync(model);

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View("LoginAuth", model); // return this error message to the user if the login attempt fails due to incorrect email or password
            }

            var role = GetRoleFromToken(token);

            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("UserEmail", model.Email);
            HttpContext.Session.SetString("UserRole", role);

            return RedirectToAction("Index", "Home"); // return to the home page if the login attempt is successful
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("RegisterAuth");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterAuth", model); // return the register view with the model data if the model state is invalid to show a user incorrectly filled out the registration form
            }

            var result = await _authService.RegisterAsync(model);

            if (result.Success) // if successful registration prompt this message and redirect the user to the login page to login with their new credentials
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", result.Message);
            return View("RegisterAuth", model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login"); // if a user logs out of the application the session is cleared and the user is redirected to the login page
        }

        private string GetRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;

            return role ?? "Participant";
        }
    }
}