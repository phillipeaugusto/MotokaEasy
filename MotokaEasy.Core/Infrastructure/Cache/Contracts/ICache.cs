using System;

namespace MotokaEasy.Core.Infrastructure.Cache.Contracts;

public interface ICache
{ 
    bool Active();
    void Save(string key, string value, TimeSpan timeSpan);
    void Remove(string key);
    string GetValue(string key);
    bool Exists(string key);
    void ClearAll();
}