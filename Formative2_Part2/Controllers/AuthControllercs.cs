using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Formative2_Part2.Data;
using Formative2_Part2.DTOs;
using Formative2_Part2.Entities;

namespace Formative2_Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public AuthController(ApplicationDBcontext context)
        {
            _context = context;
        }

        [HttpPost("register")] // HttpPost attribute specifies that this action responds to POST requests at the route "api/auth/register"
        public IActionResult Register(UserRegisterDto request)
        {
      
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);


            var newUser = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = passwordHash,
                UserTypeId = request.UserTypeId
            };

          
            _context.Users.Add(newUser);
            _context.SaveChanges();

  
            return Ok("User registered successfully.");
        }
    }
}