using DestallMaterials.WheelProtection.Copying;
using Protocol.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Grpc.ProtoModels
{
    public sealed partial class ProtoException : IExceptionReadOnlyModel, ICopied<ProtoException>
    {
        byte IExceptionReadOnlyModel.Index { get { return (byte)Index; } set { Index = value; } }

        public ProtoException Copy() => new ProtoException
        {
            Index = (byte)Index,
            Message = Message
        };

        IExceptionReadOnlyModel ICopied<IExceptionReadOnlyModel>.Copy() => this.Clone();
    }

    public static class ProtoExceptionsExtensions
    {
        public static ProtoException ToProto(this IExceptionReadOnlyModel exception)
        {
            return new ProtoException
            {
                Index = exception.Index,
                Message = exception.Message
            };

        }
    }
}
