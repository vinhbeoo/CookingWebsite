using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Diagnostics;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class TagController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public TagController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api/Tag";
        }

        // GET: TagController
        public async Task<ActionResult> Index(int? TagId, int? page)
        {
            int pageSize = 5; // Set your desired page size

            try
            {
                HttpResponseMessage responseMessage;
                List<Tag> tagList;

                if (TagId.HasValue)
                {
                    responseMessage = await _httpClient.GetAsync($"{_url}/{TagId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var tag = JsonSerializer.Deserialize<Tag>(data, options);

                        // If a single user is found, create a list with that user
                        tagList = new List<Tag> { tag };
                    }
                    else
                    {
                        TempData["Message"] = "No tag found with the specified tagId.";
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

                        tagList = JsonSerializer.Deserialize<List<Tag>>(data, options);
                    }
                    else
                    {
                        return View(new List<Tag>());
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1; // If page is null, default to page 1
                var pagedList = tagList.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                // For now, returning an empty list to the view
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<Tag>());
            }
        }

        // GET: TagController/Details/5
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
                Tag tag = JsonSerializer.Deserialize<Tag>(strData, options);
                return View(tag);
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
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(tag);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(_url, contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Tag inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return View(tag);
        }

        // GET: TagController/Edit/5
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
                Tag tag = JsonSerializer.Deserialize<Tag>(strData, options);
                return View(tag);
            }

            return NotFound();
        }

        // POST: TagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tag tag)
        {
            var contestData = JsonSerializer.Serialize(tag);
            var content = new StringContent(contestData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        // GET: TagController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_url}/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Tag tag = JsonSerializer.Deserialize<Tag>(data, options);
            return View(tag);
        }

        // POST: TagController/Delete/5
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
