// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.Filters;
[MemoryPackable]
public partial class IncomingPaymentFilterModel : Protocol.Models.ModelBase, Protocol.Models.Filters.IIncomingPaymentFilterReadOnlyModel, ICopied<IncomingPaymentFilterModel>, IPackable<IncomingPaymentFilterModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static IncomingPaymentFilterModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<IncomingPaymentFilterModel>(bytes);
    public IncomingPaymentFilterModel Copy() => new(this);
    Protocol.Models.Filters.IIncomingPaymentFilterReadOnlyModel ICopied<Protocol.Models.Filters.IIncomingPaymentFilterReadOnlyModel>.Copy() => ((ICopied<IncomingPaymentFilterModel>)this).Copy();
    Protocol.Models.Filters.INumberFilterReadOnlyModel IIncomingPaymentFilterReadOnlyModel.Sum => this.Sum;
    public Protocol.Models.Filters.NumberFilterModel Sum { get; set; }

    Protocol.Models.Filters.IStringFilterReadOnlyModel IIncomingPaymentFilterReadOnlyModel.Number => this.Number;
    public Protocol.Models.Filters.StringFilterModel Number { get; set; }

    Protocol.Models.Filters.INumberFilterReadOnlyModel IIncomingPaymentFilterReadOnlyModel.Status => this.Status;
    public Protocol.Models.Filters.NumberFilterModel Status { get; set; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IIncomingPaymentFilterReadOnlyModel.Reference => this.Reference;
    public Protocol.Models.Filters.ReferenceFilterModel Reference { get; set; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IIncomingPaymentFilterReadOnlyModel.Order => this.Order;
    public Protocol.Models.Filters.ReferenceFilterModel Order { get; set; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IIncomingPaymentFilterReadOnlyModel.Payer => this.Payer;
    public Protocol.Models.Filters.ReferenceFilterModel Payer { get; set; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IIncomingPaymentFilterReadOnlyModel.Currency => this.Currency;
    public Protocol.Models.Filters.ReferenceFilterModel Currency { get; set; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IIncomingPaymentFilterReadOnlyModel.Accountable => this.Accountable;
    public Protocol.Models.Filters.ReferenceFilterModel Accountable { get; set; }

    public IncomingPaymentFilterModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public IncomingPaymentFilterModel(Protocol.Models.Filters.NumberFilterModel @sum, //
 Protocol.Models.Filters.StringFilterModel @number, //
 Protocol.Models.Filters.NumberFilterModel @status, //
 Protocol.Models.Filters.ReferenceFilterModel @reference, //
 Protocol.Models.Filters.ReferenceFilterModel @order, //
 Protocol.Models.Filters.ReferenceFilterModel @payer, //
 Protocol.Models.Filters.ReferenceFilterModel @currency, //
 Protocol.Models.Filters.ReferenceFilterModel @accountable//
    )
    {
        Sum = @sum;
        Number = @number;
        Status = @status;
        Reference = @reference;
        Order = @order;
        Payer = @payer;
        Currency = @currency;
        Accountable = @accountable;
    }

    public IncomingPaymentFilterModel(IIncomingPaymentFilterReadOnlyModel from)
    {
        Sum = from.Sum is null ? new() : new(from.Sum);
        Number = from.Number is null ? new() : new(from.Number);
        Status = from.Status is null ? new() : new(from.Status);
        Reference = from.Reference is null ? new() : new(from.Reference);
        Order = from.Order is null ? new() : new(from.Order);
        Payer = from.Payer is null ? new() : new(from.Payer);
        Currency = from.Currency is null ? new() : new(from.Currency);
        Accountable = from.Accountable is null ? new() : new(from.Accountable);
    }
}