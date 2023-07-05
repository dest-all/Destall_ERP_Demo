// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using DestallMaterials.WheelProtection.Copying;
using DestallMaterials.WheelProtection.Extensions.Strings;
using MemoryPack;

namespace Protocol.Models.DataHolders;
[MemoryPackable]
public partial class GoodReference : Protocol.Models.Reference, Protocol.Models.DataHolders.IGoodReadOnlyReference, ICopied<GoodReference>, IPackable<GoodReference>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static GoodReference Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<GoodReference>(bytes);
    public GoodReference()
    {
    }

    public GoodReference(Protocol.Models.DataHolders.IGoodReadOnlyReference other)
    {
        this.Id = other.Id;
        this.Representation = other.Representation;
    }

    [JsonConstructor]
    [MemoryPackConstructor]
    public GoodReference(long id, string representation)
    {
        this.Id = id;
        this.Representation = representation;
    }

    public GoodReference Copy() => new(this);
    Protocol.Models.DataHolders.IGoodReadOnlyReference ICopied<Protocol.Models.DataHolders.IGoodReadOnlyReference>.Copy() => Copy();
    public override sealed string ToString() => Representation.HasContent() ? Representation : $"Good #{Id}";
}