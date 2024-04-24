using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MotokaEasy.Core.Infrastructure.MessageBroker.Contracts;
using MotokaEasy.Core.Infrastructure.MessageBroker.Enum;
using MotokaEasy.Core.Infrastructure.MessageBroker.RabbitMQ;
using MotokaEasy.Core.Resilience.Contracts;

namespace MotokaEasy.Core.Infrastructure.MessageBroker;

public static class MessageBrokerService
{
    public static void Add(IServiceCollection services, MessageBrokerEnum typeMessageBroker, MessageBrokerConfig messageBrokerConfig)
    {
        switch (typeMessageBroker)
        {
            case MessageBrokerEnum.RabbitMq:
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                services.AddSingleton(new MessageBrokerRabbitMqConnection(messageBrokerConfig,scope.ServiceProvider.GetService<ILogger<MessageBrokerRabbitMqConnection>>(),scope.ServiceProvider.GetService<IResilience>()));
                services.AddTransient<IMessageBroker, MessageBrokerRabbitMq>();
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(typeMessageBroker), typeMessageBroker, "Messagebroker type not informed");
        }
    }
}