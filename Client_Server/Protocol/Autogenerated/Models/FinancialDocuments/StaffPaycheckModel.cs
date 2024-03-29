// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.FinancialDocuments;
[MemoryPackable]
public partial class StaffPaycheckModel : Protocol.Models.ModelBase, Protocol.Models.FinancialDocuments.IStaffPaycheckReadOnlyModel, ICopied<StaffPaycheckModel>, IPackable<StaffPaycheckModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static StaffPaycheckModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<StaffPaycheckModel>(bytes);
    public override sealed string ToString() => this.Reference?.ToString() ?? base.ToString();
    public StaffPaycheckModel Copy() => new(this);
    Protocol.Models.FinancialDocuments.IStaffPaycheckReadOnlyModel ICopied<Protocol.Models.FinancialDocuments.IStaffPaycheckReadOnlyModel>.Copy() => ((ICopied<StaffPaycheckModel>)this).Copy();
    System.Double IStaffPaycheckReadOnlyModel.CashPart => this.CashPart;
    public System.Double CashPart { get; set; }

    System.Double IStaffPaycheckReadOnlyModel.BankTransferPart => this.BankTransferPart;
    public System.Double BankTransferPart { get; set; }

    System.Double IStaffPaycheckReadOnlyModel.Withheld => this.Withheld;
    public System.Double Withheld { get; set; }

    System.DateTime IStaffPaycheckReadOnlyModel.PeriodStart => this.PeriodStart;
    public System.DateTime PeriodStart { get; set; } = System.DateTime.Now;
    System.DateTime IStaffPaycheckReadOnlyModel.PeriodEnd => this.PeriodEnd;
    public System.DateTime PeriodEnd { get; set; } = System.DateTime.Now;
    System.Double IStaffPaycheckReadOnlyModel.Sum => this.Sum;
    public System.Double Sum { get; set; }

    System.String IStaffPaycheckReadOnlyModel.Number => this.Number;
    public System.String Number { get; set; }

    System.UInt16 IStaffPaycheckReadOnlyModel.Status => this.Status;
    public System.UInt16 Status { get; set; }

    Protocol.Models.FinancialDocuments.IStaffPaycheckReadOnlyReference IStaffPaycheckReadOnlyModel.Reference => this.Reference;
    public Protocol.Models.FinancialDocuments.StaffPaycheckReference Reference { get; set; } = new();
    Protocol.Models.People.IEmployeeReadOnlyReference IStaffPaycheckReadOnlyModel.PaidTo => this.PaidTo;
    public Protocol.Models.People.EmployeeReference PaidTo { get; set; } = new();
    Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference IStaffPaycheckReadOnlyModel.Currency => this.Currency;
    public Protocol.Models.ReferrableEntities.CurrencyReference Currency { get; set; } = new();
    Protocol.Models.People.IEmployeeReadOnlyReference IStaffPaycheckReadOnlyModel.Accountable => this.Accountable;
    public Protocol.Models.People.EmployeeReference Accountable { get; set; } = new();
    public StaffPaycheckModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public StaffPaycheckModel(System.Double @cashPart, //
 System.Double @bankTransferPart, //
 System.Double @withheld, //
 System.DateTime @periodStart, //
 System.DateTime @periodEnd, //
 System.Double @sum, //
 System.String @number, //
 System.UInt16 @status, //
 Protocol.Models.FinancialDocuments.StaffPaycheckReference @reference, //
 Protocol.Models.People.EmployeeReference @paidTo, //
 Protocol.Models.ReferrableEntities.CurrencyReference @currency, //
 Protocol.Models.People.EmployeeReference @accountable//
    )
    {
        CashPart = @cashPart;
        BankTransferPart = @bankTransferPart;
        Withheld = @withheld;
        PeriodStart = @periodStart;
        PeriodEnd = @periodEnd;
        Sum = @sum;
        Number = @number;
        Status = @status;
        Reference = @reference;
        PaidTo = @paidTo;
        Currency = @currency;
        Accountable = @accountable;
    }

    public StaffPaycheckModel(IStaffPaycheckReadOnlyModel from)
    {
        this.CashPart = from.CashPart;
        this.BankTransferPart = from.BankTransferPart;
        this.Withheld = from.Withheld;
        this.PeriodStart = from.PeriodStart;
        this.PeriodEnd = from.PeriodEnd;
        this.Sum = from.Sum;
        this.Number = from.Number;
        this.Status = from.Status;
        Reference = from.Reference is null ? new() : new(from.Reference);
        PaidTo = from.PaidTo is null ? new() : new(from.PaidTo);
        Currency = from.Currency is null ? new() : new(from.Currency);
        Accountable = from.Accountable is null ? new() : new(from.Accountable);
    }
}