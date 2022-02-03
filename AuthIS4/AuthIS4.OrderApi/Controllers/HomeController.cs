using Microsoft.AspNetCore.Mvc;

namespace AuthIS4.OrderApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
