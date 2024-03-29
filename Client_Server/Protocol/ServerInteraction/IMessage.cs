using System;
using System.Linq;

namespace Protocol
{
    public interface IProtocolMessage
    {
        public IProtocolMessageAddin Addin { get; }
    }

    public interface IProtocolMessage<TPayload> : IProtocolMessage
    {
        public TPayload Message { get; }
    }
}