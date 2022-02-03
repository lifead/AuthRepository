using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
    //{
    //    config.Authority = "https://localhost:10001";
    //    config.Audience = "OrderApi";
    //});
    .AddJwtBearer(options =>
        {
            options.Authority = "https://localhost:10001";
            options.Audience = "OrderApi";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                //ValidateIssuer = false,
                ValidateAudience = false,
                //ValidateLifetime = false,
            };
        });


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
