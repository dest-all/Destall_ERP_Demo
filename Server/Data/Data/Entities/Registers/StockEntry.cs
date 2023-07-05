using Data.Entities.DataHolders;
using System;

namespace Data.Entities.Registers;

public partial class StockEntry : StatusMovementRegistryEntry
{
    public double Added { get; set; }
    public double Reserved { get; set; }
    public Good Good { get; set; }
}
