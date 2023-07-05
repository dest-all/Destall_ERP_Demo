using DestallMaterials.WheelProtection.Copying;
using Protocol.Exceptions;
using Protocol.Models;

namespace Protocol;

public interface IProtocolMessageAddin : IModelBase, ICopied<IProtocolMessageAddin>
{
    IExceptionReadOnlyModel Error { get; }        
    string SessionKey { get; }

    [JsonIgnore]
    bool Errored => Error != null;
    [JsonIgnore]
    bool ErrorHandled => Errored && Error.Index != 0;

    int IModelBase.ComputeChecksum()
    {
        unchecked
        {
            return SessionKey.GetHashCode() + Error.ComputeChecksum();
        }
    }
}