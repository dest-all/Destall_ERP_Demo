using Data.Entities.DataHolders;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System;
using System.Linq.Expressions;

namespace Data.Entities.Documents
{
    [Permissions("Move")]
    [Status("Draft", "Submitted")]
    public abstract partial class Document : ReferrableEntity
    {
        public static Expression<Func<Document, string>> ReferenceRepresentation { get; } = d => d.Number;

        public string Number { get; set; }

        public ushort Status { get; set; }

        public override string GetRepresentation() => $"{Metadata.Representation.Singular} #{Number}"; 

        public Employee Accountable { get; set; }
    }
}