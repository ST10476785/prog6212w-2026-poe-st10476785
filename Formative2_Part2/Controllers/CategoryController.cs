using Formative2_Part2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Formative2_Part2.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Formative2_Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ApplicationDBcontext _context;

        public CategoryController(ApplicationDBcontext context)
        {
            _context = context;
        }

        //POST METHOD
        [Authorize(Roles = "1")] // Authorization whereby only an admin user can add a new category to the catalog of events
        [HttpPost] // Route: POST /api/category
        public IActionResult AddCategory(Category newCategory) { // Adds a new category
        
        
        _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return Ok("A new category has been added.");

        }

        //GET METHOD
        [Authorize] // Makes sure only logged in users can view the categories
        [HttpGet] // Route: GET /api/category
        public IActionResult GetAllCategories() //Get/show al l categories in the catalog of events
        {
            var categories = _context.Categories.ToList();
            
            if (!categories.Any()) return NotFound("No categories found ");// In the case that the database is emptyy

            
            return Ok(categories);// if not return the categories in the database


        }
        //DELETE METHOD
        [Authorize(Roles = "1")] // Authorization whereby only an admin user can delete a category from the catalog of events
        [HttpDelete("{id}")] // Route: DELETE /api/category/{id}

        public IActionResult  DeleteCategory(int id) // Deletes a category
        {
            var categoryToDelete = _context.Categories.Find(id);
            if (categoryToDelete == null) return NotFound("Category not found");
            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
            return NoContent(); // Category deleted successfully .
        }


    }

}

