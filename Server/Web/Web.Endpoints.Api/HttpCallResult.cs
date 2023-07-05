using Microsoft.Extensions.Primitives;
using Protocol;
using Protocol.Exceptions;
using Protocol.Extensions;
using Protocol.MessageExchange;
using Web.Endpoints.Api.Extensions;

namespace Web.Endpoints.Api;

public struct HttpRequestData
{ 
    public required string RelativePath { get; init; }
    public required IDictionary<string, StringValues> Headers { get; init; }
    public required byte[] Body { get; init; }
}

public struct HttpCallResult
{
    public int HttpStatusCode { get; private init; }

    public byte[] Bytes { get; private set; }


    public static HttpCallResult FromMessage<T>(ProtocolMessage<T> protocolMessage, PackingOptions packingOptions)
    {
        int httpStatus = protocolMessage.GetHttpStatus();

        var result = new HttpCallResult
        { 
            HttpStatusCode = httpStatus,
            Bytes = protocolMessage.ToBytes(packingOptions.MemoryPacked)
        };

        return result;
    }

    public static HttpCallResult FromError<T>(ExceptionModel error, PackingOptions packingOptions)
        => FromMessage(ProtocolMessage.FromError<T>(error), packingOptions);
    
}
