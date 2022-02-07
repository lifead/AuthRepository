using AuthIS4_Core3_1.IdentityServer4Service.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthIS4_Core3_1.IdentityServer4Service
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(config =>
                    {
                        config.UseInMemoryDatabase("MEMORY");
                    })
                .AddIdentity<IdentityUser, IdentityRole>(config =>
                      {
                          config.Password.RequireDigit = false;
                          config.Password.RequireLowercase = false;
                          config.Password.RequireUppercase = false;
                          config.Password.RequireNonAlphanumeric = false;
                          config.Password.RequiredLength = 3;
                      })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.ConfigureApplicationCookie(config =>
            //{
            //    config.LoginPath = "/Auth/Login";
            //    config.AccessDeniedPath = "/Auth/AccessDenied";
            //});


            services.AddIdentityServer(config =>
                    {
                        config.UserInteraction.LoginUrl = "/Auth/Login";
                        config.UserInteraction.LogoutUrl = "/Auth/Logout";
                    })
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryClients(AuthConfiguration.GetClients())
                .AddInMemoryApiResources(AuthConfiguration.GetApiResources())
                .AddInMemoryIdentityResources(AuthConfiguration.GetIdentityResources())
                .AddInMemoryApiScopes(AuthConfiguration.ApiScopes())
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
