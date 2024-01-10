using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RatingController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public RatingController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api/Rating";
        }
        public async Task<IActionResult> Index(int? recipeId, int? page)
        {
            int pageSize = 5;

            try
            {
                List<Rating> ratingList;

                // Kiểm tra xem có giá trị recipeId không
                if (recipeId.HasValue)
                {
                    // Nếu có recipeId, thực hiện truy vấn tìm kiếm
                    HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_url}/rating/{recipeId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        ratingList = JsonSerializer.Deserialize<List<Rating>>(data, options);
                    }
                    else
                    {
                        TempData["Message"] = "No Rating found with the specified recipeId.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Nếu không có recipeId, thực hiện truy vấn phân trang
                    HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_url}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        ratingList = JsonSerializer.Deserialize<List<Rating>>(data, options);
                    }
                    else
                    {
                        // Xử lý trường hợp không thành công nếu cần
                        return View(new List<Rating>());
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1;
                var pagedList = ratingList.ToPagedList(pageNumber, pageSize);

                // Lưu giữ giá trị recipeId để sử dụng trong phân trang
                ViewBag.RecipeId = recipeId;

                return View(pagedList);

            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<Rating>());
            }
        }


        // GET: RatingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RatingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RatingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RatingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RatingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RatingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RatingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
