using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using Newtonsoft.Json;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using ProjectWebMVC.Areas.User.Services;
using System.Security.Claims;
using System.Net;
using System.Net.Http;

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
            //Current User
            var user = User as ClaimsPrincipal;
            var userName = user?.FindFirstValue(ClaimTypes.Name);
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            var userData = await client.GetAsync("https://localhost:7269/api/User");
            var userDataRead = await userData.Content.ReadAsStringAsync();
            var userDataJson = JsonConvert.DeserializeObject<IEnumerable<ProjectLibrary.ObjectBussiness.User>>(userDataRead);
            //Xử lý lấy thông tin user theo userId
            var UserList = userDataJson.Where(x => x.UserId.ToString() == userId).ToList();
            // lấy usertype -- 1 member -- 2 free
            var usertype = UserList.FirstOrDefault().UserType;

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
            ViewBag.UserType = usertype;
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

        public async Task<IActionResult> DeleteRecipe(int? recipeId)
        {
            try
            {
                HttpClient client = new HttpClient();
                string ApiUrl = "https://localhost:7269/api/";

                HttpResponseMessage response1 = await client.DeleteAsync(ApiUrl + "Comment/DelByRecId/" + recipeId.ToString());
                if (!response1.IsSuccessStatusCode)
                {
                    // Xử lý lỗi 
                }
                HttpResponseMessage response2 = await client.DeleteAsync(ApiUrl + "RecipesStep/" + recipeId.ToString());
                if (!response2.IsSuccessStatusCode)
                {
                    // Xử lý lỗi 
                }
                HttpResponseMessage response3 = await client.DeleteAsync(ApiUrl + "IngredientGroup/DelByRecId/" + recipeId.ToString());
                if (!response3.IsSuccessStatusCode)
                {
                    // Xử lý lỗi 
                }
                HttpResponseMessage response4 = await client.DeleteAsync(ApiUrl + "Rating/DelByRecId/" + recipeId.ToString());
                if (!response4.IsSuccessStatusCode)
                {
                    // Xử lý lỗi 
                }
                HttpResponseMessage response5 = await client.DeleteAsync(ApiUrl + "Recipe/" + recipeId.ToString());
                if (!response5.IsSuccessStatusCode)
                {
                    // Xử lý lỗi 
                }
                //if (response.IsSuccessStatusCode)
                //{
                return RedirectToAction("UserIndex");
                //}
                //else
                //{
                //    return View("Error");
                //}
            }
            catch (Exception ex)
            {
                // Handle exceptions, set an error message, etc.
                TempData["ErrorMessage"] = "An error occurred while saving the comment.";
            }


            return RedirectToAction("UserIndex");
        }

    }
}