using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Infrastructure.Customer;

namespace MyShop.Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedAdmin(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<CustomerIdentity>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
            }

            var admin = await userManager.FindByEmailAsync("admin@shop.local");
            if (admin == null)
            {
                var newAdmin = new CustomerIdentity
                {
                    UserName = "admin@shop.local",
                    Email = "admin@shop.local", 
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    PhoneNumber = string.Empty 
                };
                var result = await userManager.CreateAsync(newAdmin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.SetEmailAsync(newAdmin, "admin@shop.local");
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }

    }
}
