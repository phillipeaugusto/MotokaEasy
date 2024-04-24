namespace MotokaEasy.Core.Infrastructure.Cloud.Aws.Config;

public class AwsConfig
{
    public AwsConfig(string accessKey, string secretKey, bool production, string url)
    {
        AccessKey = accessKey;
        SecretKey = secretKey;
        Production = production;
        Url = url;
    }

    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public bool Production { get; set; }
    public string Url { get; set; }
}