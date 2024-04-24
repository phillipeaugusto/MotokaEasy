using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MotokaEasy.Core.Resilience.Contracts;
using MotokaEasy.Core.Resilience.Polly;

namespace MotokaEasy.Api.Extensions;
[ExcludeFromCodeCoverage]
public static class ServicesResilienceExtension
{
    public static void ResilienceInitialization(this IServiceCollection services)
    {
        services.AddSingleton<IResilience>(new ResiliencePolly());
    }
}