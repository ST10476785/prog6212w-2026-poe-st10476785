using Formative2_Part2.Data;
using Formative2_Part2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Formative2_Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase // Enrollment controller class is defined to handle enrollment management functions such as creating, retrieving, and deleting enrollments in the Formative2_Part2 application
    {
        private readonly ApplicationDBcontext _context;

        public EnrollmentController(ApplicationDBcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// retrieves all enrollments from the database
        /// </summary>
        [Authorize]

        [HttpGet]
        public IActionResult GetAllEnrollments() 
              {
            var enrollments = _context.EventEnrollments.ToList();

            return Ok(enrollments);
        }

        /// <summary>
        /// retrieves a specific enrollment by its id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetEnrollmentById(int id) // Route: GET /api/enrollment/{id}
        {
            var enrollment = _context.EventEnrollments.Find(id);// find specific enrollment by id

            if (enrollment == null) return NotFound("Enrollment not found ");

            return Ok(enrollment);
        }

        /// <summary>
        /// adds a new enrollment to the database with the event details provided in the request body
        /// </summary>
        [Authorize]
        [HttpPost]
        public IActionResult AddNewEnrollment([FromBody] EventEnrollment newEnrollment)
        {


            _context.EventEnrollments.Add(newEnrollment);// adds a new enrollment to the database 

            _context.SaveChanges();

            return StatusCode(201, "New enrollment has been added.");
        }

        /// <summary>
        /// deletes an enrollment by its id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteEnrollment(int id)
        {
            var enrollmentToDelete = _context.EventEnrollments.Find(id); // deletes an enrollment

            if (enrollmentToDelete == null) return NotFound("Enrollment not found.");

            _context.EventEnrollments.Remove(enrollmentToDelete);

            _context.SaveChanges();

            return NoContent();
        }
    }
}