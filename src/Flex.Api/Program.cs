using Serilog;
using Flex.Api.Bootstraping;
using Flex.Api.Middlewares;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

SeriLoggerExtension.ConfigureLogger(builder.Configuration);

Log.Information("Application starting up.");

try
{
    // Logging
    builder.Host.UseSerilog();

    // Add services to the container.

    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwagger();

    // Register DI
    services.RegisterOptions(configuration);
    services.RegisterServices(configuration);

    // Validators
    services.AddFluentValidation();

    // Data
    services.AddEntityFrameworkCore(configuration);

    // Identity
    services.AddIdentityEntityFrameworkCore();

    builder.Services.AddControllers();

    var app = builder.Build();

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    // Seeder
    await app.MigrateSeederDatabase();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Logging
    app.UseMiddleware<LoggingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application encountered a fatal error and will terminate.");
}
finally
{
    Log.Information("Application is shutting down.");
    Log.CloseAndFlush();
}