using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using X.PagedList;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class CommentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public CommentController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api/Comment";
        }

        // GET: CommentController
        public async Task<ActionResult> Index(int? commentId, int? page)
        {
            int pageSize = 5; // Set your desired page size

            try
            {
                HttpResponseMessage responseMessage;
                List<Comment> commentsList;

                if (commentId.HasValue)
                {
                    responseMessage = await _httpClient.GetAsync($"{_url}/{commentId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var comment = JsonSerializer.Deserialize<Comment>(data, options);

                        // If a single user is found, create a list with that user
                        commentsList = new List<Comment> { comment };
                    }
                    else
                    {
                        TempData["Message"] = "No comment found with the specified commentId.";
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

                        commentsList = JsonSerializer.Deserialize<List<Comment>>(data, options);
                    }
                    else
                    {
                        TempData["Message"] = "No comment found with the specified commentId.";
                        return RedirectToAction("Index");
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1; // If page is null, default to page 1
                var pagedList = commentsList.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                // For now, returning an empty list to the view
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<Comment>());
            }
        }

        // GET: CommentController/Details/5
        public async Task<IActionResult> Details(int commentId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{commentId}");

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Comment comment = JsonSerializer.Deserialize<Comment>(strData, options);
                return View(comment);
            }

            TempData["Message"] = "No comment found with the specified commentId.";
            return RedirectToAction("Index");
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                var user = User as ClaimsPrincipal;
                var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

                comment.CreateDate = DateTime.Now;
                comment.UserId = int.Parse(userId.ToString());
                string strData = JsonSerializer.Serialize(comment);
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
            return View(comment);
        }

        // GET: CommentController/Edit/5
        public async Task<IActionResult> Edit(int commentId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{commentId}");

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Comment comment = JsonSerializer.Deserialize<Comment>(strData, options);
                return View(comment);
            }

            TempData["Message"] = "No comment found with the specified commentId.";
            return RedirectToAction("Index");
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int commentId, Comment comment)
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            comment.CreateDate = DateTime.Now;
            comment.UserId = int.Parse(userId.ToString());

            var commenttData = JsonSerializer.Serialize(comment);
            var content = new StringContent(commenttData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/{commentId}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(comment);
        }

        // GET: CommentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_url}/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Comment comment = JsonSerializer.Deserialize<Comment>(data, options);
            return View(comment);
        }

        // POST: CommentController/Delete/5
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
