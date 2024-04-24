using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using MotokaEasy.Core.Infrastructure.MessageBroker.Constants;
using MotokaEasy.Core.Infrastructure.MessageBroker.Contracts;
using MotokaEasy.Core.Infrastructure.MessageBroker.Dto;
using MotokaEasy.Core.Resilience.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static System.String;

namespace MotokaEasy.Core.Infrastructure.MessageBroker.RabbitMQ;

public class MessageBrokerRabbitMq: IMessageBroker
{
    private readonly IConnection _connection;
    private readonly ILogger<MessageBrokerRabbitMq> _logger;
    private readonly IResilience _resilience;
    
    public MessageBrokerRabbitMq(MessageBrokerRabbitMqConnection connection, ILogger<MessageBrokerRabbitMq> logger, IResilience resilience)
    {
        _logger = logger;
        _resilience = resilience;
        _connection = connection.Connection;
        Active();
    }
        
    public event Action<MessageBrokerDto> ListenerQueue;
    public bool Active()
    { 
        return _connection.IsOpen;
    }

    public void CreateQueue(string queue)
    {
        if (!Active())
            return;
            
        using var channel = _connection.CreateModel();
        _resilience.Execute(() => channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null));              
    }

    public void PublishQueue<T>(string queueName, T obj, Dictionary<string,object> additionalInformation = null)
    {
        if (!Active())
            return;
            
        var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        using var channel = _connection.CreateModel();
        var basicProperties = channel.CreateBasicProperties();
        basicProperties.Headers = new Dictionary<string,object>();

        if (additionalInformation is not null)
        {
            foreach (var (key, value) in additionalInformation)
                basicProperties.Headers.Add(key, value);
        }

        if (!basicProperties.Headers.ContainsKey(MessageBrokerConstants.Attempts))
            basicProperties.Headers.Add(MessageBrokerConstants.Attempts, 1);
            
        CreateQueue(queueName);
        _logger.LogInformation("SendToMessageBroker|Queue: " + queueName + " |Data: " + Convert.ToBase64String(message));
        _resilience.Execute(() => channel.BasicPublish(exchange: Empty, routingKey: queueName, basicProperties: basicProperties, body: message));
    }

    public void ConsumeMessageQueue(string queueName, int numberConsumers = 1, bool automaticReading = true)
    {
        if (!Active())
            return;
            
        var channel = _connection.CreateModel();
        CreateQueue(queueName);
        channel.QueueDeclarePassive(queue: queueName);
        for (var i = 0; i < numberConsumers; i++)
        {
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            consumer.Received += (_, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    OnListenerQueue(new MessageBrokerDto(message, ea.DeliveryTag, ea.BasicProperties.Headers, new MessageBrokerRabbitMqChannel(channel))); 
                    if (automaticReading) 
                        channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Erro Consumer Toppic: {queueName}, Error: {e.Message}");
                    channel.BasicNack(ea.DeliveryTag, false, true);
                    throw new Exception(e.Message);
                }
            };                
        }
    }

    private void OnListenerQueue(MessageBrokerDto messageBrokerDto)
    {
        ListenerQueue?.Invoke(messageBrokerDto);
    }

    public void ConfirmReading(MessageBrokerDto messageBrokerDto)
    {
        messageBrokerDto.ChannelBroker.ConfirmReading(messageBrokerDto.DeliveryTag);
    }

    public void NotConfirmReading(MessageBrokerDto messageBrokerDto)
    {
        messageBrokerDto.ChannelBroker.NotConfirmReading(messageBrokerDto.DeliveryTag);
    }

    private static Dictionary<string, object> ReturnDataInfoErro(string messageException){
        return new Dictionary<string, object>(new List<KeyValuePair<string, object>> {new(MessageBrokerConstants.ErroConsumerMessage, messageException)});
    }

    private Dictionary<string, object> ReturnDataQuantityOfAttempts(int amount){
        return new Dictionary<string, object>(new List<KeyValuePair<string, object>> {new(MessageBrokerConstants.Attempts, amount)});
    }
        
    public void RepublishQueue<T>(string queueName, string queueNameError, T obj, MessageBrokerDto messageBrokerDto, string messageException)
    {
        ConfirmReading(messageBrokerDto); 
        object countMessage;
        if (messageBrokerDto.AdditionalInformation.TryGetValue(MessageBrokerConstants.Attempts, out countMessage))
        {
            var count = Convert.ToInt32(countMessage);
            if (count <= MessageBrokerConstants.QuantityOfAttempts){
                PublishQueue(queueName, obj, ReturnDataQuantityOfAttempts(count + 1));
                return;
            }
            PublishQueue(queueNameError, obj, ReturnDataInfoErro(messageException));                   
        }
    }
}