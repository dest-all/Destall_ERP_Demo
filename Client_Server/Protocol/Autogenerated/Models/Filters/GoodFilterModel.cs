// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.Filters;
[MemoryPackable]
public partial class GoodFilterModel : Protocol.Models.ModelBase, Protocol.Models.Filters.IGoodFilterReadOnlyModel, ICopied<GoodFilterModel>, IPackable<GoodFilterModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static GoodFilterModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<GoodFilterModel>(bytes);
    public GoodFilterModel Copy() => new(this);
    Protocol.Models.Filters.IGoodFilterReadOnlyModel ICopied<Protocol.Models.Filters.IGoodFilterReadOnlyModel>.Copy() => ((ICopied<GoodFilterModel>)this).Copy();
    Protocol.Models.Filters.IStringFilterReadOnlyModel IGoodFilterReadOnlyModel.Name => this.Name;
    public Protocol.Models.Filters.StringFilterModel Name { get; set; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IGoodFilterReadOnlyModel.Reference => this.Reference;
    public Protocol.Models.Filters.ReferenceFilterModel Reference { get; set; }

    public GoodFilterModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public GoodFilterModel(Protocol.Models.Filters.StringFilterModel @name, //
 Protocol.Models.Filters.ReferenceFilterModel @reference//
    )
    {
        Name = @name;
        Reference = @reference;
    }

    public GoodFilterModel(IGoodFilterReadOnlyModel from)
    {
        Name = from.Name is null ? new() : new(from.Name);
        Reference = from.Reference is null ? new() : new(from.Reference);
    }
}