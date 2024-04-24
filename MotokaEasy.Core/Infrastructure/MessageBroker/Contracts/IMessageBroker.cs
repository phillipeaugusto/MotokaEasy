using System;
using System.Collections.Generic;
using MotokaEasy.Core.Infrastructure.MessageBroker.Dto;

namespace MotokaEasy.Core.Infrastructure.MessageBroker.Contracts;

public interface IMessageBroker
{
    bool Active();
    void CreateQueue(string name);
    void PublishQueue<T>(string queueName, T obj, Dictionary<string,object> additionalInformation = null);
    void ConsumeMessageQueue(string queueName, int numberConsumers = 1, bool automaticReading = true);
    public event Action<MessageBrokerDto> ListenerQueue;
    void ConfirmReading(MessageBrokerDto messageBrokerDto);
    void NotConfirmReading(MessageBrokerDto messageBrokerDto);
    void RepublishQueue<T>(string queueName, string queueNameError, T obj, MessageBrokerDto messageBrokerDto, string messageException);
}