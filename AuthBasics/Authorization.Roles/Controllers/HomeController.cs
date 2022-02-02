using Microsoft.AspNetCore.Mvc;

namespace Authorization.Roles.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Name = User.Identity?.Name ?? "Unknown user";
            ViewBag.IsAuthenticated = User.Identity?.IsAuthenticated ?? false;

            return View();
        }
    }
}
