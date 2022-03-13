namespace FlightManager.Web
{
    using System.Reflection;

    using FlightManager.Data;
    using FlightManager.Data.Common;
    using FlightManager.Data.Common.Repositories;
    using FlightManager.Data.Models;
    using FlightManager.Data.Repositories;
    using FlightManager.Data.Seeding;
    using FlightManager.Services.Mapping;
    using FlightManager.Services.Messaging;
    using FlightManager.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using FlightManager.Web.Infrastucture.Extensions;
    using FlightManager.Web.Infrastucture.Configurations;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDatabase(this.configuration)
                .AddIdentity()
                .AddCookie()
                .ApplyControllersWithViews()
                .AddApplicationServices()
                .AddSingleton(this.configuration);

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Uncomment the line below if you want to seed data in your database
            app.SeedData();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseCookiePolicy()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(
                    endpoints =>
                        {
                            endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                            endpoints.MapRazorPages();
                        });
        }
    }
}
