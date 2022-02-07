using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(config =>
    {
        config.AddPolicy("DefaultPolicy",
            options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.Authority = "https://localhost:10001";
            options.Audience = "OrderApi";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
            };
        });


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("DefaultPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
