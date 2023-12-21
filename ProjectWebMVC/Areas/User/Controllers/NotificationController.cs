
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectLibrary.ObjectBussiness;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProjectWebMVC.Areas.User.Controllers
{
    [Area("User")]
	[Authorize(Roles = "User")]
	[Authorize(AuthenticationSchemes = "User")]
	public class NotificationController : Controller
    {
        private readonly HttpClient _httpClient;
        private string _userApiUrl = "https://localhost:7269/api/Notification"; // Kiểm tra và điều chỉnh URL

        public NotificationController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_userApiUrl);
            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Notification> userList = JsonSerializer.Deserialize<List<Notification>>(strData, options);
                return View(userList);
            }
            else
            {
                return View(new List<Notification>());
            }
        }

        public async Task<IActionResult> Dropdown()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_userApiUrl);
            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Notification> notifications = JsonSerializer.Deserialize<List<Notification>>(strData, options);

                // Lấy 4 thông báo mới nhất
                List<Notification> top4Notifications = notifications.OrderByDescending(n => n.Date).Take(4).ToList();

                return View("Dropdown", top4Notifications);
            }
            else
            {
                return View("Dropdown", new List<Notification>());
            }
        }
    }
}

