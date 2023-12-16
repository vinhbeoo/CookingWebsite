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
        public async Task<IActionResult> Index(int? page)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_url);

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<UserActivity> userActivityList = JsonSerializer.Deserialize<List<UserActivity>>(strData, options);

                // Configure the pagination
                int pageSize = 5; // You can adjust the page size as needed
                int pageNumber = page ?? 1;

                // Paginate the list
                var pagedList = userActivityList.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }

            return View(new List<UserActivity>()); // Return an empty list if there's an error
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
