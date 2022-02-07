using AuthIS4_Core3_1.IdentityServer4Service.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthIS4_Core3_1.IdentityServer4Service.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IIdentityServerInteractionService interaction;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IIdentityServerInteractionService interaction)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.interaction = interaction;
        }

        public IActionResult Login(string ReturnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }

            bool isPersistent = false;      // запомнить пользователя
            bool lockoutOnFailure = false;  // блокировать учю запись после нескольких неудачных попыток входа
            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent, lockoutOnFailure);
            if (signInResult.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError("", "User not found");
            return View(model);
        }


        public async Task<IActionResult> Logout(string logoutId)
        {
            await signInManager.SignOutAsync();
            var logoutResult = await interaction.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrEmpty(logoutResult.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Site");
            }

            return Redirect(logoutResult.PostLogoutRedirectUri);
        }
    }
}
