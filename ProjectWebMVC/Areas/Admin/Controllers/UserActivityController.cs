using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class UserActivityController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public UserActivityController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api/UserActivity";
        }

        // GET: UserActivityController

        public async Task<IActionResult> Index(int? userId, int? page)
        {
            int pageSize = 5;

            try
            {
                List<UserActivity> userList;

                // Kiểm tra xem có giá trị userId không
                if (userId.HasValue)
                {
                    // Nếu có userId, thực hiện truy vấn tìm kiếm
                    HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_url}/{userId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        userList = JsonSerializer.Deserialize<List<UserActivity>>(data, options);
                    }
                    else
                    {
                        TempData["Message"] = "No user acvity found with the specified userId.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Nếu không có userId, thực hiện truy vấn phân trang
                    HttpResponseMessage responseMessage = await _httpClient.GetAsync(_url);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        userList = JsonSerializer.Deserialize<List<UserActivity>>(data, options);
                    }
                    else
                    {
                        // Xử lý trường hợp không thành công nếu cần
                        return View(new List<UserActivity>());
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1;
                var pagedList = userList.ToPagedList(pageNumber, pageSize);

                // Lưu giữ giá trị userId để sử dụng trong phân trang
                ViewBag.UserId = userId;

                return View(pagedList);

            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<UserActivity>());
            }
        }


        // GET: UserActivityController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserActivityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserActivityController/Create
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

        // GET: UserActivityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserActivityController/Edit/5
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

        // GET: UserActivityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserActivityController/Delete/5
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
