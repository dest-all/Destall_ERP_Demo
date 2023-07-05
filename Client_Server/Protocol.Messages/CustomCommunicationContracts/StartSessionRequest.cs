using Data.Entities;
using Data.Entities.DataHolders.AccountingUsers;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;

namespace Protocol.Messages.CustomCommunicationContracts;

public class StartSessionRequest : Transportable
{
    public string Login { get; }
    public string Password { get; }
}

public partial class StartSessionResponse : Transportable
{
    public string SessionKey { get; }
    
    [IncludeFull]
    public User User { get; }
}

public class VerifySessionRequest : Transportable
{
    public string SessionKey { get; }
    public long SessionUserId { get; }
}