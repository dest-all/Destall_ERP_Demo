using Data.Entities.Registers;

namespace Data.Entities.Documents.Finances;

public partial class CashEntry : StatusMovementRegistryEntry
{
    public Currency Currency { get; set; }
    public double Added { get; set; }
    public double Reserved { get; set; }
}