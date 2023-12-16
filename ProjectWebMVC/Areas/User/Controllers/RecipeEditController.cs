using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
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
            ApiUrl = "https://localhost:7269/api/Recipe";
        }


        [HttpPost]
        public async Task<IActionResult> SaveRecipe(IFormFile file, Recipe rec)
        {
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            Random rnd = new Random();// + chuỗi random vào tên file để tránh trùng lặp tên ảnh trong folder
            string imageName = rnd.Next().ToString() + file.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/User/images", imageName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            rec.ImageTitle = "/User/images/" + imageName;
            rec.Creator = 2;
            rec.CreateDate = DateTime.Now;


            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(rec);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync(ApiUrl, contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Recipe inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }


            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            return View();
        }
    }


}
