using Protocol;
using Protocol.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communication;

public static class ClientCommunicationPipeline
{
    public static async Task<TIncomingMessage> SendAndReceiveAsync<TIncomingMessage>
        (Func<Task<IProtocolMessage<TIncomingMessage>>> action)
    {
        var result = await action();
        bool isErrored = result?.Addin?.Errored == true;
        if (isErrored)
        {
            var exception = result.Addin.Error.ToException();
            if (result.Addin.ErrorHandled)
            {
                throw new ServerSideException("Server threw exception on incorrect request.", exception);
            }
            else
            {
                throw new ServerSideUnhandledException("Server unhandled exception.", exception);
            }
        }

        return result.Message;
    }
}
