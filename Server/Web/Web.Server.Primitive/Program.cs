using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using Web.Endpoints.Api.Extensions;
using Web.Server.Primitive;
using DestallMaterials.WheelProtection.Extensions.Tasks;

string portString = args.FirstOrDefault() ?? "5056";

var port = int.Parse(portString);

using var listener = new HttpListener();

bool isWindows = OperatingSystem.IsWindows();

string prefix;

if (isWindows)
{
    prefix = prefix = $"http://localhost:{port}/";
} else
{
    prefix = prefix = $"http://*:{port}/";
}

listener.Prefixes.Add(prefix);
listener.Start();

var log = (string message) => Console.WriteLine($"{DateTime.UtcNow} -----> {message}");

log($"Started listening on port {port}...");

var close = () => log($"Stopped listening on port {port}.");

Console.CancelKeyPress += (o, k) => close();

var requestProcessor = ApiServerSetup.CreateRequestProcessor((ex, context) => log($"op_id: {context.OperationId} skey: {context.SessionKey}     ERROR {ex.Message} {ex.StackTrace}"), out var _);

while (true)
{
    var context = await listener.GetContextAsync();
    var request = context.Request;
    if (request.LocalEndPoint.Port != port)
    {
        continue;
    }

    var response = context.Response;

    response.Headers.Add("Access-Control-Allow-Origin", "*");
    response.Headers.Add("Access-Control-Allow-Methods", "POST");
    response.Headers.Add("Access-Control-Allow-Headers", "*");

    if (request.HttpMethod == HttpMethod.Options.Method)
    {
        response.StatusCode = 200;
        response.Close();
        continue;
    }


    Task.Run(async () => {
        var logMessage = $"Processing request to {request.RawUrl}...";

        log(logMessage);

        var requestData = await request.ToDataAsync();

        var packingOptions = requestData.GetPackingOptions();

        string messageFormat = packingOptions.MemoryPacked ? "Memory Pack" : "JSON";
        
        log($"... uses {messageFormat} format...");

        if (packingOptions.Compressed)
        {
            log("... with compression...");
        }

        var result = await requestProcessor.ProcessRequest(requestData);

        await response.LoadResponseAsync(result, packingOptions.Compressed);

        log($"Request processed with status {result.HttpStatusCode}.");
    }).Forget();
}