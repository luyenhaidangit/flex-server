using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Flex.Core.Domain.Identity;
using RoleConstants = Flex.Core.Shared.Constants.Identity.Role;

namespace Flex.Data.Seeders
{
    public class IdentitySeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            if (!await roleManager.RoleExistsAsync(RoleConstants.Admin))
            {
                await roleManager.CreateAsync(new Role("Admin"));
            }

            var adminEmail = "admin@flex.com";
            var username = "admin";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new User
                {
                    UserName = username,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, "Haidang106@");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
