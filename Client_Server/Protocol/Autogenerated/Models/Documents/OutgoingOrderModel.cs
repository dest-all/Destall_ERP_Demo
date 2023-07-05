// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.Documents;
[MemoryPackable]
public partial class OutgoingOrderModel : Protocol.Models.ModelBase, Protocol.Models.Documents.IOutgoingOrderReadOnlyModel, ICopied<OutgoingOrderModel>, IPackable<OutgoingOrderModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static OutgoingOrderModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<OutgoingOrderModel>(bytes);
    public override sealed string ToString() => this.Reference?.ToString() ?? base.ToString();
    public OutgoingOrderModel Copy() => new(this);
    Protocol.Models.Documents.IOutgoingOrderReadOnlyModel ICopied<Protocol.Models.Documents.IOutgoingOrderReadOnlyModel>.Copy() => ((ICopied<OutgoingOrderModel>)this).Copy();
    System.String IOutgoingOrderReadOnlyModel.Number => this.Number;
    public System.String Number { get; set; }

    System.UInt16 IOutgoingOrderReadOnlyModel.Status => this.Status;
    public System.UInt16 Status { get; set; }

    Protocol.Models.Documents.IOutgoingOrderReadOnlyReference IOutgoingOrderReadOnlyModel.Reference => this.Reference;
    public Protocol.Models.Documents.OutgoingOrderReference Reference { get; set; } = new();
    IReadOnlyList<Protocol.Models.GoodTransactionLines.IOutgoingOrderLineReadOnlyModel> IOutgoingOrderReadOnlyModel.GoodsBought => this.GoodsBought;
    public List<Protocol.Models.GoodTransactionLines.OutgoingOrderLineModel> GoodsBought { get; set; } = new();
    Protocol.Models.Counterparties.ISupplierReadOnlyReference IOutgoingOrderReadOnlyModel.Supplier => this.Supplier;
    public Protocol.Models.Counterparties.SupplierReference Supplier { get; set; } = new();
    Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference IOutgoingOrderReadOnlyModel.Currency => this.Currency;
    public Protocol.Models.ReferrableEntities.CurrencyReference Currency { get; set; } = new();
    Protocol.Models.People.IEmployeeReadOnlyReference IOutgoingOrderReadOnlyModel.Accountable => this.Accountable;
    public Protocol.Models.People.EmployeeReference Accountable { get; set; } = new();
    public OutgoingOrderModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public OutgoingOrderModel(System.String @number, //
 System.UInt16 @status, //
 Protocol.Models.Documents.OutgoingOrderReference @reference, //
 List<Protocol.Models.GoodTransactionLines.OutgoingOrderLineModel> @goodsBought, //
 Protocol.Models.Counterparties.SupplierReference @supplier, //
 Protocol.Models.ReferrableEntities.CurrencyReference @currency, //
 Protocol.Models.People.EmployeeReference @accountable//
    )
    {
        Number = @number;
        Status = @status;
        Reference = @reference;
        Supplier = @supplier;
        Currency = @currency;
        Accountable = @accountable;
        GoodsBought = @goodsBought;
    }

    public OutgoingOrderModel(IOutgoingOrderReadOnlyModel from)
    {
        this.Number = from.Number;
        this.Status = from.Status;
        Reference = from.Reference is null ? new() : new(from.Reference);
        GoodsBought = from.GoodsBought?.Select(i => new Protocol.Models.GoodTransactionLines.OutgoingOrderLineModel(i)).ToList() ?? new();
        Supplier = from.Supplier is null ? new() : new(from.Supplier);
        Currency = from.Currency is null ? new() : new(from.Currency);
        Accountable = from.Accountable is null ? new() : new(from.Accountable);
    }
}