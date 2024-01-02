using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using Newtonsoft.Json;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace ProjectWebMVC.Areas.User.Controllers
{

	[Area("User")]
	[Authorize(Roles = "User")]
	[Authorize(AuthenticationSchemes = "User")]
	public class ContestRecipeListController : Controller
	{
		//private readonly HttpClient _httpClient;
		//private string _userApiUrl = "";
		public async Task<IActionResult> Index(string? searchString, int? page, int contestId)
		{

			HttpClient client = new HttpClient();
			//Recipe
			var recipeData = await client.GetAsync("https://localhost:7269/api/Recipe");
			var recipeDataRead = await recipeData.Content.ReadAsStringAsync();
			var recipeDataJson = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(recipeDataRead);
			//Category
			var categoryData = await client.GetAsync("https://localhost:7269/api/Category");
			var categoryDataRead = await categoryData.Content.ReadAsStringAsync();
			var categoryDataJson = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoryDataRead);

			//Phan trang
			var pageNumber = page ?? 1;

			//Search string
			var Search = searchString ?? "";

			//Send to view
			ViewBag.Recipe = recipeDataJson.Where(x => x.RecipeTitle.Contains(Search) && x.ContestId == contestId).ToList().ToPagedList(pageNumber, 5);
			ViewBag.Category = categoryDataJson;

			return View();
		}
	}
}
