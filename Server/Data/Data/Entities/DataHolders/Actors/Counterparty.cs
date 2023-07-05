using System;
using System.Linq.Expressions;

namespace Data.Entities.DataHolders.Actors
{
    public abstract class Counterparty : DataHolder
    {
        public static Expression<Func<Counterparty, string>> ReferenceRepresentation { get; } = p => p.Name;

        public override string GetRepresentation()
        {
            return Name;
        }

        public string Name { get; set; }
    }
}