using System;
using Enyim.Caching;
using MotokaEasy.Core.Infrastructure.Cache.Contracts;
using static System.String;

namespace MotokaEasy.Core.Infrastructure.Cache.MemCached;

public class MemCached: ICache
{
    private readonly IMemcachedClient _cache;

    public MemCached(IMemcachedClient cache)
    {
        _cache = cache;
    }

    public bool Active()
    {
        var status = _cache.Stats();

        return status is not null; // rever aqui
    }

    public void Save(string key, string value, TimeSpan timeSpan)
    {
        
        if (!Active())
            return;

        if (Exists(key) && value == GetValue(key))
            return;

        Remove(key);
        _cache.Add(key, value, timeSpan);
    }

    public void Remove(string key)
    {
        if (!Active())
            return;
        
        _cache.Remove(key);
    }

    public string GetValue(string key)
    {
        if (!Active())
            return Empty;
        
        _cache.TryGet(key, out string result);
        
        return result;
    }

    public bool Exists(string key)
    {
        return Active() && _cache.TryGet(key, out string _);
    }

    public void ClearAll()
    {
        if (!Active())
            return;
        
        _cache.FlushAll();
    }
}