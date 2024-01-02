using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository repository = new CategoryRepository(); // Assuming you have a CategoryRepository

        // GET: api/<CategoryController>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories() => repository.GetCategories();


       

        // GET api/<CategoryController>/5
        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetCategoryById(int categoryId)
        {
            var category = repository.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound(); // Return 404 if the category is not found
            }
            return category;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid category data");
            }

            var newCategory = new Category()
            {
                CategoryId = categoryDTO.CategoryId,
                CategoryName = categoryDTO.CategoryName,
            };
            // Call the service to add the category to the database
            repository.SaveCategory(newCategory);

            // Return a success message or other necessary information
            return Ok("Category created successfully");
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDTO updatedCategoryDTO)
        {
            if (updatedCategoryDTO == null || categoryId != updatedCategoryDTO.CategoryId)
            {
                return BadRequest("Invalid category data");
            }

            // Check if the category exists
            var existingCategory = repository.GetCategoryById(categoryId);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            // Update category information
            existingCategory.CategoryId = updatedCategoryDTO.CategoryId;
            existingCategory.CategoryName = updatedCategoryDTO.CategoryName;

            // Call the service to save changes to the database
            repository.UpdateCategory(existingCategory);

            // Return a success message or other necessary information
            return Ok("Category updated successfully");
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            var category = repository.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            repository.DeleteCategory(category);
            return Ok("Category deleted successfully");
        }
    }
}
