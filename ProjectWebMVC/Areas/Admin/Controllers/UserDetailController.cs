using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectWebAPI.Application;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserDetailController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private string UserDetailApiUrl = "";
        public UserDetailController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            UserDetailApiUrl = "https://localhost:44388/api/UserDetail";
        }
        // GET: UserDetailController
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage res = await _httpClient.GetAsync(UserDetailApiUrl);
            string strData = await res.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<UserDetail> list = JsonSerializer.Deserialize<List<UserDetail>>(strData, option);
            return View(list);
        }

        // GET: UserDetailController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{UserDetailApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UserDetail userDetail = JsonSerializer.Deserialize<UserDetail>(strData, options);
                return View(userDetail);
            }
            return View();
        }

        // GET: UserDetailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDetailDTO userDetailDTO)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(userDetailDTO);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(UserDetailApiUrl, contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "UserDetail inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return View(userDetailDTO);
        }

        // GET: UserDetailController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{UserDetailApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UserDetail userDetail = JsonSerializer.Deserialize<UserDetail>(strData, options);
                return View(userDetail);
            }
            return View();
        }

        // POST: UserDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDetailDTO userDetailDTO)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(userDetailDTO);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PutAsync($"{UserDetailApiUrl}/{id}", contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "UserDetail updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return View(userDetailDTO);
        }

        // GET: UserDetailController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{UserDetailApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UserDetail userDetail = JsonSerializer.Deserialize<UserDetail>(strData, options);
                return View(userDetail);
            }
            return View();
        }

        // POST: UserDetailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage res = await _httpClient.DeleteAsync($"{UserDetailApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                TempData["Message"] = "UserDetail deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Message"] = "Error while call Web API";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
