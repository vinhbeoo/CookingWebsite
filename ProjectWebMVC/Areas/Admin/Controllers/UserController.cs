
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.Json;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private string _userApiUrl = "";

        public UserController()
        {
            _httpClient = new HttpClient();
            var contextType = new MediaTypeWithQualityHeaderValue("application/json");
            _userApiUrl = "https://localhost:7269/api/UserManager";
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
                List<User> userList = JsonSerializer.Deserialize<List<User>>(strData, options);
                return View(userList);
            }
            else
            {
                return View(new List<User>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_userApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                User user = JsonSerializer.Deserialize<User>(strData, options);
                return View(user);
            }

            return NotFound();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(user);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(_userApiUrl, contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "User inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return View(user);
        }
        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_userApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                User user = JsonSerializer.Deserialize<User>(strData, options);
                return View(user);
            }

            return NotFound();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            var contestData = JsonSerializer.Serialize(user);
            var content = new StringContent(contestData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_userApiUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_userApiUrl}/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            User user = JsonSerializer.Deserialize<User>(data, options);
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"{_userApiUrl}/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
