using Data.Entities.DataHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Documents.Trade
{
    public partial class IncomingOrderLine : GoodTransactionLine
    {
        public IncomingOrder IncomingOrder { get; set; }
    }
}