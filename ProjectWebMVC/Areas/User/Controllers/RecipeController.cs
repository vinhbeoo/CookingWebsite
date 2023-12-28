using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectLibrary.ObjectBussiness;
using System.Security.Claims;

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
			var idData = await client.GetAsync("https://localhost:7269/api/IngredientDetail/GetByRecipeId/" + recId.ToString());
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

			//Current User
			var user = User as ClaimsPrincipal;
			var userName = user?.FindFirstValue(ClaimTypes.Name);
			var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
			ViewBag.UserName = userName;
			ViewBag.UserId = userId;
			ViewBag.RecipeId = recipeId;


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SaveComment(Comment comm)
		{
			try
			{
				HttpClient client = new HttpClient();
				string ApiUrl = "https://localhost:7269/api";

				comm.CreateDate = DateTime.Now;
				string strData = System.Text.Json.JsonSerializer.Serialize(comm);
				var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
				HttpResponseMessage res = await client.PostAsync(ApiUrl + "/Comment", contentData);
				TempData["SuccessMessage"] = "Comment saved successfully.";
			}
			catch (Exception ex)
			{
				// Handle exceptions, set an error message, etc.
				TempData["ErrorMessage"] = "An error occurred while saving the comment.";
			}


			return RedirectToAction("Index", new { recipeId = comm.RecipeId });
		}
	}
}
