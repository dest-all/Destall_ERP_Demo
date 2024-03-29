// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.PaginationRequests;
[MemoryPackable]
public partial class AccountEntryPaginationRequestModel : Protocol.Models.ModelBase, Protocol.Models.PaginationRequests.IAccountEntryPaginationRequestReadOnlyModel, ICopied<AccountEntryPaginationRequestModel>, IPackable<AccountEntryPaginationRequestModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static AccountEntryPaginationRequestModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<AccountEntryPaginationRequestModel>(bytes);
    public AccountEntryPaginationRequestModel Copy() => new(this);
    Protocol.Models.PaginationRequests.IAccountEntryPaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IAccountEntryPaginationRequestReadOnlyModel>.Copy() => ((ICopied<AccountEntryPaginationRequestModel>)this).Copy();
    System.UInt32 IAccountEntryPaginationRequestReadOnlyModel.PageNumber => this.PageNumber;
    public System.UInt32 PageNumber { get; set; }

    System.UInt32 IAccountEntryPaginationRequestReadOnlyModel.Limit => this.Limit;
    public System.UInt32 Limit { get; set; }

    IReadOnlyList<string> IAccountEntryPaginationRequestReadOnlyModel.Orderings => this.Orderings;
    public List<string> Orderings { get; set; } = new();
    Protocol.Models.Filters.IAccountEntryFilterReadOnlyModel IAccountEntryPaginationRequestReadOnlyModel.Filter => this.Filter;
    public Protocol.Models.Filters.AccountEntryFilterModel Filter { get; set; } = new();
    public AccountEntryPaginationRequestModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public AccountEntryPaginationRequestModel(System.UInt32 @pageNumber, //
 System.UInt32 @limit, //
 List<string> @orderings, //
 Protocol.Models.Filters.AccountEntryFilterModel @filter//
    )
    {
        PageNumber = @pageNumber;
        Limit = @limit;
        Filter = @filter;
        Orderings = @orderings;
    }

    public AccountEntryPaginationRequestModel(IAccountEntryPaginationRequestReadOnlyModel from)
    {
        this.PageNumber = from.PageNumber;
        this.Limit = from.Limit;
        Orderings = from?.Orderings.ToList();
        Filter = from.Filter is null ? new() : new(from.Filter);
    }
}