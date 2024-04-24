namespace MotokaEasy.Core.Infrastructure.Cache;

public class CacheConfig
{
    public CacheConfig() { }

    public CacheConfig(string host, int port, string password)
    {
        Host = host;
        Port = port;
        Password = password;
    }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }

}