using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectWebMVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = "User")]
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
        public ActionResult Details(int id)
        {
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

        // GET: UserDetailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserDetailController/Edit/5
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

        // GET: UserDetailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserDetailController/Delete/5
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
