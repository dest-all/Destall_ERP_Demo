using MemoryPack;
using Protocol.Exceptions;
using Protocol.Models;
using Protocol.Models.Documents;
using Protocol.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Protocol;

[MemoryPackable]
public partial class EmployeeModelProtocolMessage : ProtocolMessage<EmployeeModel>
{
    
}

[MemoryPackable]
public partial class IncomingOrderProtocolMessage : ProtocolMessage<IncomingOrderModel>
{ 
}

public class ProtocolMessage : IProtocolMessage
{
    public ProtocolMessageAddin Addin { get; set; }

    IProtocolMessageAddin IProtocolMessage.Addin => Addin;

    public static ProtocolMessage<TMessage> FromMessage<TMessage>(TMessage message)
        => new ProtocolMessage<TMessage>(message, default);

    public static ProtocolMessage FromError(IExceptionReadOnlyModel error)
        => new ProtocolMessage
        {
            Addin = new ProtocolMessageAddin(error)
        };


    public static ProtocolMessage<TMessage> FromTuple<TMessage>(ValueTuple<ProtocolMessageAddin, TMessage> tuple)
        => new ProtocolMessage<TMessage>
        {
            Message = tuple.Item2,
            Addin = tuple.Item1
        };

    public static ProtocolMessage<TMessage> FromTuple<TMessage>(ValueTuple<TMessage, ProtocolMessageAddin> tuple)
       => new ProtocolMessage<TMessage>
       {
           Message = tuple.Item1,
           Addin = tuple.Item2
       };

    public static ProtocolMessage<TMessage> FromError<TMessage>(IExceptionReadOnlyModel error)
        => new ProtocolMessage<TMessage>
        {
            Addin = new ProtocolMessageAddin(error)
        };

    public static ProtocolMessage<TMessage> Unpack<TMessage>(byte[] bytes)
        => ProtocolMessage<TMessage>.Unpack(bytes);
}

public class ProtocolMessage<TMessage> : ProtocolMessage, IProtocolMessage<TMessage>, IPackable<ProtocolMessage<TMessage>>
{
    public TMessage Message { get; init; }


    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public ProtocolMessage(TMessage message, ProtocolMessageAddin addin)
    {
        Message = message;
        Addin = addin;
    }

    public ProtocolMessage(ProtocolMessageAddin addin)
    {
        Addin = addin;
    }

    public ProtocolMessage(IExceptionReadOnlyModel error)
    {
        Addin = new ProtocolMessageAddin(error);
    }

    public ProtocolMessage() 
    {
    }

    public static ProtocolMessage<TMessage> FromTuple(ValueTuple<ProtocolMessageAddin, TMessage> tuple) 
        => new ProtocolMessage<TMessage>
        {
            Message = tuple.Item2,
            Addin = tuple.Item1
        };

    public static ProtocolMessage<TMessage> FromTuple(ValueTuple<TMessage, ProtocolMessageAddin> tuple)
       => new ProtocolMessage<TMessage>
       {
           Message = tuple.Item1,
           Addin = tuple.Item2
       };

    public ValueTuple<ProtocolMessageAddin, TMessage> ToTuple() => new(Addin, Message);

    public static ProtocolMessage<TMessage> Unpack(byte[] bytes)
    {
        var tuple = MemoryPackSerializer.Deserialize<ValueTuple<ProtocolMessageAddin, TMessage>>(bytes);

        var result = ProtocolMessage.FromTuple(tuple);

        return result;
    }

    public byte[] Pack()
    {
        var tuple = this.ToTuple();
        var result = MemoryPackSerializer.Serialize(tuple);
        return result;
    }
}

//public class ProtocolMessage<TMessage> : ProtocolMessage, IProtocolMessage<TMessage>
//{
//    public ProtocolMessage(TMessage message)
//    {
//        Message = message;
//    }

//    public ProtocolMessage(ProtocolMessageAddin status)
//    {

//        Status = status;
//    }

//    public ProtocolMessage(IExceptionReadOnlyModel exception)
//    {
//        Status = new ProtocolMessageAddin(exception);
//    }

//    public ProtocolMessage()
//    { 
//    }

//    [JsonConstructor]
//    public ProtocolMessage(TMessage message, ProtocolMessageAddin status) : this(message)
//    {
//        Status = status;
//    }

//    public TMessage Message { get; set; }

//    TMessage IProtocolMessage<TMessage>.Payload => Message;
//}

public class ServerListResponse<TMessage, TMessageImplementation> : ProtocolMessage, IProtocolMessage<IList<TMessage>>
    where TMessageImplementation : TMessage
{
    public ServerListResponse(IList<TMessage> message)
    {
        Message = message;
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public ServerListResponse(List<TMessageImplementation> message, ProtocolMessageAddin status)
    {
        Addin = status;
        Message = (message ?? Enumerable.Empty<TMessageImplementation>()).Select(i => (TMessage)i).ToList();
    }

    public ServerListResponse(IExceptionReadOnlyModel exceptionModel)
    {
        Addin = new ProtocolMessageAddin(exceptionModel);
    }

    public IList<TMessage> Message { get; set; }

    IList<TMessage> IProtocolMessage<IList<TMessage>>.Message => Message;
}