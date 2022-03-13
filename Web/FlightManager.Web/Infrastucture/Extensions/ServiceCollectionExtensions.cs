namespace FlightManager.Web.Infrastucture.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FlightManager.Data;
    using FlightManager.Data.Common;
    using FlightManager.Data.Common.Repositories;
    using FlightManager.Data.Models;
    using FlightManager.Data.Repositories;
    using FlightManager.Services;
    using FlightManager.Services.Data;
    using FlightManager.Services.Data.Interfaces;
    using FlightManager.Services.Messaging;
    using FlightManager.Web.Infrastucture.Configurations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetDefaultConnectionString()));

        public static IServiceCollection AddCookie(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(
               options =>
               {
                   options.CheckConsentNeeded = context => true;
                   options.MinimumSameSitePolicy = SameSiteMode.None;
               });

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(
                options =>
                    {
                        // Remove password rules if we are in development environment
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 6;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredUniqueChars = 0;
                    })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
            => services
                .AddTransient<IFlightService, FlightService>()
                .AddTransient<IPaginationService, PaginationService>()
                .AddTransient<IReservationService, ReservationService>()
                .AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
                .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                .AddScoped<IDbQueryRunner, DbQueryRunner>()
                .AddTransient<IEmailSender, SendGridEmailSender>();

        public static IServiceCollection ApplyControllersWithViews(this IServiceCollection services)
        {
            services.AddControllersWithViews(
               options =>
               {
                   options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
               })
            .AddRazorRuntimeCompilation();

            return services;
        }
    }
}
