using Flex.Core.Exceptions;
using Flex.Core.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Flex.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var response = JsonSerializer.Serialize(Result.Failure(exception.Message),options);

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;
           

            await httpContext.Response.WriteAsync(response);
        }

        private static int GetStatusCode(Exception exception)
        {
            switch (exception)
            {
                case BadHttpRequestException:
                    return StatusCodes.Status400BadRequest;
                case UnauthorizedException:
                    return StatusCodes.Status401Unauthorized;
                case ValidationException:
                    return StatusCodes.Status400BadRequest;
                case FormatException:
                    return StatusCodes.Status422UnprocessableEntity;
                case ArgumentException:
                    return StatusCodes.Status400BadRequest;
                default:
                    return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
