using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private IRecipeRepository repository = new RecipeRepository();

        // GET: api/<TypeController>
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetRecipes() => repository.GetRecipes();

        // GET api/<TypeController>/5
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipeById(int id)
        {
            var recipe = repository.GetRecipeById(id);
            if (recipe == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }
            return recipe;
        }

        // POST api/<TypeController>
        [HttpPost]
        public IActionResult CreateRecipe([FromBody] RecipeDTO recipeDTO)
        {
            if (recipeDTO == null)
            {
                return BadRequest("Invalid recipe data");
            }

            // Chuyển đổi từ DTO sang đối tượng Contest
            var newRecipe = new Recipe
            {
                RecipeId = recipeDTO.RecipeId,
                RecipeTitle = recipeDTO.RecipeTitle,
                ImageTitle = recipeDTO.ImageTitle,
                Creator = recipeDTO.Creator,
                CreateDate = recipeDTO.CreateDate,
                TagId = recipeDTO.TagId,
                Description = recipeDTO.Description,
                VideoUrl = recipeDTO.VideoUrl,
                PrepTime = recipeDTO.PrepTime,
                CookTime = recipeDTO.CookTime,
                TotalTime = recipeDTO.TotalTime,
                Servings = recipeDTO.Servings,
                Calories = recipeDTO.Calories,
                Fat = recipeDTO.Fat,
                Carbs = recipeDTO.Carbs,
                Protein = recipeDTO.Protein,
                TypeId = recipeDTO.TypeId,
                ContestId = recipeDTO.ContestId,
                Rating = recipeDTO.Rating
            };

            // Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
            repository.SaveRecipe(newRecipe);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Recipe created successfully");
        }

        // PUT api/<TypeController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRecipe(int id, [FromBody] RecipeDTO updatedRecipeDTO)
        {
            if (updatedRecipeDTO == null || id != updatedRecipeDTO.RecipeId)
            {
                return BadRequest("Invalid tag data");
            }

            // Kiểm tra xem cuộc thi có tồn tại không
            var existingRecipe = repository.GetRecipeById(id);
            if (existingRecipe == null)
            {
                return NotFound("Recipe not found");
            }

            // Cập nhật thông tin cuộc thi từ DTO
            existingRecipe.RecipeTitle = updatedRecipeDTO.RecipeTitle;
            existingRecipe.ImageTitle = updatedRecipeDTO.ImageTitle;
            existingRecipe.Creator = updatedRecipeDTO.Creator;
            existingRecipe.CreateDate = updatedRecipeDTO.CreateDate;
            existingRecipe.TagId = updatedRecipeDTO.TagId;
            existingRecipe.Description = updatedRecipeDTO.Description;
            existingRecipe.VideoUrl = updatedRecipeDTO.VideoUrl;
            existingRecipe.PrepTime = updatedRecipeDTO.PrepTime;
            existingRecipe.CookTime = updatedRecipeDTO.CookTime;
            existingRecipe.TotalTime = updatedRecipeDTO.TotalTime;
            existingRecipe.Servings = updatedRecipeDTO.Servings;
            existingRecipe.Calories = updatedRecipeDTO.Calories;
            existingRecipe.Fat = updatedRecipeDTO.Fat;
            existingRecipe.Carbs = updatedRecipeDTO.Carbs;
            existingRecipe.Protein = updatedRecipeDTO.Protein;
            existingRecipe.TypeId = updatedRecipeDTO.TypeId;
            existingRecipe.ContestId = updatedRecipeDTO.ContestId;
            existingRecipe.Rating = updatedRecipeDTO.Rating;

            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            repository.UpdateRecipe(existingRecipe);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Recipe updated successfully");
        }

        // DELETE api/<TypeController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            var temp = repository.GetRecipeById(id);
            if (temp == null)
            {
                return NotFound();
            }
            repository.DeleteRecipe(temp);
            return NoContent();
        }
    }
}
