using Microsoft.AspNetCore.Authorization;
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
    public class WinnerInfoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public WinnerInfoController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api/WinnerInfo";
        }

        // GET: WinnerInfoController
        public async Task<ActionResult> Index(int? winnerId, int? page)
        {
            int pageSize = 5; // Set your desired page size

            try
            {
                HttpResponseMessage responseMessage;
                List<WinnerInfo> winnerInfoList;

                if (winnerId.HasValue)
                {
                    responseMessage = await _httpClient.GetAsync($"{_url}/{winnerId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var winnerInfo = JsonSerializer.Deserialize<WinnerInfo>(data, options);

                        // If a single user is found, create a list with that user
                        winnerInfoList = new List<WinnerInfo> { winnerInfo };
                    }
                    else
                    {
                        TempData["Message"] = "No winnerInfo found with the specified winnerId.";
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

                        winnerInfoList = JsonSerializer.Deserialize<List<WinnerInfo>>(data, options);
                    }
                    else
                    {
                        return View(new List<WinnerInfo>());
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1; // If page is null, default to page 1
                var pagedList = winnerInfoList.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                // For now, returning an empty list to the view
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<WinnerInfo>());
            }
        }

        public async Task<IActionResult> Details(int contestId)
        {
            if (contestId <= 0)
            {
                TempData["Message"] = "Invalid Winner Info ID.";
                return RedirectToAction("Index", "WinnerInfo", new { message = "invalidid" });
            }

            HttpResponseMessage res = await _httpClient.GetAsync($"{_url}/Recipe/{contestId}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                WinnerInfo winnerInfo = JsonSerializer.Deserialize<WinnerInfo>(strData, options);

                if (winnerInfo != null)
                {
                    return View(winnerInfo);
                }
                else
                {
                    TempData["Message"] = "WinnerInfo not found.";
                    return RedirectToAction("Index", "WinnerInfo", new { message = "notfound" });
                }
            }

            TempData["Message"] = "Error retrieving WinnerInfo.";
            return RedirectToAction("Index", "Contest", new { message = "error" });
        }
    }
}
