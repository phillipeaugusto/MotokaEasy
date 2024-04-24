using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MotokaEasy.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesHealthChecksExtension
{
    public static void ServicesHealthChecks(this IServiceCollection services, IConfiguration configuration)
    { 
        var connection = !string.IsNullOrEmpty(configuration["ConnectionDb:ConnectionString"]) ? configuration["ConnectionDb:ConnectionString"] : Environment.GetEnvironmentVariable("ConnectionStringDb");
        services.AddHealthChecks();
        services.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(5);
            options.MaximumHistoryEntriesPerEndpoint(10);
        })
        .AddPostgreSqlStorage(connection!);
    }
}