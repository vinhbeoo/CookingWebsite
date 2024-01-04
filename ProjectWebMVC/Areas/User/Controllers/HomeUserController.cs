using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectWebMVC.Areas.User.Services;

namespace ProjectWebMVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = "User")]
    public class HomeUserController : Controller
    {
        private readonly INotificationService notificationService;

        public HomeUserController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Notifications = await notificationService.GetAsync();
            return View();
        }
    }
}
