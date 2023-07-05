using Business.ActionPoints;
using Business.Extensions;
using Business.Services;
using Microsoft.Extensions.Primitives;
using System.Net;
using Web.Endpoints.Api;
using Web.Server.Asp;
using Data.EntityFramework;
using Data.EntityFramework.Repository;
using Protocol.Extensions;
using Protocol;
using Microsoft.Extensions.Logging.Abstractions;
using Data.Repository;
using System.Collections.Concurrent;
using Data.Samples;

namespace Web.Server.Primitive;

public static class ApiServerSetup
{
    public const string Prefix = "/api/v1/";
    internal static async Task<HttpRequestData> ToDataAsync(this HttpListenerRequest request)
    {
        var bodyLength = (int)request.ContentLength64;
        var body = new byte[bodyLength];
        await request.InputStream.ReadAsync(body, 0, bodyLength);

        var requestHeaders = request.Headers;
        var headers = new Dictionary<string, StringValues>();

        foreach (var key in request.Headers.AllKeys)
        {
            var value = requestHeaders[key];
            headers.Add(key, value);
        }

        var result = new HttpRequestData
        {
            Body = body,
            Headers = headers,
            RelativePath = request.RawUrl.Split(Prefix)[1]
        };

        return result;
    }

    public static ApiRequestProcessor CreateRequestProcessor(Action<Exception, IExecutionContext> logException, out BusinessNetFactory businessFactory)
    {
        var dbContextPool = new SqliteDbContextRecycler();

        var dbPreparationTask = Task.Run(async () => {
            using var pooledDbContext = await dbContextPool.AwaitAnother();
            var dbContext = pooledDbContext.Item;
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.EnsureSuperUserPresenceAsync();
            using var repo = new DataRepository(dbContext, () => { });
            await repo.EnsureDbContentsAsync();
        });

        var httpClient = new Lazy<HttpClient>(() => new HttpClient());

        ConcurrentDictionary<IExecutionContext, DataRepository> scopes = new();

        var facilitiesSetup = new FactoriesSet.FacilitySetup
        {
            CreateLogger = t => new NullLogger<Program>(),
            GetHttpClientAsync = async () => httpClient.Value,
            GetRepositoryAsync = async (executionContext) => {
                if (scopes.TryGetValue(executionContext, out var readyResult))
                {
                    return readyResult;
                }

                await dbPreparationTask;
                var locker = await dbContextPool.AwaitAnother();
                var dbContext = locker.Item;
                var result = new DataRepository(dbContext, () => {
                    locker.Item.ChangeTracker.Clear();
                    locker.Dispose();
                    scopes.TryRemove(executionContext, out var _);
                });
                scopes[executionContext] = result;
                return result;
            }
        };

        businessFactory = new BusinessNetFactory(facilitiesSetup);

        var business = BusinessNetFactoryExtensions.CreateWithoutContext(businessFactory);
        business.Administration.SessionsManagement.OpenFullAccessSessionForTesting.Call(1.ToString());

        var processor = new ApiRequestProcessor
        {
            BusinessNetFactory = businessFactory,
            LogException = logException
        };

        return processor;
    }

    internal static async ValueTask LoadResponseAsync(this HttpListenerResponse response, HttpCallResult callResult, bool compress)
    {
        if (callResult.HttpStatusCode == 0)
        {
            response.StatusCode = 404;
            return;
        }

        response.StatusCode = callResult.HttpStatusCode;

        var bytesResult = callResult.Bytes;

        if (compress)
        {
            bytesResult = bytesResult.Compress();
        } else
        {
            response.ContentType = "application/json";
        }

        using var responseStream = response.OutputStream;
        await responseStream.WriteAsync(bytesResult);
        await responseStream.FlushAsync();
    }

    public static async Task EnsureDbContentsAsync(this IRepository repo)
    {
        var items = InitialData.Entities;

        try
        {
            await repo.CreateAsync(items);
        }
        catch { }
    }
}
