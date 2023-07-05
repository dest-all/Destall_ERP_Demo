using Data.Entities.DataHolders.Actors;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System.Collections.Generic;

namespace Data.Entities.Documents.Trade;

[Status("Ready", "Executed", "Closed")]
public partial class IncomingOrder : Documents.Document
{
    public ICollection<IncomingOrderLine> GoodsSold { get; set; }

    public Customer Customer { get; set; }

    public Currency Currency { get; set; }
}