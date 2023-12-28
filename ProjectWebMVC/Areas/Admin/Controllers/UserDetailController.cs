using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectLibrary.ObjectBussiness;
using ProjectWebAPI.Application;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class UserDetailController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private string UserDetailApiUrl = "";
        public UserDetailController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            UserDetailApiUrl = "https://localhost:7269/api/UserDetail";
        }
        // GET: UserDetailController
        public async Task<ActionResult> Index(int? userId, int? page)
        {
            int pageSize = 5; // Set your desired page size

            try
            {
                HttpResponseMessage responseMessage;
                List<UserDetail> userList;

                if (userId.HasValue)
                {
                    responseMessage = await _httpClient.GetAsync($"{UserDetailApiUrl}/{userId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var user = JsonSerializer.Deserialize<UserDetail>(data, options);

                        // If a single user is found, create a list with that user
                        userList = new List<UserDetail> { user };
                    }
                    else
                    {
                        TempData["Message"] = "No userDetail found with the specified userId.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    responseMessage = await _httpClient.GetAsync(UserDetailApiUrl);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        userList = JsonSerializer.Deserialize<List<UserDetail>>(data, options);
                    }
                    else
                    {
                        return View(new List<UserDetail>());
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
                return View(new List<UserDetail>());
            }
        }

        // GET: UserDetailController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                TempData["Message"] = "Invalid UserDetail ID.";
                return RedirectToAction("Index", "UserDetail", new { message = "invalidid" });
            }

            HttpResponseMessage res = await _httpClient.GetAsync($"{UserDetailApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UserDetail userDetail = JsonSerializer.Deserialize<UserDetail>(strData, options);

                if (userDetail != null)
                {
                    return View(userDetail);
                }
                else
                {
                    TempData["Message"] = "UserDetail not found.";
                    return RedirectToAction("Index", "UserDetail", new { message = "notfound" });
                }
            }

            TempData["Message"] = "Error retrieving UserDetail.";
            return RedirectToAction("Index", "User", new { message = "error" });
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
