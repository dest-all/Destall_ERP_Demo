using Data.Entities.Documents;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Registers;

[HasNoOwner]
public abstract class RegistryEntry : Entity
{
    public long ActorId { get; set; }

    public DateTime RegisteredAt { get; set; }

    public override void BeforeSave()
    {
        base.BeforeSave();
        RegisteredAt = DateTime.UtcNow;
    }
}

public abstract class StatusMovementRegistryEntry : RegistryEntry
{
    public ushort Status { get; set; }
}
