using Microsoft.AspNetCore.Authorization;
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
    public class UserResHistoryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public UserResHistoryController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api/UserResHistory";
        }

        // GET: UserRegHistoryController

        public async Task<IActionResult> Index(int? userId, int? page)
        {
            int pageSize = 5;

            try
            {
                List<UserRegHistory> userList;

                // Kiểm tra xem có giá trị userId không
                if (userId.HasValue)
                {
                    // Nếu có userId, thực hiện truy vấn tìm kiếm
                    HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_url}/user/{userId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        userList = JsonSerializer.Deserialize<List<UserRegHistory>>(data, options);
                    }
                    else
                    {
                        TempData["Message"] = "No user register history found with the specified id.";
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

                        userList = JsonSerializer.Deserialize<List<UserRegHistory>>(data, options);
                    }
                    else
                    {
                        // Xử lý trường hợp không thành công nếu cần
                        return View(new List<UserRegHistory>());
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1;
                var pagedList = userList.ToPagedList(pageNumber, pageSize);

                // Lưu giữ giá trị userId để sử dụng trong phân trang
                ViewBag.userId = userId;

                return View(pagedList);

            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<UserRegHistory>());
            }
        }
    }
}
