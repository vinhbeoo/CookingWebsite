using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Security.Claims;
using System.Text.Json;

namespace ProjectWebMVC.Controllers
{
	[Area("User")]
	[Authorize(Roles = "User")]
	[Authorize(AuthenticationSchemes = "User")]
	public class RecipeEditController : Controller
	{
		private readonly HttpClient client = null;
		private string ApiUrl = "";
		public RecipeEditController()
		{
			client = new HttpClient();
			//var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			//client.DefaultRequestHeaders.Accept.Add(contentType);
			ApiUrl = "https://localhost:7269/api";
		}







		[HttpPost]
		public async Task<IActionResult> SaveRecipe(IFormFile file, Recipe rec, List<RecipesStep> recipeStep, List<IngredientsGroup> ingGroup, List<IngredientsDetail> ingDetail)
		{
			// kiểm tra hình ảnh
			if (file == null || file.Length == 0)
			{
				return Content("file not selected");
			}

			// luu file vao thu muc
			Random rnd = new Random();// + chuỗi random vào tên file để tránh trùng lặp tên ảnh trong folder
			string imageName = rnd.Next().ToString() + file.FileName;
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/User/images", imageName);

			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}


			// thiet lap cac gia tri mac dinh
			rec.ImageTitle = "/User/images/" + imageName;



			var user = User as ClaimsPrincipal;
			var userName = user?.FindFirstValue(ClaimTypes.NameIdentifier);

			rec.Creator = int.Parse(userName.ToString());
			rec.CreateDate = DateTime.Now;



			//if (ModelState.IsValid)
			//{
			// add vào bảng recipe
			string strData = JsonSerializer.Serialize(rec);
			var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
			HttpResponseMessage res = await client.PostAsync(ApiUrl + "/Recipe", contentData);
			// HttpResponseMessage res = await client.PostAsync(ApiUrl + $"?userId={rec.Creator}", contentData);



			if (res.IsSuccessStatusCode)
			{
				TempData["Message"] = "Recipe inserted successfully";
				var result = await res.Content.ReadAsStringAsync();
				// add vào bảng recipe step
				foreach (var recStep in recipeStep)
				{
					recStep.RecipeId = int.Parse(result.ToString());
					recStep.ImageUrl = "";

					string strData1 = JsonSerializer.Serialize(recStep);
					var contentData1 = new StringContent(strData1, System.Text.Encoding.UTF8, "application/json");
					HttpResponseMessage res1 = await client.PostAsync(ApiUrl + "/RecipesStep", contentData1);
					var result2 = await res1.Content.ReadAsStringAsync();
				}

				foreach (var iGr in ingGroup)
				{
					iGr.RecipeId = int.Parse(result.ToString());

					string strData2 = JsonSerializer.Serialize(iGr);
					var contentData2 = new StringContent(strData2, System.Text.Encoding.UTF8, "application/json");
					HttpResponseMessage res2 = await client.PostAsync(ApiUrl + "/IngredientGroup", contentData2);
					var result2 = await res2.Content.ReadAsStringAsync();

					foreach (var iDt in ingDetail)
					{
						if (iGr.IngredientId == iDt.IngredientId)
						{
							iDt.IngredientId = iGr.IngredientId;
							iDt.RecipeId = int.Parse(result.ToString());
							iDt.Description = iDt.Description.ToString();

							string strData3 = JsonSerializer.Serialize(iDt);
							var contentData3 = new StringContent(strData3, System.Text.Encoding.UTF8, "application/json");
							HttpResponseMessage res3 = await client.PostAsync(ApiUrl + "/IngredientDetail", contentData3);
							var result3 = await res3.Content.ReadAsStringAsync();
						}

					}

				}
				return RedirectToAction("Index", "Recipe", new { recipeId = int.Parse(result.ToString()) });

				//// add vào bảng Ingredient 
				//string strData2 = JsonSerializer.Serialize(ingGroup);
				//var contentData2 = new StringContent(strData2, System.Text.Encoding.UTF8, "application/json");
				//HttpResponseMessage res2 = await client.PostAsync(ApiUrl+ "/IngredientGroup", contentData2);

				//string strData3 = JsonSerializer.Serialize(ingDetail);
				//var contentData3 = new StringContent(strData3, System.Text.Encoding.UTF8, "application/json");
				//HttpResponseMessage res3 = await client.PostAsync(ApiUrl + "/IngredientDetail", contentData3);

				//return RedirectToAction(nameof(Index));
			}
			else
			{
				TempData["Message"] = "Error while call Web API";
				return RedirectToAction("Index");

			}




			//}



		}


		public IActionResult Index()
		{
			return View();
		}
	}


}
