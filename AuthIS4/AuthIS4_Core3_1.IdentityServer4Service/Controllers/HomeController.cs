using Microsoft.AspNetCore.Mvc;

namespace AuthIS4_Core3_1.IdentityServer4Service.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
