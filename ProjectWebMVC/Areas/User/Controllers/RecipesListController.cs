using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using Newtonsoft.Json;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using ProjectWebMVC.Areas.User.Services;
using System.Security.Claims;
using System.Net;

namespace ProjectWebMVC.Areas.User.Controllers
{

    [Area("User")]
    [Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = "User")]
    public class RecipesListController : Controller
    {
        private readonly INotificationService notificationService;

        public RecipesListController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task<IActionResult> Index(string? searchString, int? page)
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
            ViewBag.Recipe = recipeDataJson.Where(x => x.RecipeTitle.Contains(Search)).ToList().ToPagedList(pageNumber, 6);
            ViewBag.Category = categoryDataJson;
            ViewBag.Notifications = await this.notificationService.GetAsync();
            return View();
        }

		public async Task<IActionResult> UserIndex(string? searchString, int? page)
		{
			var user = User as ClaimsPrincipal;
			var id = user?.FindFirstValue(ClaimTypes.NameIdentifier);

			if (int.TryParse(id, out int userId))
			{
				HttpClient client = new HttpClient();

				//Recipe
				var recipeData = await client.GetAsync($"https://localhost:7269/api/Recipe/List/ByUserId/{userId}");

				if (recipeData.IsSuccessStatusCode)
				{
					// API call was successful
					var recipeDataRead = await recipeData.Content.ReadAsStringAsync();
					var recipeDataJson = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(recipeDataRead);

					//Category
					var categoryData = await client.GetAsync("https://localhost:7269/api/Category");
					var categoryDataRead = await categoryData.Content.ReadAsStringAsync();
					var categoryDataJson = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoryDataRead);

					//Phan trang
					var pageNumber = page ?? 1;

					//Search string
					var search = searchString ?? "";

					// Kiểm tra xem có công thức nào hay không
					if (recipeDataJson.Any())
					{
						// Có công thức, gửi dữ liệu đến view
						ViewBag.Recipe = recipeDataJson.Where(x => x.RecipeTitle.Contains(search)).ToList().ToPagedList(pageNumber, 6);
						ViewBag.Category = categoryDataJson;
						ViewBag.Notifications = await this.notificationService.GetAsync();
						return View();
					}
					else
					{
						// Không có công thức, gửi thông báo lỗi đến view
						ViewBag.ErrorMessage = "Không tìm thấy công thức của bạn.";
						return View();
					}
				}
				else if (recipeData.StatusCode == HttpStatusCode.NotFound)
				{
					// Không có công thức, gửi thông báo lỗi đến view
					ViewBag.ErrorMessage = "Không tìm thấy công thức của bạn.";
					// Redirect to the "Index" action of "HomeUser" controller in "User" area
					return RedirectToAction("Index", "HomeUser", new { area = "User" });
				}
				else
				{
					// Handle other error scenarios
					ViewBag.ErrorMessage = "Đã xảy ra lỗi khi gọi API.";
					return View();
				}
			}
			else
			{
				// Xử lý lỗi khi không thể chuyển đổi userId thành số nguyên
				return BadRequest("Invalid userId");
			}
		}

	}
}