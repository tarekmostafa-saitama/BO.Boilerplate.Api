using Application.Mapping;
using Infrastructure.Auth;
using Infrastructure.BackgroundJobs;
using Infrastructure.Caching;
using Infrastructure.Common;
using Infrastructure.Cors;
using Infrastructure.HealthChecks;
using Infrastructure.Identity;
using Infrastructure.Mailing;
using Infrastructure.Middlewares;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        MapsterSettings.Configure();
        return services
            .AddAuth(config)
            .AddServices()
            .AddBackgroundServices(config)
            .AddCorsPolicy(config)
            .AddCaching(config)
            .AddCorsPolicy(config)
            .AddMailing(config)
            .AddIdentityServices()
            .AddPersistence(config)
            .AddRepositories()
            .AddCustomMiddlewares()
            .AddHealthChecks(config);
    }


    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
    {
        return builder
            .UseExceptionHandlerMiddleware()
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseRouting()
            .UseCorsPolicy()
            .UseAuthentication()
            .UseAuthorization()
            .UseHangfireDashboard(config);
    }
}