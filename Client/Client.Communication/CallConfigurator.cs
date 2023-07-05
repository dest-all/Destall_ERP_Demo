using Common.AdvancedDataStructures;
using DestallMaterials.WheelProtection.Copying;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using Protocol.MessageExchange;
using static Protocol.Constants;

namespace Client.Communication;

public enum HttpMessageProtocol
{
    MemoryPack,
    Json
}

public class CallConfiguration
{
    public CancellationToken CancellationToken { get; set; }

    public TimeSpan Deadline { get; set; }

    public Dictionary<string, string> Headers { get; set; } = new();

    public ProtocolOptions ProtocolOptions { get; set; }

    public Task Ready { get; init; } = Task.CompletedTask;

    public DateTime DoTill => Deadline.Ticks > long.MaxValue / 2 ? DateTime.MaxValue : DateTime.UtcNow + Deadline;

}


public interface ICallConfigurationGetter
{
    CallConfiguration GetConfiguration(object invoker, object parameter);
    CallConfiguration GetConfiguration(object invoker);

    Task TillReady { get; }
}

public class CallConfigurator : ICallConfigurationGetter
{
    public CallConfigurator()
    {
        ConfigurationFactory = (a, b) => CreateDefaultConfiguration();
    }

    CallConfiguration CreateDefaultConfiguration() => new CallConfiguration
    {
        CancellationToken = default,
        Deadline = TimeSpan.MaxValue / 2,
        Headers = new(),
        ProtocolOptions = ProtocolOptions
    };

    public Func<object, object, CallConfiguration> ConfigurationFactory { get; set; }

    public ProtocolOptions ProtocolOptions { get; set; } = new();

    CallConfiguration ICallConfigurationGetter.GetConfiguration(object invoker, object parameter)
    {
        var result = ConfigurationFactory(invoker, parameter);

        result = new CallConfiguration
        {
            CancellationToken = result.CancellationToken,
            Deadline = result.Deadline,
            Headers = result.Headers,
            ProtocolOptions = result.ProtocolOptions,
            Ready = Task.WhenAll(result.Ready, TillReady)
        };

        return result;
    }
    CallConfiguration ICallConfigurationGetter.GetConfiguration(object invoker)
        => ConfigurationFactory(invoker, Nothing.Instance);

    public Task TillReady { get; set; } = Task.CompletedTask;
}
