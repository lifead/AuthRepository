using Authorization.Roles.Models.CustomClaims;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie")
    .AddCookie("Cookie", config =>
    {
        config.LoginPath = "/Auth/Login";
        config.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(AuthClaims.RoleAdministrator, conf =>
        {
            conf.RequireRole(ClaimTypes.Role, AuthClaims.RoleAdministrator);

            options.AddPolicy("ManagerPolicy", conf =>
            {
                conf.RequireAssertion(x =>
                       x.User.HasClaim(ClaimTypes.Role, AuthClaims.RoleManager)
               //     || x.User.HasClaim(ClaimTypes.Role, AuthClaims.RoleAdministrator)
                    );
            });
        });
    });

builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
