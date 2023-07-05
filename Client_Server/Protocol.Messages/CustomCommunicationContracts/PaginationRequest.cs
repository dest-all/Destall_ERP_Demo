using Data.Entities;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;

namespace Protocol.Messages.CustomCommunicationContracts;

public abstract class PaginationRequest : Transportable
{
    public uint PageNumber { get; }
    public uint Limit { get; }
    public ICollection<string> Orderings { get; }
}

[PaginationRequest]
public abstract class PaginationRequest<T> : PaginationRequest
    where T : Entity    
{
}