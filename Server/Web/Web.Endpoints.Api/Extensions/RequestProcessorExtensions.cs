using Business.ActionPoints;
using Microsoft.Extensions.Logging;
using Protocol;
using Protocol.Exceptions;
using Protocol.Extensions;
using Protocol.MessageExchange;
using static Protocol.Constants;

namespace Web.Endpoints.Api.Extensions;

public static class RequestProcessorExtensions
{
    public static int GetHttpStatus(this ProtocolMessage response) => response.Addin?.Error is null ? 200 : response.Addin.Error.IsHandled() ? 400 : 500;

    public static bool ReceivedMemoryPackMessage(this HttpRequestData httpRequest)
    {
        if (httpRequest.Headers.TryGetValue(Headers.MemoryPack.Key, out var contentType))
        {
            return contentType.ToString() == Headers.MemoryPack.Affirmative;
        }
        return false;
    }

    public static bool ApplyCompression(this HttpRequestData httpRequest)
    {
        if (httpRequest.Headers.TryGetValue(Headers.ApplyCompression, out var contentType))
        {
            return contentType == "1";
        }
        return true;
    }

    public static async Task<T> ExtractParameterAsync<T>(this HttpRequestData request, PackingOptions packingOptions)
    {
        var bytes = request.Body;

        if (packingOptions.Compressed)
        {
            bytes = bytes.Decompress();
        }

        var result = await bytes.ExtractProtocolMessageAsync<T>(packingOptions.MemoryPacked);

        return result.Message;
    }

    public static PackingOptions GetPackingOptions(this HttpRequestData request) =>
        new()
        {
            Compressed = request.ApplyCompression(),
            MemoryPacked = request.ReceivedMemoryPackMessage()
        };
}