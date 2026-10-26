using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Formative2_Part2.Data;
using Formative2_Part2.Entities;
using Microsoft.AspNetCore.Authorization;
namespace Formative2_Part2.Controllers
{
    [Route("api/[controller]")] // localhost:port/api/user{id}
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public UserController(ApplicationDBcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a specific user by their id
        /// </summary>
        [Authorize] // This makes sure only a logged in User can get their specific account details
        [HttpGet("/api/user/{id}")]

        public IActionResult GetSpecificUser(int id) // Route: GET /api/user/{id}
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        /// <summary>
        /// Updates an existing user's details through their id and new details
        /// </summary>
        [Authorize] //This makes sure only a logged in user can update their specific account details
        [HttpPut("/api/users/{id}")]


        public IActionResult UpdateSpecificUser(int id, [FromBody] User updatedUser) // Route: PUT /api/user/{id}
        {
            if (id != updatedUser.UserId) return BadRequest("Invalid request current details have remained unchanged");
            _context.Users.Update(updatedUser);

            _context.SaveChanges();

            return Ok("User details have been updated successfully.");
        }


    }
}