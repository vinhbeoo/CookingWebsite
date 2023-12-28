using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectLibrary.ObjectBussiness;
using X.PagedList;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ContestController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public ContestController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api/Contest";
        }

        // GET: ContestController
        public async Task<ActionResult> Index(int? contestId, int? page)
        {
            int pageSize = 5; // Set your desired page size

            try
            {
                HttpResponseMessage responseMessage;
                List<Contest> contestList;

                if (contestId.HasValue)
                {
                    responseMessage = await _httpClient.GetAsync($"{_url}/{contestId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var contest = JsonSerializer.Deserialize<Contest>(data, options);

                        // If a single user is found, create a list with that user
                        contestList = new List<Contest> { contest };
                    }
                    else
                    {
                        TempData["Message"] = "No contest found with the specified contestId.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    responseMessage = await _httpClient.GetAsync(_url);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        contestList = JsonSerializer.Deserialize<List<Contest>>(data, options);
                    }
                    else
                    {
                        return View(new List<Contest>());
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1; // If page is null, default to page 1
                var pagedList = contestList.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                // For now, returning an empty list to the view
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<Contest>());
            }
        }

        // GET: ContestController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Contest contest = JsonSerializer.Deserialize<Contest>(strData, options);
                return View(contest);
            }

            return NotFound();
        }

        // GET: ContestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contest contest)
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            contest.OwnerUserId = int.Parse(userId.ToString());
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(contest);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(_url, contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Contest inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return View(contest);
        }

        // GET: ContestController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Contest contest = JsonSerializer.Deserialize<Contest>(strData, options);
                return View(contest);
            }

            return NotFound();
        }

        // POST: ContestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contest contest)
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            contest.OwnerUserId = int.Parse(userId.ToString());

            var contestData = JsonSerializer.Serialize(contest);
            var content = new StringContent(contestData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(contest);
        }

        // GET: ContestController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_url}/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Contest contests = JsonSerializer.Deserialize<Contest>(data, options);
            return View(contests);
        }

        // POST: ContestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"{_url}/{id}");
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
