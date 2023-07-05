using Protocol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Protocol.ServerInteraction
{
    public class RequestToServer
    {
        public IProtocolMessageAddin Addin { get; set; }
    }
    public class RequestToServer<TMessage> : RequestToServer, IProtocolMessage<TMessage>
    {
        public TMessage Message { get; set; }
    }


    public class RequestListToServer<TMessage>
        : RequestToServer<List<TMessage>>, IProtocolMessage<IList<TMessage>>
    {
        IList<TMessage> IProtocolMessage<IList<TMessage>>.Message => Message;
    }

    public class RequestListToServer<TMessage, TImplementation>
        : RequestToServer<List<TMessage>>, IProtocolMessage<IList<TMessage>>
        where TImplementation : TMessage
    {
        IList<TMessage> IProtocolMessage<IList<TMessage>>.Message => Message;
    }
        
}
