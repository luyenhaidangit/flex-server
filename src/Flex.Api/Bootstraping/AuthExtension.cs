using Microsoft.AspNetCore.Identity;
using Flex.Core.Domain.Identity;
using Flex.Data;

namespace Flex.Api.Bootstraping
{
    public static class AuthExtension
    {
        public static IServiceCollection AddIdentityEntityFrameworkCore(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = null;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}