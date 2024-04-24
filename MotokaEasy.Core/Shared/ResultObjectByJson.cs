using Newtonsoft.Json;

namespace MotokaEasy.Core.Shared;

public static class ObjectByJson
{

    public static string ReturnJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T ReturnObject<T>(string json)
    { 
        return JsonConvert.DeserializeObject<T>(json);
    }
}