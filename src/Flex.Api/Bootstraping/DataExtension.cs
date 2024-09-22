using Microsoft.EntityFrameworkCore;
using Flex.Data;
using Flex.Core.Shared.Constants.System;
using Flex.Data.Seeders;

namespace Flex.Api.Bootstraping
{
    public static class DataExtension
    {
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(AppSetting.DefaultConnection));
            }, ServiceLifetime.Transient);

            return services;
        }

        public static async Task<WebApplication> MigrateSeederDatabase(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();

            await IdentitySeeder.Initialize(scope.ServiceProvider);

            return application;
        }
    }
}