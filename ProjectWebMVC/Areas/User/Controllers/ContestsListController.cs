using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using Newtonsoft.Json;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using ProjectWebMVC.Areas.User.Services;
using System.Security.Claims;

namespace ProjectWebMVC.Areas.User.Controllers
{

	[Area("User")]
	[Authorize(Roles = "User")]
	[Authorize(AuthenticationSchemes = "User")]
	public class ContestsListController : Controller
	{
        private readonly INotificationService notificationService;

        public ContestsListController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        //private readonly HttpClient _httpClient;
        //private string _userApiUrl = "";
        public async Task<IActionResult> Index(string? searchString, int? page)
		{

			HttpClient client = new HttpClient();
            //Current User
            var user = User as ClaimsPrincipal;
            var userName = user?.FindFirstValue(ClaimTypes.Name);
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            //Contest
            var contestData = await client.GetAsync("https://localhost:7269/api/Contest");
			var contestDataRead = await contestData.Content.ReadAsStringAsync();
			var contestDataJson = JsonConvert.DeserializeObject<IEnumerable<Contest>>(contestDataRead);
			
			//Phan trang
			var pageNumber = page ?? 1;

			//Search string
			var Search = searchString ?? "";

            //Recipe
            var recipeData = await client.GetAsync("https://localhost:7269/api/Recipe");
            var recipeDataRead = await recipeData.Content.ReadAsStringAsync();
            var recipeDataJson = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(recipeDataRead);
            //Xử lý recipe theo contestId
            var recipeList = recipeDataJson.Where(x => x.ContestId != null && x.Creator.ToString() == userId).ToList();

            //Send to view
            ViewBag.Contest = contestDataJson.Where(x => x.ContestName.Contains(Search)).ToList().ToPagedList(pageNumber, 5);
            ViewBag.RecipeList = recipeList;
            ViewBag.Notifications = await this.notificationService.GetAsync();
            ViewBag.UserId = userId;

            return View();
		}


        public async Task<IActionResult> ViewRecipe(int? contestId)
        {
            HttpClient client = new HttpClient();
            //Current User
            var user = User as ClaimsPrincipal;
            var userName = user?.FindFirstValue(ClaimTypes.Name);
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            //Recipe
            var recipeData = await client.GetAsync("https://localhost:7269/api/Recipe");
            var recipeDataRead = await recipeData.Content.ReadAsStringAsync();
            var recipeDataJson = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(recipeDataRead);
            //Xử lý recipe theo contestId
            var recipeList = recipeDataJson.Where(x =>  x.ContestId == contestId && x.Creator.ToString() == userId).ToList();
            var recID = recipeList.FirstOrDefault().RecipeId;

            return RedirectToAction("Index", "Recipe", new { recipeID = recID });
        }

        public async Task<IActionResult> FirstRecipe(int? contestId)
        {
            HttpClient client = new HttpClient();
            //Current User
            var user = User as ClaimsPrincipal;
            var userName = user?.FindFirstValue(ClaimTypes.Name);
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            //Recipe
            var recipeData = await client.GetAsync("https://localhost:7269/api/Recipe");
            var recipeDataRead = await recipeData.Content.ReadAsStringAsync();
            var recipeDataJson = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(recipeDataRead);
            //Xử lý recipe theo contestId
            var recipeList = recipeDataJson.Where(x => x.ContestId == contestId && x.Creator.ToString() == userId).ToList();

            //Rating
            var RatingData = await client.GetAsync("https://localhost:7269/api/Rating");
            var RatingDataRead = await RatingData.Content.ReadAsStringAsync();
            var RatingDataJson = JsonConvert.DeserializeObject<IEnumerable<Rating>>(RatingDataRead);

            //Xử lý sum vote từ rating theo IdRecipe
            var totalVoteByRecipe = RatingDataJson
            .GroupBy(rating => rating.RecipeId)
            .Select(group => new
            {
                RecipeId = group.Key,
                TotalVote = group.Sum(rating => rating.Vote)
            });
            // join totalVoteByRecipe vào recipeList
            var recipesWithVote = recipeList
                .GroupJoin(totalVoteByRecipe,
                      recipe => recipe.RecipeId,
                      rating => rating?.RecipeId,
                      (recipe, rating) => new
                      {
                          RecipeId = recipe.RecipeId,
                          Createdate = recipe.CreateDate,
                          RecipeTitle = recipe.RecipeTitle,
                          ImageTitle = recipe.ImageTitle,
                          Description = recipe.Description,
                          TotalVote = rating?.FirstOrDefault()?.TotalVote ?? 0
                      });

            var RecipeData = recipesWithVote
                .GroupJoin(RatingDataJson,
                      recipe => recipe.RecipeId,
                      rating => rating?.RecipeId,
                      (recipe, rating) => new
                      {
                          RecipeId = recipe.RecipeId,
                          Createdate = recipe.Createdate,
                          RecipeTitle = recipe.RecipeTitle,
                          ImageTitle = recipe.ImageTitle,
                          Description = recipe.Description,
                          TotalVote = recipe.TotalVote,
                          Voted = rating?.Any(r => r.UserId == int.Parse(userId)) == true ? userId : ""
                      })
                .OrderByDescending(r => r.TotalVote)
                .OrderBy(r => r.Createdate);
            //string RecipeDataJson = JsonConvert.SerializeObject(RecipeData);
            //var RecipeDataList = JsonConvert.DeserializeObject<List<object>>(RecipeDataJson);





            var recID = RecipeData.FirstOrDefault().RecipeId;
            return RedirectToAction("Index", "Recipe", new { recipeID = recID });
        }


    }
}
