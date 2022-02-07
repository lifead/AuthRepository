using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace AuthIS4_Core3_1.IdentityServer4Service.DataBase
{
    public static class DataBaseInitializer
    {
        public static void Init(this IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<IdentityUser>>();
            var user = new IdentityUser
            {
                UserName = "admin"
            };
            var result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager
                    .AddClaimAsync(user, new Claim(ClaimTypes.Role, "administrator"))
                    .GetAwaiter().GetResult();
            }
        }
    }
}
