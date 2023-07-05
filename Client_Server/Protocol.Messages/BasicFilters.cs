using DestallMaterials.CodeGeneration.ERP.Models.Data.Entity;
using System;
using System.Collections.Generic;

namespace Protocol.Messages
{
    [Filter]
    public abstract class Filter : Transportable
    {
    }

    public class StringFilter : Filter
    {
        public string Pattern { get; }
    }

    public class NumberFilter : Filter
    {
        public double LeftLimit { get; }
        public double RightLimit { get; }
    }

    public class DateTimeFilter : Filter
    {
        public DateTime LeftLimit { get; }
        public DateTime RightLimit { get; }
    }

    public class BooleanFilter : Filter
    {
        public bool Value { get; }
    }

    public class ReferenceFilter : Filter
    {
        public StringFilter Representation { get; }
        public NumberFilter Id { get; }
        public ICollection<long> In { get; }
        public ICollection<long> NotIn { get; }

    }

    public class CollectionFilter : Filter
    {
        public NumberFilter Count { get; }
    }

    public class ReferenceCollectionFilter : Filter
    {
        public ReferenceFilter Any { get; }
        public ReferenceFilter All { get; }
    }

    /* public class CollectionFilter@(L)TFilter@(G)
        : CollectionFilter where TFilter : Filter
        {
        public TFilter All { get; }
        public TFilter Any { get; }
        } */

}