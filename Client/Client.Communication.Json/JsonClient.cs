using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communication.Json
{
    public static partial class JsonClientFactory
    {
        public static IBusinessServerActionInvokersNet CreateJsonWebClient(string backendUrl, out JsonClientCallConfigurator configurator)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(backendUrl);
            var client = JsonClientFactory.CreateJsonWebClient(httpClient, out configurator);
            return client;
        }
    }
}
