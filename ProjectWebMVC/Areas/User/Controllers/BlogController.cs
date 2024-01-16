using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectWebMVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = "User")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
