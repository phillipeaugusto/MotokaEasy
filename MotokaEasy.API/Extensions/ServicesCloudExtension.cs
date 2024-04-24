using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotokaEasy.Core.ConstantsApp;
using MotokaEasy.Core.Infrastructure.Cloud;
using MotokaEasy.Core.Infrastructure.Cloud.Aws.Config;

namespace MotokaEasy.Api.Extensions;

public static class ServicesCloudExtension
{
    public static void CloudInitialization(this IServiceCollection services, IConfiguration configuration)
    {
        var awsAccessKey =  !string.IsNullOrEmpty(configuration["CloudConfig:AWSAccessKey"]) ? configuration["CloudConfig:AWSAccessKey"] : Environment.GetEnvironmentVariable("CloudConfigAWSAccessKey");
        var awsSecretKey =  !string.IsNullOrEmpty(configuration["CloudConfig:AWSSecretKey"]) ? configuration["CloudConfig:AWSSecretKey"] : Environment.GetEnvironmentVariable("CloudConfigAWSSecretKey");
        var production = !string.IsNullOrEmpty(configuration["CloudConfig:Production"]) ? configuration["CloudConfig:Production"] : Environment.GetEnvironmentVariable("CloudConfigProduction");
        var urlService = !string.IsNullOrEmpty(configuration["CloudConfig:UrlService"]) ? configuration["CloudConfig:UrlService"] : Environment.GetEnvironmentVariable("CloudConfigUrlService");
        CloudService.AddAws(services, new AwsConfig(awsAccessKey, awsSecretKey, production == ApplicationConstants.StatusSim, urlService));
    }
}