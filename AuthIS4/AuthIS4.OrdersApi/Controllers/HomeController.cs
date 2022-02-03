using Microsoft.AspNetCore.Mvc;

namespace AuthIS4.OrdersApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
