using Protocol;

namespace Web.Endpoints.Api;

public static class CallContextUnifier
{
    class ExecutionContext : IExecutionContext
    {
        public long OperationId { get; init; }

        public string SessionKey { get; init; }

        public string Language { get; init; } = Constants.Languages.English;
    }

    const string _sessionKeyHeader = Constants.Headers.SessionKey;

    public static IExecutionContext ToExecutionContext(this HttpRequestData request)
    {
        string sessionKey = request.Headers.ContainsKey(_sessionKeyHeader) == true ? request.Headers[_sessionKeyHeader] : "0";

        return new ExecutionContext
        {
            SessionKey = sessionKey
        };
    }
}