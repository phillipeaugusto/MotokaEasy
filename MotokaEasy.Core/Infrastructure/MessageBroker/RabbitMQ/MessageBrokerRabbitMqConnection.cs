using System;
using Microsoft.Extensions.Logging;
using MotokaEasy.Core.Resilience.Contracts;
using RabbitMQ.Client;

namespace MotokaEasy.Core.Infrastructure.MessageBroker.RabbitMQ;

public class MessageBrokerRabbitMqConnection
{
    private void Connect(MessageBrokerConfig messageBrokerConfig)
    {
        var factory = new ConnectionFactory {Uri = new Uri($"amqp://{messageBrokerConfig.UserName}:{messageBrokerConfig.Password}@{messageBrokerConfig.Host}/")};
        Connection = factory.CreateConnection();
    }
    public MessageBrokerRabbitMqConnection(MessageBrokerConfig messageBrokerConfig, ILogger logger, IResilience resilience)
    {
        try
        { 
            resilience.Execute(() => Connect(messageBrokerConfig));
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }
    public IConnection Connection;
}