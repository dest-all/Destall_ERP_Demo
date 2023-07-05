using Data.Entities.DataHolders.Actors;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System.Collections.Generic;

namespace Data.Entities.Documents.Trade;

[Status("Ready", "Executed", "Closed")]
public partial class OutgoingOrder : Document
{
    public ICollection<OutgoingOrderLine> GoodsBought { get; set; }
    public Supplier Supplier { get; set; }
    public Currency Currency { get; set; }
}