using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotokaEasy.Infrastructure.Contexts;

namespace MotokaEasy.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesDataBaseExtension
{
    public static void ServicesDataBaseInitialization(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        
        var connection = !string.IsNullOrEmpty(configuration["ConnectionDb:ConnectionString"]) ? configuration["ConnectionDb:ConnectionString"] : Environment.GetEnvironmentVariable("ConnectionStringDb");
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(connection!,providerOptions => { providerOptions.EnableRetryOnFailure(); });
            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging().UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));    
            }
        }, ServiceLifetime.Transient);
    }
}