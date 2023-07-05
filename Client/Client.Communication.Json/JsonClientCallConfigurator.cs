using Protocol.MessageExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communication.Json
{
    public class JsonClientCallConfiguration : CallConfiguration
    {
        public JsonClientCallConfiguration()
        {
        }

        public JsonClientCallConfiguration(CallConfiguration other)
        {
            CancellationToken = other.CancellationToken;
            Deadline = other.Deadline;
            Headers = other.Headers;
        }

        public CancellationToken CancellationToken { get; init; }

        public TimeSpan Deadline { get; init; }

        public IReadOnlyDictionary<string, string> Headers { get; init; }

        public PackingOptions PackingOptions { get; init; }
    }


    public class JsonClientCallConfigurator : CallConfigurator
    {
    }
}
