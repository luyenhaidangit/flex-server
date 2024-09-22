using Serilog;
using Serilog.Events;

namespace Flex.Api.Bootstraping
{
    public static class SeriLoggerExtension
    {
        public static void ConfigureLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Async(a => a.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}"))
                .WriteTo.Async(a => a.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}"))
                .CreateLogger();
        }

        #region Document
        /// <summary>
        /// Unused
        /// Logging
        /// app.UseMiddleware<LoggingMiddleware>();
        /// app.UseSerilogRequestLoggingApplication();
        /// </summary>
        #endregion
        public static WebApplication UseSerilogRequestLoggingApplication(this WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
                options.GetLevel = (httpContext, elapsed, ex) => elapsed > 1000 ? LogEventLevel.Warning : LogEventLevel.Information;
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
                };
            });

            return app;
        }
    }
}
