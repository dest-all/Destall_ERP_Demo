using Data.Entities.DataHolders;
using System;

namespace Data.Entities.Documents.Finances;

public partial class StaffPaycheck : FinancialDocument
{
    public Employee PaidTo { get; set; }

    public double CashPart { get; set; }

    public double BankTransferPart { get; set; }

    public double Withheld { get; set; }

    [DestallMaterials.CodeGeneration.ERP.ClientDependency.DateOnlyAttribute]
    public DateTime PeriodStart { get; set; }

    [DestallMaterials.CodeGeneration.ERP.ClientDependency.DateOnlyAttribute(true)]
    public DateTime PeriodEnd { get; set; }
}
