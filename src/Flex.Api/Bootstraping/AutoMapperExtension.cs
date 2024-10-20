using AutoMapper;
using Flex.Core.Extensions;
namespace Flex.Api.Bootstraping
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Core.AssemblyReference.Assembly);

            // Config mapper for services
            var mapper = services.BuildServiceProvider().GetRequiredService<IMapper>();

            IQueryableExtension.ConfigureMapper(mapper);

            return services;
        }
    }
}
