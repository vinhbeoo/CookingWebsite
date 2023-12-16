using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectLibrary.ObjectBussiness;

namespace ProjectWebMVC.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = "User")]
    public class RecipeController : Controller
	{
		[Route("Recipe/{recipeId?}")]
		public async Task<IActionResult> Index(int? recipeId)
		{
			int recId = recipeId ?? 14;


			HttpClient client = new HttpClient();
			//Recipe
			var recipeData = await client.GetAsync("https://localhost:7269/api/Recipe/List/" + recId.ToString());
			var recipeDataRead = await recipeData.Content.ReadAsStringAsync();
			var recipeDataJson = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(recipeDataRead);
			ViewBag.Recipe = recipeDataJson;

			//Ingredients group
			var igData = await client.GetAsync("https://localhost:7269/api/IngredientGroup/GetByRecipeId/" + recId.ToString());
			var igDataRead = await igData.Content.ReadAsStringAsync();
			var igDataJson = JsonConvert.DeserializeObject<IEnumerable<IngredientsGroup>>(igDataRead);
			ViewBag.IngredientGroup = igDataJson;

			//Ingredients detail
			var idData = await client.GetAsync("https://localhost:7269/api/IngredientDetail");
			var idDataRead = await idData.Content.ReadAsStringAsync();
			var idDataJson = JsonConvert.DeserializeObject<IEnumerable<IngredientsDetail>>(idDataRead);
			ViewBag.IngredientDetail = idDataJson;

			//Recipe Step
			var recStepData = await client.GetAsync("https://localhost:7269/api/RecipesStep/List/" + recId.ToString());
			var recStepDataRead = await recStepData.Content.ReadAsStringAsync();
			var recStepDataJson = JsonConvert.DeserializeObject<IEnumerable<RecipesStep>>(recStepDataRead);
			ViewBag.RecipeStep = recStepDataJson;

			//Comment
			var CommData = await client.GetAsync("https://localhost:7269/api/Comment/CommentByRecipe/" + recId.ToString());
			var CommDataRead = await CommData.Content.ReadAsStringAsync();
			var CommDataJson = JsonConvert.DeserializeObject<IEnumerable<Comment>>(CommDataRead);
			ViewBag.Comment = CommDataJson;


			return View();
		}
	}
}
