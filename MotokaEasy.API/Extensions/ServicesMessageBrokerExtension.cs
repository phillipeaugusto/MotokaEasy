using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotokaEasy.Core.Infrastructure.MessageBroker;
using MotokaEasy.Core.Infrastructure.MessageBroker.Enum;

namespace MotokaEasy.Api.Extensions;
[ExcludeFromCodeCoverage]
public static class MessageBrokerExtension
{
    public static void MessageBrokerInitialization(this IServiceCollection services, IConfiguration configuration)
    {
        var host =  !string.IsNullOrEmpty(configuration["ConnectionMessageBroker:Host"]) ? configuration["ConnectionMessageBroker:Host"] : Environment.GetEnvironmentVariable("ConnectionMessageBrokerHost");
        var userName = !string.IsNullOrEmpty(configuration["ConnectionMessageBroker:UserName"]) ? configuration["ConnectionMessageBroker:UserName"] : Environment.GetEnvironmentVariable("ConnectionMessageBrokerUserName");
        var passWord = !string.IsNullOrEmpty(configuration["ConnectionMessageBroker:PassWord"]) ? configuration["ConnectionMessageBroker:PassWord"] : Environment.GetEnvironmentVariable("ConnectionMessageBrokerPassWord");
        var port =  !string.IsNullOrEmpty(configuration["ConnectionMessageBroker:Port"]) ? configuration["ConnectionMessageBroker:Port"] : Environment.GetEnvironmentVariable("ConnectionMessageBrokerPort");
        var typeMessageBrokerEnum = Enum.Parse<MessageBrokerEnum>(!string.IsNullOrEmpty(configuration["ConnectionMessageBroker:MessageBroker"]) ? configuration["ConnectionMessageBroker:MessageBroker"] : Environment.GetEnvironmentVariable("ConnectionMessageBrokerMessageBroker")); 
        MessageBrokerService.Add(services, typeMessageBrokerEnum, new MessageBrokerConfig{Host = host, Password = passWord, Port = int.Parse(port), UserName = userName});
    }
}