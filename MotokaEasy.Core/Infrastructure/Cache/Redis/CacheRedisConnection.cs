using StackExchange.Redis;

namespace MotokaEasy.Core.Infrastructure.Cache.Redis;

public class CacheRedisConnection
{
    public CacheRedisConnection(CacheConfig cacheConfig)
    {
        ConfigurationOptions = new ConfigurationOptions {EndPoints = {{cacheConfig.Host, cacheConfig.Port}}, AllowAdmin = true, Password = cacheConfig.Password, ConnectTimeout = 60 * 1000, AbortOnConnectFail = true};
    }
        
    public ConfigurationOptions ConfigurationOptions { get; set; }
}