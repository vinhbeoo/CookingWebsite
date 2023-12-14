using Microsoft.AspNetCore.Mvc;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
