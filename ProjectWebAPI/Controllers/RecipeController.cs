using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.App.Code;
using ProjectWebAPI.Application;

namespace ProjectWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipeController : ControllerBase
	{
		private IRecipeRepository repository = new RecipeRepository(); // Assuming you have a RecipeRepository

		// GET: api/<RecipeController>
		[HttpGet]
		public ActionResult<IEnumerable<Recipe>> GetRecipes() => repository.GetRecipes();

		// GET: api/<RecipeController>
		[HttpGet("List/{recipeId}")]
		public ActionResult<IEnumerable<Recipe>> GetRecipeListById(int recipeId) => repository.GetRecipeListById(recipeId);

		// GET api/<RecipeController>/5
		[HttpGet("{recipeId}")]
		public ActionResult<Recipe> GetRecipeById(int recipeId)
		{
			var recipe = repository.GetRecipeById(recipeId);
			if (recipe == null)
			{
				return NotFound(); // Return 404 if the recipe is not found
			}
			return recipe;
		}

		// POST api/<RecipeController>
		[HttpPost]
		public IActionResult CreateRecipe([FromBody] RecipeDTO recipeDTO)
		{
			if (recipeDTO == null)
			{
				return BadRequest("Invalid recipe data");
			}

			// Convert from DTO to Recipe object
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
				CategoryId = recipeDTO.CategoryId,
				ContestId = recipeDTO.ContestId,
				Rating = recipeDTO.Rating,
				ReadFree = recipeDTO.ReadFree
			};

			// Call the service to add the recipe to the database
			repository.SaveRecipe(newRecipe);

			//Hàm ghi log UserActivity
			LogUserActivity.LogCommentActivity(newRecipe.Creator, newRecipe.RecipeId, "Create", "Created a new Recipe");

			// Return a success message or other necessary information
			return Ok(newRecipe.RecipeId);
		}

		// PUT api/<RecipeController>/5
		[HttpPut("{id}")]
		public IActionResult UpdateRecipe(int id, [FromBody] RecipeDTO updatedRecipeDTO)
		{
			if (updatedRecipeDTO == null || id != updatedRecipeDTO.RecipeId)
			{
				return BadRequest("Invalid recipe data");
			}

			// Check if the recipe exists
			var existingRecipe = repository.GetRecipeById(id);
			if (existingRecipe == null)
			{
				return NotFound("Recipe not found");
			}

			// Update recipe information from DTO
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
			existingRecipe.CategoryId = updatedRecipeDTO.CategoryId;
			existingRecipe.ContestId = updatedRecipeDTO.ContestId;
			existingRecipe.Rating = updatedRecipeDTO.Rating;
			existingRecipe.ReadFree = updatedRecipeDTO.ReadFree;

			// Call the service to save changes to the database
			repository.UpdateRecipe(existingRecipe);

			//Hàm ghi log UserActivity
			LogUserActivity.LogCommentActivity(existingRecipe.Creator, existingRecipe.RecipeId, "Update", "Created a new Update");

			// Return a success message or other necessary information
			return Ok("Recipe updated successfully");
		}

		// DELETE api/<RecipeController>/5
		[HttpDelete("{recipeId}")]
		public IActionResult DeleteRecipe(int recipeId)
		{
			var recipe = repository.GetRecipeById(recipeId);
			if (recipe == null)
			{
				return NotFound();
			}
			repository.DeleteRecipe(recipe);

			//Hàm ghi log UserActivity
			LogUserActivity.LogCommentActivity(recipe.Creator, recipe.RecipeId, "Delete", "Delete a new Recipe");

			return Ok("Recipe deleted successfully");
		}
	}
}
