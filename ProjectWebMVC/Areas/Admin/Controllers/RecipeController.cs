using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using X.PagedList;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RecipeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public RecipeController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api/Recipe";
        }
        // GET: RecipeController
        public async Task<ActionResult> Index(int? recipeId, int? page)
        {
            int pageSize = 5; // Set your desired page size

            try
            {
                HttpResponseMessage responseMessage;
                List<Recipe> recipeList;

                if (recipeId.HasValue)
                {
                    responseMessage = await _httpClient.GetAsync($"{_url}/{recipeId}");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var data = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var recipe = JsonSerializer.Deserialize<Recipe>(data, options);

                        // If a single user is found, create a list with that user
                        recipeList = new List<Recipe> { recipe };
                    }
                    else
                    {
                        TempData["Message"] = "No recipe found with the specified recipeId.";
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

                        recipeList = JsonSerializer.Deserialize<List<Recipe>>(data, options);
                    }
                    else
                    {
                        return View(new List<Recipe>());
                    }
                }

                // Apply pagination
                int pageNumber = page ?? 1; // If page is null, default to page 1
                var pagedList = recipeList.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or take appropriate action
                // For now, returning an empty list to the view
                ViewBag.ErrorMessage = "An unexpected error occurred: " + ex.Message;
                return View(new List<Recipe>());
            }
        }

        // GET: RecipeController/Details/5
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
                Recipe recipe = JsonSerializer.Deserialize<Recipe>(strData, options);
                return View(recipe);
            }

            return NotFound();
        }

        // GET: RecipeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recipe recipe)
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            recipe.Creator = int.Parse(userId.ToString());

            recipe.CreateDate = DateTime.Now;
            /*if (ModelState.IsValid)
            {*/
                string strData = JsonSerializer.Serialize(recipe);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(_url, contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Recipe inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            /*}*/
            return View(recipe);
        }

        // GET: RecipeController/Edit/5
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
                Recipe recipe = JsonSerializer.Deserialize<Recipe>(strData, options);
                return View(recipe);
            }

            return NotFound();
        }

        // POST: RecipeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe)
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            recipe.Creator = int.Parse(userId.ToString());

            recipe.CreateDate = DateTime.Now;

            var contestData = JsonSerializer.Serialize(recipe);
            var content = new StringContent(contestData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }

        // GET: RecipeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{_url}/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Recipe recipe = JsonSerializer.Deserialize<Recipe>(data, options);
            return View(recipe);
        }

        // POST: RecipeController/Delete/5
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
