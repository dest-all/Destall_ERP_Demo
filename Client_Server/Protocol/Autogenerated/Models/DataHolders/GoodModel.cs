// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.DataHolders;
[MemoryPackable]
public partial class GoodModel : Protocol.Models.ModelBase, Protocol.Models.DataHolders.IGoodReadOnlyModel, ICopied<GoodModel>, IPackable<GoodModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static GoodModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<GoodModel>(bytes);
    public override sealed string ToString() => this.Reference?.ToString() ?? base.ToString();
    public GoodModel Copy() => new(this);
    Protocol.Models.DataHolders.IGoodReadOnlyModel ICopied<Protocol.Models.DataHolders.IGoodReadOnlyModel>.Copy() => ((ICopied<GoodModel>)this).Copy();
    System.String IGoodReadOnlyModel.Name => this.Name;
    public System.String Name { get; set; }

    Protocol.Models.DataHolders.IGoodReadOnlyReference IGoodReadOnlyModel.Reference => this.Reference;
    public Protocol.Models.DataHolders.GoodReference Reference { get; set; } = new();
    public GoodModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public GoodModel(System.String @name, //
 Protocol.Models.DataHolders.GoodReference @reference//
    )
    {
        Name = @name;
        Reference = @reference;
    }

    public GoodModel(IGoodReadOnlyModel from)
    {
        this.Name = from.Name;
        Reference = from.Reference is null ? new() : new(from.Reference);
    }
}