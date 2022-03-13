using FlightManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static FlightManager.Common.GlobalConstants;

namespace FlightManager.Data.Seeding
{
    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            ApplicationUser userFromDb = await userManager.FindByNameAsync(Admin.Username);

            if (userFromDb != null)
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = Admin.Username,
                Email = Admin.Email,
                Name = Admin.Name,
                Surname = Admin.Surname,
                PersonalNumber = Admin.PersonalNumber,
                PhoneNumber = Admin.PhoneNumber,
                Address = Admin.Address
            };

            await userManager.CreateAsync(user, Admin.Password);
            IdentityResult result = await userManager.AddToRoleAsync(user, Roles.Administrator);

        }
    }
}
