using AutoMapper;
using Flex.Core.Extensions;
namespace Flex.Api.Bootstraping
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Core.AssemblyReference.Assembly);

            return services;
        }
    }
}
