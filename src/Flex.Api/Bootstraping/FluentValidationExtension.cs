using Flex.Core.Models.Common;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Flex.Api.Bootstraping
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(options =>
            {
                //options.ImplicitlyValidateChildProperties = true;
                //options.ImplicitlyValidateRootCollectionElements = true;
                options.DisableDataAnnotationsValidation = true;

                options.RegisterValidatorsFromAssembly(Core.AssemblyReference.Assembly);

                ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorsMessage = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        ).FirstOrDefault().Value.FirstOrDefault();

                    var result = new BadRequestObjectResult(Result.Failure(errorsMessage));

                    return result;
                };
            });

            return services;
        }
    }

    public class CustomLanguageManager : LanguageManager
    {
        public CustomLanguageManager()
        {
            Culture = new CultureInfo("vi");

            AddTranslation("vi", "NotEmptyValidator", "{PropertyName} không được để trống");
        }
    }
}
