using Newtonsoft.Json;



namespace Common.Extensions.Object;


public static class ObjectExtensions
{
    static readonly JsonSerializerSettings _jsonSerializerSettings = new()
    {
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Formatting = Formatting.Indented
    };


    public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj, _jsonSerializerSettings);

    public static T ParseAsJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json);

    public static bool CompareConsideringNulls<T>(this T obj1, T obj2)
    {
        if (obj1 == null && obj2 == null)
        {
            return true;
        }
        if (obj1 == null || obj2 == null)
        {
            return false;
        }
        return obj1.Equals(obj2);
    }
}
