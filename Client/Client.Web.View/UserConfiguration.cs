using Client.Communication;
using Common.Extensions.Object;
using LocalStore;
using Protocol.MessageExchange;

namespace Client.Web.View;


public class AppConfiguration
{
    public ProtocolOptions ProtocolOptions { get; set; } = new();

    public AppConfiguration()
    {
    }
}

public class AppConfigurator
{
    const string _configKey = "AppConfiguration";

    readonly ILocalStore _localStore;

    AppConfiguration _configuration = new();

    Task _loading;

    public AppConfiguration Configuration
    {
        get
        {
            if (!_loading.IsCompleted)
            {
                throw new InvalidOperationException("Configuration hasn't been loaded yet.");
            }
            return _configuration;
        }
    }


    public AppConfigurator(ILocalStore localStore)
    {
        _localStore = localStore;
        _loading = Task.Run(async () => 
        {
            _configuration = await _localStore.GetAsync<AppConfiguration>(_configKey);
            if (_configuration is null)
            {
                _configuration = new AppConfiguration();
                await SaveAsync();
            }
        });
    }

    public Task TillLoadedAsync()
        => _loading;

    public async Task SaveAsync()
    {
        await _localStore.PutAsync(_configKey, _configuration);
    }
}
