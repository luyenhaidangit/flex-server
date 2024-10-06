using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Flex.Api.Bootstraping
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.DocumentFilter<LowerCaseDocumentFilter>();

                // Token
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Vui lòng nhập token của bạn",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }

    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.ToDictionary(
                path => path.Key.ToLowerInvariant(),
                path => path.Value
            );

            swaggerDoc.Paths = new OpenApiPaths();
            foreach (var pathItem in paths)
            {
                swaggerDoc.Paths.Add(pathItem.Key, pathItem.Value);
            }
        }
    }
}
