// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.PaginationRequests;
[MemoryPackable]
public partial class OutgoingOrderPaginationRequestModel : Protocol.Models.ModelBase, Protocol.Models.PaginationRequests.IOutgoingOrderPaginationRequestReadOnlyModel, ICopied<OutgoingOrderPaginationRequestModel>, IPackable<OutgoingOrderPaginationRequestModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static OutgoingOrderPaginationRequestModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<OutgoingOrderPaginationRequestModel>(bytes);
    public OutgoingOrderPaginationRequestModel Copy() => new(this);
    Protocol.Models.PaginationRequests.IOutgoingOrderPaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IOutgoingOrderPaginationRequestReadOnlyModel>.Copy() => ((ICopied<OutgoingOrderPaginationRequestModel>)this).Copy();
    System.UInt32 IOutgoingOrderPaginationRequestReadOnlyModel.PageNumber => this.PageNumber;
    public System.UInt32 PageNumber { get; set; }

    System.UInt32 IOutgoingOrderPaginationRequestReadOnlyModel.Limit => this.Limit;
    public System.UInt32 Limit { get; set; }

    IReadOnlyList<string> IOutgoingOrderPaginationRequestReadOnlyModel.Orderings => this.Orderings;
    public List<string> Orderings { get; set; } = new();
    Protocol.Models.Filters.IOutgoingOrderFilterReadOnlyModel IOutgoingOrderPaginationRequestReadOnlyModel.Filter => this.Filter;
    public Protocol.Models.Filters.OutgoingOrderFilterModel Filter { get; set; } = new();
    public OutgoingOrderPaginationRequestModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public OutgoingOrderPaginationRequestModel(System.UInt32 @pageNumber, //
 System.UInt32 @limit, //
 List<string> @orderings, //
 Protocol.Models.Filters.OutgoingOrderFilterModel @filter//
    )
    {
        PageNumber = @pageNumber;
        Limit = @limit;
        Filter = @filter;
        Orderings = @orderings;
    }

    public OutgoingOrderPaginationRequestModel(IOutgoingOrderPaginationRequestReadOnlyModel from)
    {
        this.PageNumber = from.PageNumber;
        this.Limit = from.Limit;
        Orderings = from?.Orderings.ToList();
        Filter = from.Filter is null ? new() : new(from.Filter);
    }
}