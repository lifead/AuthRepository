using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthIS4.OrdersApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public string GetSecret()
        {
            return $"Secret string from {nameof(AuthIS4)}.{nameof(OrdersApi)}.{nameof(HomeController)}.{nameof(GetSecret)}";
        }
    }
}
