using Data.Entities.DataHolders;

namespace Data.Entities.Registers;

public partial class SalarySettlementEntry : StatusMovementRegistryEntry
{
    public Employee PaidTo { get; set; }

    public double Paid { get; set; }    

    public double Accrued { get; set; }
}
