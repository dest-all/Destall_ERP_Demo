using DestallMaterials.CodeGeneration.ERP.ClientDependency;

namespace Data.Entities.Documents.Finances;

[Status("Ready", "Executed", "Closed")]
public abstract partial class FinancialDocument : Document
{
    public Currency Currency { get; set; }

    public double Sum { get; set; }
}
