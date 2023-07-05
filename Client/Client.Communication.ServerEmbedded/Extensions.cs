using Common.AdvancedDataStructures;
using Protocol;

namespace Client.Communication.ServerEmbedded;

internal class ServerEmbeddedExecutionContext : IExecutionContext
{
    static readonly IdsGenerator _idsGenerator = new IdsGenerator();
    public long OperationId { get; } = _idsGenerator.Generate();

    public string SessionKey { get; init; }

    public string Language { get; set; } = Constants.Languages.English;
}
public static class Extensions
{
    public static IExecutionContext ToExecutionContext(this CallConfiguration callConfiguration)
    {
        callConfiguration.Headers.TryGetValue(Constants.Headers.SessionKey, out var sessionKey);
        return new ServerEmbeddedExecutionContext
        {
            SessionKey = sessionKey ?? ""
        };
    }

    public static CallConfiguration ToCallConfiguration(this IExecutionContext executionContext)
        => new InMemoryCallConfugiration
        {
            Deadline = TimeSpan.MaxValue,
            Headers = new Dictionary<string, string>()
            {
                { "SessionKey", executionContext.SessionKey.ToString() }
            }
        };
}
