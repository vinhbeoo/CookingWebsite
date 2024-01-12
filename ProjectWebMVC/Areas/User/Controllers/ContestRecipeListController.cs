using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using Newtonsoft.Json;
using System.Text.Json;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using System.Security.Claims;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ProjectWebMVC.Areas.User.Services;

namespace ProjectWebMVC.Areas.User.Controllers
{

	[Area("User")]
	[Authorize(Roles = "User")]
	[Authorize(AuthenticationSchemes = "User")]
	public class ContestRecipeListController : Controller
	{
        private readonly INotificationService notificationService;

        public ContestRecipeListController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task<IActionResult> Index(string? searchString, int? page, int contestId, int end)
		{

            HttpClient client = new HttpClient();
            //Phan trang
            var pageNumber = page ?? 1;
            //Search string
            var Search = searchString ?? "";
            //Current User
            var user = User as ClaimsPrincipal;
            var userName = user?.FindFirstValue(ClaimTypes.Name);
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);


            //Recipe
            var recipeData = await client.GetAsync("https://localhost:7269/api/Recipe");
            var recipeDataRead = await recipeData.Content.ReadAsStringAsync();
            var recipeDataJson = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(recipeDataRead);
            //Xử lý recipe theo contestId
            var recipeList = recipeDataJson.Where(x => x.RecipeTitle.Contains(Search) && x.ContestId == contestId).ToList().ToPagedList(pageNumber, 5);

            //Category
            var categoryData = await client.GetAsync("https://localhost:7269/api/Category");
            var categoryDataRead = await categoryData.Content.ReadAsStringAsync();
            var categoryDataJson = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoryDataRead);


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
                          RecipeTitle = recipe.RecipeTitle,
                          ImageTitle = recipe.ImageTitle,
                          Description = recipe.Description,
                          TotalVote = recipe.TotalVote,
                          Voted = rating?.Any(r => r.UserId == int.Parse(userId)) == true ? userId : ""
                      });
                //.OrderByDescending(r => r.TotalVote);

            string RecipeDataJson = JsonConvert.SerializeObject(RecipeData);
            var RecipeDataList = JsonConvert.DeserializeObject<List<object>>(RecipeDataJson);

            //Send to view
            ViewBag.Recipe = RecipeDataList;
            ViewBag.Recipe4Page = recipeList;
            ViewBag.UserName = userName;
            ViewBag.UserId = userId;
            ViewBag.ContestId = contestId;
            ViewBag.End = end;

            ViewBag.Notifications = await this.notificationService.GetAsync();
            return View();

        }


        //Vote Contest Recipe
        [HttpPost]
        public async Task<IActionResult> VoteRecipe(int recipeId, int vote, int contestId)
        {
            //Current User
            var user = User as ClaimsPrincipal;
            var userName = user?.FindFirstValue(ClaimTypes.Name);
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            HttpClient client = new HttpClient();
            string ApiUrl = "https://localhost:7269/api/Rating";

            if (vote == 1)
            {
                var ratingList = new List<Rating>();
                ratingList.Add(new Rating
                {
                    RatingId = 0,
                    UserId = int.Parse(userId.ToString()),
                    RecipeId = recipeId,
                    Vote = vote
                });
                foreach (var rate in ratingList)
                {
                    string strData = JsonSerializer.Serialize(rate);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PostAsync(ApiUrl, contentData);
                }


            }
            else
            {
                string recId = recipeId.ToString();
                HttpResponseMessage res = await client.DeleteAsync(ApiUrl + "/" + userId + "/" + recId);
            }

            return RedirectToAction("Index", "ContestRecipeList", new { contestId = contestId });


        }
    }
}
