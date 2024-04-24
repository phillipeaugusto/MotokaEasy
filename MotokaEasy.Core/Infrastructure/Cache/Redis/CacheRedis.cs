using System;
using MotokaEasy.Core.Infrastructure.Cache.Contracts;
using StackExchange.Redis;
using static System.String;

namespace MotokaEasy.Core.Infrastructure.Cache.Redis;

public class CacheRedis: ICache
{
    private readonly IDatabase _dbredis;
    private readonly IServer _server;

    public CacheRedis(ConnectionMultiplexer connectionMultiplexer, CacheConfig cacheConfig)
    {
        _dbredis = connectionMultiplexer.GetDatabase();
        _server = connectionMultiplexer.GetServer(cacheConfig.Host + ":" + cacheConfig.Port );
    }
    public bool Active()
    {
        return _dbredis.IsConnected(Empty);
    }

    public void Save(Guid id, string value, TimeSpan timeSpan)
    {
        if (!Active())
            return;

        if (Exists(id) && value == GetValue(id))
            return;

        Remove(id);
            
        _dbredis.StringAppend(id.ToString(), value);
        _dbredis.KeyExpire(id.ToString(), timeSpan);
    }

    public void Save(Guid id, string value)
    {
        if (!Active())
            return;

        if (Exists(id) && value == GetValue(id))
            return;

        Remove(id);
            
        _dbredis.StringAppend(id.ToString(), value);
    }

    public void Save(string id, string value)
    {
        if (!Active())
            return;

        if (Exists(id) && value == GetValue(id))
            return;

        Remove(id);
            
        _dbredis.StringAppend(id, value);
    }

    public void Save(string key, string value, TimeSpan timeSpan)
    {
        if (!Active())
            return;

        if (Exists(key) && value == GetValue(key))
            return;

        Remove(key);
        _dbredis.StringAppend(key, value);
        _dbredis.KeyExpire(key, timeSpan);
    }

    public void Remove(Guid id)
    {
        if (!Active())
            return;
        
        if (!Exists(id))
            return;
            
        _dbredis.KeyDelete(id.ToString());
    }

    public void Remove(string key)
    {
        if (!Active())
            return;
        
        if (!Exists(key))
            return;
            
        _dbredis.KeyDelete(key);
    }

    public string GetValue(Guid id)
    {             
        if (!Active())
            return Empty;

        return _dbredis.StringGet(id.ToString());
    }

    public string GetValue(string key)
    {
        if (!Active())
            return Empty;

        return _dbredis.StringGet(key);
    }

    public bool Exists(Guid id)
    {
        return Active() && _dbredis.KeyExists(id.ToString());
    }

    public bool Exists(string key)
    {
        return Active() && _dbredis.KeyExists(key);
    }

    public void ClearAll()
    {
        if (!Active())
            return;
        
        _server.FlushDatabase();
    }
}