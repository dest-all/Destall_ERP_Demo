using DestallMaterials.WheelProtection.Copying;
using MemoryPack;
using Protocol.Exceptions;
using Protocol.Models;

namespace Protocol;

[MemoryPack.MemoryPackable]
public partial class ProtocolMessageAddin : IProtocolMessageAddin, ICopied<ProtocolMessageAddin>, IPackable<IProtocolMessageAddin>
{
    [JsonConstructor]
    [MemoryPackConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public ProtocolMessageAddin(ExceptionModel error, string sessionKey) : this(error)
    {
        SessionKey = sessionKey;
    }

    public ProtocolMessageAddin(IExceptionReadOnlyModel error)
    {
        if (error is null)
        {
            return;
        }
        Error = new()
        {
            Index = error.Index,
            Message = error.Message
        };
    }

    public ProtocolMessageAddin()
    { 
    }

    public ExceptionModel Error { get; set; }

    public string SessionKey { get; set; }

    IExceptionReadOnlyModel IProtocolMessageAddin.Error => Error;

    IProtocolMessageAddin ICopied<IProtocolMessageAddin>.Copy()
        => Copy();

    public ProtocolMessageAddin Copy() => new ProtocolMessageAddin(Error)
    {
        SessionKey = SessionKey
    };

    public byte[] Pack() => MemoryPack.MemoryPackSerializer.Serialize(this);

    public static IProtocolMessageAddin Unpack(byte[] bytes) => MemoryPack.MemoryPackSerializer.Deserialize<ProtocolMessageAddin>(bytes);

}
