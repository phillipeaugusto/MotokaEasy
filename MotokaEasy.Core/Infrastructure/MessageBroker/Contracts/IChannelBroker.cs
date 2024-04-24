namespace MotokaEasy.Core.Infrastructure.MessageBroker.Contracts;

public interface IChannelBroker
{
    void ConfirmReading(ulong deliveryTag);
    void NotConfirmReading(ulong deliveryTag);
}