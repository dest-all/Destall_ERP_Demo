using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MemoryPack;
using Common.Extensions.Object;

namespace Protocol.Extensions;

public static class ProtocolMessagePacking
{
    static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    };

    static string ToJson<T>(T obj)
        => obj.ToJson();

    static T FromJson<T>(string json)
        => json.ParseAsJson<T>();

    public static ValueTask<ProtocolMessage<T>> ExtractProtocolMessageAsync<T>(this byte[] bytes, bool useMemoryPack)
    {
        if (useMemoryPack)
        {
            var tuple = MemoryPackSerializer.Deserialize<ValueTuple<ProtocolMessageAddin, T>>(bytes);

            var result = ProtocolMessage<T>.FromTuple(tuple);

            return new(result);
        }

        var json = Encoding.UTF8.GetString(bytes);

        var resultFromJson = FromJson<ProtocolMessage<T>>(json);

        return new(resultFromJson);
    }

    public static byte[] ToBytes<T>(this ProtocolMessage<T> message, bool useMemoryPack)
    {
        byte[] bytes;
        if (useMemoryPack)
        {
            var tuple = message.ToTuple();
            bytes = MemoryPackSerializer.Serialize(tuple);
        }
        else
        {
            var json = ToJson(message);
            bytes = Encoding.UTF8.GetBytes(json);
        }

        return bytes;
    }

}
