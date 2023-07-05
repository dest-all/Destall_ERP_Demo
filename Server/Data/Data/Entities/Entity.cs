
using Common.AdvancedDataStructures;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System;
using System.Linq.Expressions;

namespace Data.Entities
{

    [Transported]
    [Entity]
    public abstract partial class Entity
    {
        internal static readonly IdsGenerator _idsGenerator = new(new DateTime(2020, 01, 01));

        public long Id { get; set; }
        public virtual void BeforeSave()
        {
            EnsureValidId();
        }

        public long EnsureValidId()
        {
            if (Id <= 0)
            {
                Id = _idsGenerator.Generate();
            }
            return Id;
        }

        public long AssignNewId()
        {
            if (Id <= 0)
            {
                Id = _idsGenerator.Generate();
                return Id;
            }
            else
            {
                throw new InvalidOperationException($"{this.GetType().Name}'s id is already assigned: {Id}.");
            };
        }
    }
}