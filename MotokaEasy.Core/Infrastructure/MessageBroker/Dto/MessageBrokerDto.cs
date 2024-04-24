using System.Collections.Generic;
using MotokaEasy.Core.Infrastructure.MessageBroker.Contracts;

namespace MotokaEasy.Core.Infrastructure.MessageBroker.Dto;

public class MessageBrokerDto
{
    public MessageBrokerDto(string message, ulong deliveryTag, IDictionary<string, object>  additionalInformation, IChannelBroker channelBroker)
    {
        Message = message;
        DeliveryTag = deliveryTag;
        AdditionalInformation = additionalInformation;
        ChannelBroker = channelBroker;
    }

    public IChannelBroker ChannelBroker;
    public string Message { get; set; }
    public ulong DeliveryTag { get; set; }
    public IDictionary<string, object> AdditionalInformation {get; set;}
}