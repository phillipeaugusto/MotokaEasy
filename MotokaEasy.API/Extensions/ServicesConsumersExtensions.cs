using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MotokaEasy.Consumers.Consumers;

namespace MotokaEasy.Api.Extensions;
[ExcludeFromCodeCoverage]
public static class ServicesConsumerExtension
{
    public static void ConsumersInitialization(this IServiceCollection services)
    {
        services.AddHostedService<AtualizarNumeroPlacaVeiculoConsumer>();
    }
}