using System;
using Enyim.Caching.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotokaEasy.Core.Infrastructure.Cache.Contracts;
using MotokaEasy.Core.Infrastructure.Cache.Enum;
using MotokaEasy.Core.Infrastructure.Cache.Redis;
using StackExchange.Redis;

namespace MotokaEasy.Core.Infrastructure.Cache;

public static class CacheService
{
    public static void Add(IServiceCollection services, CacheEnum typeCache, CacheConfig cacheConfig)
    {
        
        services.AddSingleton(cacheConfig);
        switch (typeCache)
        {
            case CacheEnum.Redis:
            {
                services.AddSingleton(cacheConfig);
                services.AddSingleton(ConnectionMultiplexer.Connect(new CacheRedisConnection(cacheConfig).ConfigurationOptions));
                services.AddTransient<ICache, CacheRedis>();
                break;
            }
            case CacheEnum.Memcached:
            {
                services.AddEnyimMemcached(memcachedClientOptions => {
                    memcachedClientOptions.Servers.Add(new Server
                    {
                        Address = cacheConfig.Host,
                        Port = cacheConfig.Port
                    });
                });
                services.AddTransient<ICache, MemCached.MemCached>();
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(typeCache), typeCache, "Cache type not informed");
        }
    }
}