using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Exceptions;
using Protocol.Grpc.ProtoModels;

namespace Protocol.Grpc.Exceptions
{
    public static class ConvertExceptions
    {
        public static ProtoException ToProto(this IExceptionReadOnlyModel exceptionModel)
        {
            if (exceptionModel is ProtoException result)
            {
                return result;
            }

            return new ProtoException
            {
                Index = exceptionModel.Index,
                Message = exceptionModel.Message
            };
        }
        public static System.Exception ToException(this ProtoException protoException)
        {
            return HandledExceptionsHandler.CreateExceptionFromIndex((byte)protoException.Index, protoException.Message);
        }

    }
}
