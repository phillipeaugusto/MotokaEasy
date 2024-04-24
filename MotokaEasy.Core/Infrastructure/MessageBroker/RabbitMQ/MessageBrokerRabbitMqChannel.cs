using MotokaEasy.Core.Infrastructure.MessageBroker.Contracts;
using RabbitMQ.Client;

namespace MotokaEasy.Core.Infrastructure.MessageBroker.RabbitMQ;

public class MessageBrokerRabbitMqChannel: IChannelBroker
{
    private readonly IModel _model;
    public MessageBrokerRabbitMqChannel(IModel model)
    {
        _model = model;
    }

    public void ConfirmReading(ulong deliveryTag)
    {
        _model.BasicAck(deliveryTag, false);
    }

    public void NotConfirmReading(ulong deliveryTag)
    {
        _model.BasicNack(deliveryTag, false, true);
    }
}