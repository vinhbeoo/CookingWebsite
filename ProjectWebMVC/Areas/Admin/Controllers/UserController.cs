
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Policy;
using System.Text.Json;
using X.PagedList;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private string _userApiUrl = "";
        private string _userUrl = "https://localhost:7269/api/User";
        private string _userDetailUrl = "https://localhost:7269/api/UserDetail";
        public UserController()
        {
            _httpClient = new HttpClient();
            var contextType = new MediaTypeWithQualityHeaderValue("application/json");
            _userApiUrl = "https://localhost:7269/api/UserManager";
        }

        public async Task<ActionResult> Index(string input, int? page)
        {
            int pageSize = 5; // Set your desired page size

            try
            {
                HttpResponseMessage responseMessage;
                List<ProjectLibrary.ObjectBussiness.User> userList;
                if (1==2)
                {

                }   
                else
                {

                }
                if (!string.IsNullOrEmpty(input))
                {
                    responseMessage = await _httpClient.GetAsync($"{_userUrl}/{input}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var user = JsonSerializer.Deserialize<ProjectLibrary.ObjectBussiness.User>(data, options);

                        // If a single user is found, create a list with that user
                        userList = new List<ProjectLibrary.ObjectBussiness.User> { user };

                        // Check if the user list is empty and display an error message
                        if (userList.Count == 0)
                        {
                            TempData["Message"] = "Không tìm thấy";
                            return RedirectToAction("Index"); // Redirect to the original view
                        }
                    }
                    else
                    {
                        TempData["Message"] = "No user found with the specified input.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    responseMessage = await _httpClient.GetAsync(_userApiUrl);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        userList = JsonSerializer.Deserialize<List<ProjectLibrary.ObjectBussiness.User>>(data, options);

                        // Check if the user list is empty and display an error message
                        if (userList.Count == 0)
                        {
                            TempData["Message"] = "Không tìm thấy";
                            return RedirectToAction("Index"); // Redirect to the original view
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Không tìm thấy";
                        return RedirectToAction("Index"); // Redirect to the original view
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1; // If page is null, default to page 1
                var pagedList = userList.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                // For now, returning an empty list to the view
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<ProjectLibrary.ObjectBussiness.User>());
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
                ProjectLibrary.ObjectBussiness.User user = JsonSerializer.Deserialize<ProjectLibrary.ObjectBussiness.User>(strData, options);
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
        public async Task<IActionResult> Create(ProjectLibrary.ObjectBussiness.User user)
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
                ProjectLibrary.ObjectBussiness.User user = JsonSerializer.Deserialize<ProjectLibrary.ObjectBussiness.User>(strData, options);
                return View(user);
            }

            return NotFound();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectLibrary.ObjectBussiness.User user)
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
            ProjectLibrary.ObjectBussiness.User user = JsonSerializer.Deserialize<ProjectLibrary.ObjectBussiness.User>(data, options);
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // Gửi yêu cầu xóa cho UserDetail trước
                HttpResponseMessage responseMessageUserDetail = await _httpClient.DeleteAsync($"{_userDetailUrl}/{id}");

                HttpResponseMessage responseMessageUser = await _httpClient.DeleteAsync($"{_userApiUrl}/{id}");

                // Kiểm tra xem yêu cầu xóa User có thành công hay không
                if (responseMessageUser.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Xử lý trường hợp không xóa thành công User
                    TempData["ErrorMessage"] = "Xóa User không thành công.";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                // Xử lý lỗi và ghi log nếu cần
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }



    }
}
