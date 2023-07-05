using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communication.Grpc;

public static partial class GrpcClientFactory
{
    public static IBusinessServerActionInvokersNet CreateForAddress(string serverAddress, out GrpcCallConfigurator callConfigurator)
        => CreateGrpcWebClient(GrpcChannel.ForAddress(serverAddress, new GrpcChannelOptions
        {
            HttpHandler = new GrpcWebHandler(new HttpClientHandler())
        }), out callConfigurator);
}
