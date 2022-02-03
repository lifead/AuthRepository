using Microsoft.AspNetCore.Mvc;

namespace AuthIS4.UsersApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
