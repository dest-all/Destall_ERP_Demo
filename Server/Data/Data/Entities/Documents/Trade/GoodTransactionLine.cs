using Data.Entities.DataHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Documents.Trade
{
    public abstract partial class GoodTransactionLine : TableLine
    {
        public Good Good { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}