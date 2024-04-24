using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotokaEasy.Core.Infrastructure.Cache;
using MotokaEasy.Core.Infrastructure.Cache.Enum;

namespace MotokaEasy.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesCacheExtension
{
    public static void CacheInitialization(this IServiceCollection services, IConfiguration configuration)
    { 
        var host = !string.IsNullOrEmpty(configuration["ConnectionCache:Host"]) ? configuration["ConnectionCache:Host"] : Environment.GetEnvironmentVariable("ConnectionCacheHost");
        var port = !string.IsNullOrEmpty(configuration["ConnectionCache:Port"]) ? configuration["ConnectionCache:Port"] : Environment.GetEnvironmentVariable("ConnectionCachePort");
        var password = !string.IsNullOrEmpty(configuration["ConnectionCache:PassWord"]) ? configuration["ConnectionCache:PassWord"] : Environment.GetEnvironmentVariable("ConnectionCachePassWord");
        var typeCacheEnum = Enum.Parse<CacheEnum>((!string.IsNullOrEmpty(configuration["ConnectionCache:Cache"]) ? configuration["ConnectionCache:Cache"] : Environment.GetEnvironmentVariable("ConnectionCacheCache"))!);
        CacheService.Add(services, typeCacheEnum, new CacheConfig(host, int.Parse(port!), password));
    }
}