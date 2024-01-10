using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using Newtonsoft.Json;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using ProjectWebMVC.Areas.User.Services;

namespace ProjectWebMVC.Areas.User.Controllers
{

	[Area("User")]
	[Authorize(Roles = "User")]
	[Authorize(AuthenticationSchemes = "User")]
	public class ContestsListController : Controller
	{
        private readonly INotificationService notificationService;

        public ContestsListController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        //private readonly HttpClient _httpClient;
        //private string _userApiUrl = "";
        public async Task<IActionResult> Index(string? searchString, int? page)
		{

			HttpClient client = new HttpClient();
			//Contest
			var contestData = await client.GetAsync("https://localhost:7269/api/Contest");
			var contestDataRead = await contestData.Content.ReadAsStringAsync();
			var contestDataJson = JsonConvert.DeserializeObject<IEnumerable<Contest>>(contestDataRead);
			
			//Phan trang
			var pageNumber = page ?? 1;

			//Search string
			var Search = searchString ?? "";

			//Send to view
			ViewBag.Contest = contestDataJson.Where(x => x.ContestName.Contains(Search)).ToList().ToPagedList(pageNumber, 5);

            ViewBag.Notifications = await this.notificationService.GetAsync();

            return View();
		}
	}
}
