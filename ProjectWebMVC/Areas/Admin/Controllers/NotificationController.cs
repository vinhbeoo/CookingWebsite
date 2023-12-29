using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Policy;
using System.Text.Json;
using X.PagedList;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class NotificationController : Controller
    {
        // GET: NotificationController
        private readonly HttpClient _httpClient;
        private string NotificationUrl = "";

        public NotificationController()
        {
            _httpClient = new HttpClient();
            var contextType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contextType);
            NotificationUrl = "https://localhost:7269/api/Notification";
        }
        // GET: NotificationController
        public async Task<ActionResult> Index(int? id, int? page)
        {
            int pageSize = 5; // Set your desired page size

            try
            {
                HttpResponseMessage responseMessage;
                List<Notification> notificationsList;

                if (id.HasValue)
                {
                    responseMessage = await _httpClient.GetAsync($"{NotificationUrl}/{id}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var noti = JsonSerializer.Deserialize<Notification>(data, options);

                        // If a single user is found, create a list with that user
                        notificationsList = new List<Notification> { noti };
                    }
                    else
                    {
                        TempData["Message"] = "No Notification found with the specified id.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    responseMessage = await _httpClient.GetAsync(NotificationUrl);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        notificationsList = JsonSerializer.Deserialize<List<Notification>>(data, options);
                    }
                    else
                    {
                        TempData["Message"] = "No Notification found ";
                        return RedirectToAction("Index");
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1; // If page is null, default to page 1
                var pagedList = notificationsList.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                // For now, returning an empty list to the view
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<Notification>());
            }
        }

        // GET: NotificationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{NotificationUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Notification notification = JsonSerializer.Deserialize<Notification>(strData, options);
                return View(notification);
            }

            return NotFound();
        }

        // GET: NotificationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notification notification)
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            notification.Date = DateTime.Now;
            notification.UserId = int.Parse(userId.ToString());
            
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(notification);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(NotificationUrl, contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "notification inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return View(notification);
        }

        // GET: NotificationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{NotificationUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Notification notification = JsonSerializer.Deserialize<Notification>(strData, options);
                return View(notification);
            }

            return NotFound();
        }

        // POST: NotificationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Notification notification)
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            notification.Date = DateTime.Now;
            notification.UserId = int.Parse(userId.ToString());

            var notificationData = JsonSerializer.Serialize(notification);
            var content = new StringContent(notificationData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{NotificationUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(notification);
        }

        // GET: NotificationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{NotificationUrl}/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Notification notification = JsonSerializer.Deserialize<Notification>(data, options);
            return View(notification);
        }

        // POST: NotificationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{NotificationUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Notification delete successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
