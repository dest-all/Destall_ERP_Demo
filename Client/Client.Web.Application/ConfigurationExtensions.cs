using Client.Communication;
using Client.Web.View;
using Client.Web.View.Services;
using DestallMaterials.Blazor.Services;
using DestallMaterials.Blazor.Services.UI;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using LocalStore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.Application
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureBlazorOnRailWasmApp(this IServiceCollection services,
            Func<IServiceProvider, IBusinessServerActionInvokersNet> invokersNet,
            Func<IServiceProvider, ILocalStore> localStore,
            Func<IServiceProvider, ILogger> logger,
            Func<IServiceProvider, CallConfigurator> callConfigurator,
            Func<IServiceProvider, IAccountManager> accountManager = null,
            Func<IServiceProvider, IFeedManager> feedManager = null,
            Func<IServiceProvider, IDomAccessor> domAccessor = null,
            Func<IServiceProvider, IReferenceClickHandler> clicker = null,
            Func<IServiceProvider, IUiManipulator> uiManipulator = null
            )
        {
            services
                .AddSingleton(invokersNet)
                .AddSingleton(localStore)
                .AddSingleton(domAccessor ?? (sp => new JsDomAccessor(sp.GetRequiredService<IJSRuntime>())))
                .AddSingleton(logger)
                .AddSingleton(sp => new AppConfigurator(sp.GetRequiredService<ILocalStore>()))
                .AddSingleton(sp => {
                    var result = callConfigurator(sp);
                    var appConfigurator = sp.GetRequiredService<AppConfigurator>();
                    BindCallAndAppConfigurators(result, appConfigurator);
                    return result;
                })
                .AddSingleton(accountManager ?? (sp => new AccountManager(
                    sp.GetRequiredService<IBusinessServerActionInvokersNet>(),
                    sp.GetRequiredService<ILocalStore>(),
                    sp.GetRequiredService<ILogger>(),
                    sp.GetRequiredService<CallConfigurator>())))
                .AddSingleton(uiManipulator ?? (sp => new JsUiManipulator(sp.GetRequiredService<IJSRuntime>())))
                .AddSingleton(feedManager ?? (sp => new FeedManager(
                    client: sp.GetRequiredService<IBusinessServerActionInvokersNet>(),
                    localStore: sp.GetRequiredService<ILocalStore>(),
                    uiManipulator: sp.GetRequiredService<IUiManipulator>())))
                .AddSingleton(clicker ?? (sp => new FeedReferenceClicker(sp.GetRequiredService<IFeedManager>())));

            var clickCatcher = new GlobalClickCatcher();

            return services
                .AddSingleton<IGlobalClickCatcher>(clickCatcher)
                .AddSingleton<IGlobalClickInvoker>(clickCatcher)
                .AddSingleton<IScrollSensor, JsScrollSensor>()
                .AddSingleton<IResizeSensor, JsResizeSensor>();
        }

        static void BindCallAndAppConfigurators(CallConfigurator result, AppConfigurator appConfigurator)
        {
            var ready = appConfigurator.TillLoadedAsync();
            result.TillReady = ready.Then(() => {
                var config = appConfigurator.Configuration;
                result.ProtocolOptions = config.ProtocolOptions;
            });
        }
    }
}
