using Api.Configurations;
using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Infrastructure.Logging;
using Infrastructure.Logging.SerilogSettings;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Globalization;

StaticLogger.EnsureInitialized();
Log.Information("Server Booting Up...");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddConfigurations().RegisterSerilog();
    builder.Services.AddControllers();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);


    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddOpenApiDocument(options =>
    {
        options.DocumentName = "v1";
        options.Title = "Bo.Boilerplate";
        options.Version = "v1";
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        // Initialise and seed database
        using var scope = app.Services.CreateScope();
        var initialiser =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.SeedAsync();

        app.UseOpenApi();
        app.UseSwaggerUi3();
    }
    else
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseInfrastructure(builder.Configuration);
    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    app.MapHealthChecksUI();
    app.MapControllers();
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}
public partial class Program { }