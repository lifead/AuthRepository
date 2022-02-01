var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie")
    .AddCookie("Cookie", config =>
    {
        config.LoginPath = "/Auth/Login";
    });
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
