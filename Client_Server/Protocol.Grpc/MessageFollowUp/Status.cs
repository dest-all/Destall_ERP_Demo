using DestallMaterials.WheelProtection.Copying;
using Protocol.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Grpc.ProtoModels
{
    public partial class ProtocolMessageAddin : Protocol.IProtocolMessageAddin, ICopied<ProtocolMessageAddin>
    {
        IExceptionReadOnlyModel IProtocolMessageAddin.Error => error_;

        ProtocolMessageAddin ICopied<ProtocolMessageAddin>.Copy() => Clone();

        IProtocolMessageAddin ICopied<IProtocolMessageAddin>.Copy() => Clone();
    }
}
