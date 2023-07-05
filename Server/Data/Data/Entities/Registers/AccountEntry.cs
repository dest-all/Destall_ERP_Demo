using Data.Entities.DataHolders.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Registers;

public partial class AccountEntry : StatusMovementRegistryEntry
{
    public double SumPayable { get; set; }
    public double SumReceivable { get; set; }

    public Currency Currency { get; set; }

    public Supplier? Creditor { get; set; }

    public Customer? Debtor { get; set; }

    public long? BaseDocumentId { get; set; }
}
