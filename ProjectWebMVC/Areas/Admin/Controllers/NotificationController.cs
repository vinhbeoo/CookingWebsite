using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage respon = await _httpClient.GetAsync(NotificationUrl);
            var strData = await respon.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Notification> notificationlist = JsonSerializer.Deserialize<List<Notification>>(strData, options);
            return View(notificationlist);
        }

        // GET: NotificationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotificationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notification n)
        {
            var screeninglist = JsonSerializer.Serialize(n);
            var type = new StringContent(screeninglist, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage respon = await _httpClient.PostAsync(NotificationUrl, type);
            if (respon.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NoContent();
        }

        // GET: NotificationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotificationController/Edit/5
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

        // GET: NotificationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificationController/Delete/5
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
