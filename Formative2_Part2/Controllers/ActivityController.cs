using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Formative2_Part2.Controllers
//Event/Activity controller to handle CRUD operations and user interactions with events based on Part1's planning
{
    [Route("api/activities")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        [Authorize]// Lets all users who are logged in to view the catalog of events                    //(UserTyoe Context//)
                                                                                                        // (UserTypeId 1 = Admin, UserTypeId 2 = User)
        [HttpGet("catalog")]// Get all events in the catalog for a user that is authorized
        public IActionResult ShowAllEvents()
        {
            return Ok("showing all events.");
        }

        [Authorize(Roles = "1")] // Add a new eent that can only be done by an admin user
        [HttpPost("add-new-event")]
        public IActionResult AddNewEvent()
        {
            return Ok("New event has been added.");
        }

        [Authorize(Roles = "2")] // allows a user to enroll and participate in an event
        [HttpPost("participate-in-event")]
        public IActionResult ParticipateInEvent()
        {
            return Ok("You have been enrolled in the event.");
        }
    }
}