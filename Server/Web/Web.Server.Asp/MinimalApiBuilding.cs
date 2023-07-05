using Protocol.Extensions;
using Web.Endpoints.Api;
using Web.Endpoints.Api.Extensions;
using Web.Server.Primitive;

namespace Web.Server.Asp;

public static class MinimalApiBuilding
{
    public static async Task<byte[]> ReadBodyAsByteArray(this HttpRequest request)
    {
        var bytes = new byte[(int)request.ContentLength];
        await request.Body.ReadAsync(bytes);
        return bytes;
    }

    static async Task<HttpRequestData> ToHttpRequestDataAsync(HttpRequest request)
        => new HttpRequestData
        {
            Body = await request.ReadBodyAsByteArray(),
            Headers = request.Headers,
            RelativePath = request.Path.ToString().Split(ApiServerSetup.Prefix)[1]
        };

    public static Func<HttpContext, Task> ToPostRequestHandler(this ApiRequestProcessor processor)
        => async (HttpContext httpContext) =>
    {
        var request = await ToHttpRequestDataAsync(httpContext.Request);
        var response = httpContext.Response;

        var callResult = await processor.ProcessRequest(request);

        response.StatusCode = callResult.HttpStatusCode;

        var packingOptions = request.GetPackingOptions();

        var bytesResult = callResult.Bytes;

        if (packingOptions.Compressed)
        {
            bytesResult = bytesResult.Compress();
        }

        if (!packingOptions.MemoryPacked && !packingOptions.Compressed)
        {
            response.ContentType = "application/json";
        }

        await response.Body.WriteAsync(bytesResult);
    };
}
