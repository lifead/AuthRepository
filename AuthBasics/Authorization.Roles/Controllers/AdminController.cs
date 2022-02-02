using Authorization.Roles.Models.CustomClaims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Roles.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = AuthClaims.RoleAdministrator)]
        public IActionResult Administrator()
        {
            return View();
        }

        [Authorize(Policy = "ManagerPolicy")]
        public IActionResult Manager()
        {
            return View();
        }

    }
}
