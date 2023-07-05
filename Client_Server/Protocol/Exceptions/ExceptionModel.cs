using DestallMaterials.WheelProtection.Copying;
using MemoryPack;
using Protocol.Models;

namespace Protocol.Exceptions;

public partial interface IExceptionReadOnlyModel : IModelBase, ICopied<IExceptionReadOnlyModel>
{
    public string Message { get; set; }
    public byte Index { get; set; }

    int IModelBase.ComputeChecksum()
    {
        unchecked
        {
            return Message.GetHashCode() + Index * 1500;
        }
    }
}

[MemoryPackable]
public partial class ExceptionModel : IExceptionReadOnlyModel, IPackable<ExceptionModel>
{
    public ExceptionModel()
    {
        
    }

    [MemoryPackConstructor]
    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public ExceptionModel(string message, byte index)
    {
        Message = message;
        Index = index;
    }

    public string Message { get ; set ; }
    public byte Index { get; set; }

    public static ExceptionModel Unpack(byte[] bytes)
        => MemoryPackSerializer.Deserialize<ExceptionModel>(bytes);

    public ExceptionModel Copy() => new(Message, Index);

    public byte[] Pack()
        => MemoryPackSerializer.Serialize(this);

    IExceptionReadOnlyModel ICopied<IExceptionReadOnlyModel>.Copy() => Copy();
}

public static class ExceptionModelExtensions
{
    public static bool IsHandled(this IExceptionReadOnlyModel exception) => exception?.Index != 0;
}
