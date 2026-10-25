using Formative2_Part2.Data;
using Formative2_Part2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Formative2_Part2.Controllers//Event/Activity controller to handle CRUD operations and user interactions with events based on Part1's planning
{
    [Route("api/events")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public ActivityController(ApplicationDBcontext context)
        {
            _context = context;
        }

        [Authorize]// Lets all users who are logged in to view the catalog of events                    //(UserTyoe Context//)
                                                                                                 // (UserTypeId 1 = Admin, UserTypeId 2 = User)
        [HttpGet]// Get all events in the catalog for a user that is authorized
        public IActionResult ShowAllEvents()   //  / Route: GET /api/events
        {
            var events = _context.Events.ToList();

            if (!events.Any()) return NotFound("No events found.");

            return Ok(events);
        }

        [Authorize] // Makes sure only logged in users can view specific events
        [HttpGet("{id}")]
        public IActionResult GetEventById(int id) // Get a specific event 
        {
            var singleEvent = _context.Events.Find(id);

            if (singleEvent == null) return NotFound("Event not found.");

            return Ok(singleEvent);
        }

        [Authorize(Roles = "1")] // Add a new eent that can only be done by an admin user
        [HttpPost]
        public IActionResult AddNewEvent([FromBody] Event newEvent)  // // Route: POST /api/events
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            return StatusCode(201, "New event has been added.");
        }

        [Authorize(Roles = "1")] //Makes sure only an admin user can update an event
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event updatedEvent) // Route: PUT /api/events/{id}
        {
            if (id != updatedEvent.EventId) return BadRequest("ID mismatch.");

            _context.Events.Update(updatedEvent);
            _context.SaveChanges();

            return Ok("Event updated successfully.");
        }

        [Authorize(Roles = "1")] // Makes sure only an admin user can delete an event 
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id) // Route: DELETE /api/events/{id}
        {
            var eventToDelete = _context.Events.Find(id);
            if (eventToDelete == null) return NotFound("Event not found.");

            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();

            return NoContent(); // Event deleted successfully
        }
    }
}