// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.Filters;
[MemoryPackable]
public partial class StockEntryFilterModel : Protocol.Models.ModelBase, Protocol.Models.Filters.IStockEntryFilterReadOnlyModel, ICopied<StockEntryFilterModel>, IPackable<StockEntryFilterModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static StockEntryFilterModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<StockEntryFilterModel>(bytes);
    public StockEntryFilterModel Copy() => new(this);
    Protocol.Models.Filters.IStockEntryFilterReadOnlyModel ICopied<Protocol.Models.Filters.IStockEntryFilterReadOnlyModel>.Copy() => ((ICopied<StockEntryFilterModel>)this).Copy();
    Protocol.Models.Filters.INumberFilterReadOnlyModel IStockEntryFilterReadOnlyModel.Added => this.Added;
    public Protocol.Models.Filters.NumberFilterModel Added { get; set; }

    Protocol.Models.Filters.INumberFilterReadOnlyModel IStockEntryFilterReadOnlyModel.Reserved => this.Reserved;
    public Protocol.Models.Filters.NumberFilterModel Reserved { get; set; }

    Protocol.Models.Filters.INumberFilterReadOnlyModel IStockEntryFilterReadOnlyModel.Status => this.Status;
    public Protocol.Models.Filters.NumberFilterModel Status { get; set; }

    Protocol.Models.Filters.INumberFilterReadOnlyModel IStockEntryFilterReadOnlyModel.ActorId => this.ActorId;
    public Protocol.Models.Filters.NumberFilterModel ActorId { get; set; }

    Protocol.Models.Filters.IDateTimeFilterReadOnlyModel IStockEntryFilterReadOnlyModel.RegisteredAt => this.RegisteredAt;
    public Protocol.Models.Filters.DateTimeFilterModel RegisteredAt { get; set; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IStockEntryFilterReadOnlyModel.Good => this.Good;
    public Protocol.Models.Filters.ReferenceFilterModel Good { get; set; }

    public StockEntryFilterModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public StockEntryFilterModel(Protocol.Models.Filters.NumberFilterModel @added, //
 Protocol.Models.Filters.NumberFilterModel @reserved, //
 Protocol.Models.Filters.NumberFilterModel @status, //
 Protocol.Models.Filters.NumberFilterModel @actorId, //
 Protocol.Models.Filters.DateTimeFilterModel @registeredAt, //
 Protocol.Models.Filters.ReferenceFilterModel @good//
    )
    {
        Added = @added;
        Reserved = @reserved;
        Status = @status;
        ActorId = @actorId;
        RegisteredAt = @registeredAt;
        Good = @good;
    }

    public StockEntryFilterModel(IStockEntryFilterReadOnlyModel from)
    {
        Added = from.Added is null ? new() : new(from.Added);
        Reserved = from.Reserved is null ? new() : new(from.Reserved);
        Status = from.Status is null ? new() : new(from.Status);
        ActorId = from.ActorId is null ? new() : new(from.ActorId);
        RegisteredAt = from.RegisteredAt is null ? new() : new(from.RegisteredAt);
        Good = from.Good is null ? new() : new(from.Good);
    }
}