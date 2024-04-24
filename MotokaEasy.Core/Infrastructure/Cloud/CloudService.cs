using Microsoft.Extensions.DependencyInjection;
using MotokaEasy.Core.Infrastructure.Cloud.Aws;
using MotokaEasy.Core.Infrastructure.Cloud.Aws.Config;
using MotokaEasy.Core.Infrastructure.Cloud.Contracts;

namespace MotokaEasy.Core.Infrastructure.Cloud;

public class CloudService
{
    public static void AddAws(IServiceCollection services, AwsConfig awsConfig)
    {
        services.AddSingleton(awsConfig);
        services.AddTransient<ICloud, AwsCloud>();
    }
}