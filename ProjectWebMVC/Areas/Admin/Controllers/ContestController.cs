using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

public class ContestController : Controller
{
    private readonly HttpClient _httpClient;
    private string ContestUrl = "https://localhost:7269/api/Contest";

    public ContestController()
    {
        _httpClient = new HttpClient();
        var contextType = new MediaTypeWithQualityHeaderValue("application/json");
        _httpClient.DefaultRequestHeaders.Accept.Add(contextType);
    }

    public async Task<IActionResult> Index()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(ContestUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        List<Contest> contestList = JsonSerializer.Deserialize<List<Contest>>(strData, options);
        return View(contestList);
    }

    public async Task<IActionResult> Details(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ContestUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Contest contest = JsonSerializer.Deserialize<Contest>(strData, options);

            return Json(new { success = true, data = contest });
        }

        return Json(new { success = false, message = "Contest not found" });
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> Create([FromBody] Contest contest)
    {
        if (ModelState.IsValid)
        {
            var contestData = JsonSerializer.Serialize(contest);
            var contentData = new StringContent(contestData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(ContestUrl, contentData);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Contest inserted successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error while calling Web API" });
            }
        }
        return Json(new { success = false, message = "Invalid model state" });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ContestUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Contest contest = JsonSerializer.Deserialize<Contest>(strData, options);
            return Json(new { success = true, data = contest });
        }
        return Json(new { success = false, message = "Error while calling Web API" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [FromBody] Contest contest)
    {
        if (ModelState.IsValid)
        {
            string contestData = JsonSerializer.Serialize(contest);
            var contentData = new StringContent(contestData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{ContestUrl}/{id}", contentData);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Contest updated successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error while calling Web API" });
            }
        }
        return Json(new { success = false, message = "Invalid model state" });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ContestUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Contest contest = JsonSerializer.Deserialize<Contest>(strData, options);
            return Json(new { success = true, data = contest });
        }
        return Json(new { success = false, message = "Error while calling Web API" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, IFormCollection collection)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{ContestUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return Json(new { success = true, message = "Contest deleted successfully" });
        }
        return Json(new { success = false, message = "Error while calling Web API" });
    }
}
