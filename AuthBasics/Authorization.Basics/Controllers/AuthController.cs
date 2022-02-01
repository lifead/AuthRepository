using Authorization.Basics.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authorization.Basics.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var claims = new List<Claim> {
                new Claim("Demo", "Value")
            };
            ClaimsIdentity claimIdentity = new(claims, "Cookie");
            ClaimsPrincipal claimsPrincipal = new(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimsPrincipal);

            return Redirect(model.ReturnUrl);
        }


        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Index");
        }

    }
}
