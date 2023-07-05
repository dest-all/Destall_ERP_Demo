using Protocol;
using static Protocol.Constants;

namespace Client.Communication.Extensions
{
    public static class CallConfiguratorExtensions
    {
        const string _sessionKeyHeader = Headers.SessionKey;

        static CallConfiguration Mutate(this CallConfiguration callConfiguration, Action<CallConfiguration> mutation)
        {
            if (callConfiguration is CallConfiguration result)
            {
                mutation(result);
            } else
            {
                result = new CallConfiguration
                {
                    CancellationToken = callConfiguration.CancellationToken,
                    Deadline = callConfiguration.Deadline,
                    Headers = callConfiguration.Headers.ToDictionary(kv => kv.Key, kv => kv.Value)
                };
                mutation(result);
            }
            return result;

        }

        public static CallConfigurator AssignSessionKeyHeader(this CallConfigurator callConfigurator, Func<string> sessionKeySource)
        {
            var current = callConfigurator.ConfigurationFactory;

            callConfigurator.ConfigurationFactory = (a, b) => {
                var result = current(a, b);
                result = result.Mutate(callConfiguration => callConfiguration.Headers[_sessionKeyHeader] = sessionKeySource().ToString());
                return result;
            };

            return callConfigurator;
        }

        public static CallConfigurator ConfigureProtocol(this CallConfigurator callConfigurator, bool useMemoryPack, bool useCompression)
        {
            var current = callConfigurator.ConfigurationFactory;


            callConfigurator.ConfigurationFactory = (a, b) => {
                var currentConfig = current(a, b);

                var result = currentConfig.Mutate(cc => {
                    if (useCompression)
                    {
                        cc.Headers[Headers.ApplyCompression] = "1";
                    }
                    if (useMemoryPack)
                    {
                        cc.Headers[Headers.MemoryPack.Key] = Headers.MemoryPack.Affirmative;
                    }
                });
                return result;
            };

            return callConfigurator;
        }
    }
}
