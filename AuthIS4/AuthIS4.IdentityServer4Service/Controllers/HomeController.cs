using Microsoft.AspNetCore.Mvc;

namespace AuthIS4.IdentityServer4Service.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
