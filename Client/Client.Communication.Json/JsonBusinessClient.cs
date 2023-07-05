using System.Net.Http.Headers;
using Common.AdvancedDataStructures;
using DestallMaterials.WheelProtection.Extensions.Strings;
using Protocol;
using Protocol.Extensions;
using Protocol.MessageExchange;
using static Protocol.Constants;

namespace Client.Communication.Json
{
    class JsonBusinessClient : IDisposable
    {
        readonly HttpClient _httpClient;
        readonly ICallConfigurationGetter _configurator;
        public JsonBusinessClient(HttpClient httpClient, JsonClientCallConfigurator configurator)
        {
            _httpClient = httpClient;
            _configurator = configurator;
        }


        public Task<TIncomingMessage> CallAsync<TIncomingMessage, TPayload>(object invoker, string methodAddress, TPayload payload) 
            => ClientCommunicationPipeline.SendAndReceiveAsync<TIncomingMessage>(
                        async () => await GetMessageAsync<TIncomingMessage, TPayload>(invoker, methodAddress, payload)
                    );

        public Task<TIncomingMessage> CallAsync<TIncomingMessage>(object invoker, string methodAddress)
           => ClientCommunicationPipeline.SendAndReceiveAsync<TIncomingMessage>(
                    async () => await GetMessageAsync<TIncomingMessage, Nothing>(invoker, methodAddress, Nothing.Instance));

        HttpContent ComposeContent<T>(T payload, CallConfiguration callConfig)
        {
            HttpContent content;

            var packingOptions = callConfig.ProtocolOptions;
            
            if (payload is not Nothing)
            {
                ProtocolMessage<T> protocolMessage = ProtocolMessage.FromMessage(payload);
                var bytes = protocolMessage.ToBytes(packingOptions.UseMemoryPack);

                if (packingOptions.Compress)
                {
                    bytes = bytes.Compress();
                }

                content = new ByteArrayContent(bytes);
            }
            else
            {
                content = new StringContent("");
            }

            PastePackingOptionHeaders(content.Headers, packingOptions);

            FillHeaders(callConfig, content.Headers);

            return content;
        }

        async Task<ProtocolMessage<TIncomingMessage>> GetMessageAsync<TIncomingMessage, TPayload>(object invoker, string methodAddress, TPayload payload)
        {
            methodAddress = methodAddress.MustStartWith("api/v1/");

            string fullMethodAddress = _httpClient.BaseAddress.ToString() + methodAddress;

            var callConfig = _configurator.GetConfiguration(invoker, payload);

            await callConfig.Ready;

            HttpContent content = ComposeContent(payload, callConfig);            

            var response = await _httpClient.PostAsync(fullMethodAddress, content, callConfig.CancellationToken);

            var resultBytes = await response.Content.ReadAsByteArrayAsync();

            var result = await ExtractMessageAsync<TIncomingMessage>(response.Content, callConfig.ProtocolOptions);

            return result;

        }

        void FillHeaders(CallConfiguration config, HttpContentHeaders headers)
        {
            foreach (var header in config.Headers)
            {
                headers.Add(header.Key, header.Value);
            }
        }

        public void Dispose()
        {
            ((IDisposable)_httpClient).Dispose();
        }

        static void PastePackingOptionHeaders(HttpContentHeaders headers, ProtocolOptions packingOptions)
        {
            headers.Add(Headers.MemoryPack.Key, packingOptions.UseMemoryPack ? Headers.MemoryPack.Affirmative : Headers.MemoryPack.Negative);
            headers.Add(Headers.ApplyCompression, packingOptions.Compress ? "1" : "0");
        }

        static async Task<ProtocolMessage<TMessage>> ExtractMessageAsync<TMessage>(HttpContent httpContent, ProtocolOptions packingOptions)
        { 
            var bytes = await httpContent.ReadAsByteArrayAsync();

            if (packingOptions.Compress)
            {
                bytes = bytes.Decompress();
            }

            var result = await bytes.ExtractProtocolMessageAsync<TMessage>(packingOptions.UseMemoryPack);

            return result;
        }
    }
}
