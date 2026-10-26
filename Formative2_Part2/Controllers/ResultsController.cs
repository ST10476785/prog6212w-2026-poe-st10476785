using Formative2_Part2.Data;
using Formative2_Part2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Formative2_Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public ResultsController(ApplicationDBcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// retrieves all results from the database 
        /// </summary>
        [Authorize]
        [HttpGet] // Route: GET /api/results
        public IActionResult GetAllResults() // Retrieves all results from the database
        {
            var results = _context.Results.ToList();

            if (!results.Any()) return NotFound("No results found.");

            return Ok(results);
        }

        /// <summary>
        /// retrieves a specific result by its id
        /// </summary>
        [Authorize] // makes sure only logged in users can view specific results    
        [HttpGet("{id}")]
        public IActionResult GetResultById(int id) // Retrieves a specific result by its ID from the database
        {
            var result = _context.Results.Find(id);

            if (result == null) return NotFound("Result not found.");

            return Ok(result);
          }

        /// <summary>
        /// Adds a new result to the database
        /// </summary>
        [Authorize(Roles = "1")]// makes sure only admin users can add new results
        [HttpPost]
        public IActionResult AddNewResult([FromBody] Result newResult) // Adds a new result to the database
        {

            _context.Results.Add(newResult);

            _context.SaveChanges();

            return StatusCode(201, "New result has been added.");
        }

        /// <summary>
        /// Updates an existing result with the required id and the new result data
        /// </summary>
        [Authorize(Roles = "1")] // makes sure only admin users can update existing results
        [HttpPut("{id}")]
        public IActionResult UpdateResult(int id, [FromBody] Result updatedResult)
        {
            if (id != updatedResult.ResultId) return BadRequest("ID mismatch.");

            _context.Results.Update(updatedResult); // Updates the existing result in the database

            _context.SaveChanges();

            return NoContent();
        }
    }
    }