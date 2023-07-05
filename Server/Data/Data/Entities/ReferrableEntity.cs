using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using DestallMaterials.CodeGeneration.ERP.Models.Data.Entity;
using System;

namespace Data.Entities;

[Referrable]
public abstract partial class ReferrableEntity : Entity
{
    [ExcludeFromModels]
    public DateTime DateCreated { get; set; }

    [ExcludeFromModels]
    public DateTime DateUpdated { get; set; }

    [ExcludeFromModels]
    public string Representation { get; set; }

    public virtual string GetRepresentation()
        => $"{Metadata.Representation.Singular} of {DateCreated.ToShortDateString()}";

    public override void BeforeSave()
    {
        base.BeforeSave();
        if (DateCreated == default)
        {
            DateCreated = DateTime.UtcNow;
            DateUpdated = DateCreated;
        }
        else
        {
            DateUpdated = DateTime.UtcNow;
        }
        Representation = GetRepresentation();

        

    }

    public override string ToString()
     => Representation;
}