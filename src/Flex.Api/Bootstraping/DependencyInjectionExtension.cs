using System.IdentityModel.Tokens.Jwt;
using Flex.Core.Shared.Options;
using Flex.Core.Shared.Constants.System;
using AssemblyReferenceCore = Flex.Core.AssemblyReference;
using Flex.Api.Middlewares;

namespace Flex.Api.Bootstraping
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure
            services.Configure<ConnectionStrings>(configuration.GetSection(AppSetting.ConnectionStrings));
            services.Configure<Jwt>(configuration.GetSection(AppSetting.Jwt));

            return services;
        }

        #region Document
        /// <summary>
        /// Rule: XService implement IXService
        /// </summary>
        #endregion
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            var serviceProjectNamespace = $"{AssemblyReferenceCore.Assembly.GetName().Name}.Services";
            var serviceTypes = AssemblyReferenceCore.Assembly.GetTypes().Where(type => type.Namespace == serviceProjectNamespace && type.IsClass && type.GetInterfaces().Any() && !type.IsNested);

            foreach (var type in serviceTypes)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(x => x.Name == $"I{type.Name}");
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }

            // Infrastructures
            //services.AddScoped<IDataAccess>(provider =>
            //{
            //    string connectionString = configuration.GetConnectionString("OracleConnection");
            //    return new OracleDbContext(connectionString);
            //});

            // Jwt
            services.AddSingleton<JwtSecurityTokenHandler>();

            // Middleware
            services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}
