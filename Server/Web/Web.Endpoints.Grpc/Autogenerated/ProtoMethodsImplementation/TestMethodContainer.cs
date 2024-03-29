// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Grpc.ProtoModels;
using Protocol.Grpc.ProtoModels.Conversion;
using Web.Endpoints.Grpc;
using Protocol.Exceptions;
using Business.Administration.Extensions;
using DestallMaterials.WheelProtection.Extensions.Collections;
using Google.Protobuf.WellKnownTypes;

namespace Business.OpenActions
{
    public class TestMethodContainerImplementation : Protocol.Grpc.ProtoServices.TestMethodContainer.TestMethodContainerBase
    {
        public override async Task<UInt32Message> Method(UInt32Message request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.OpenActions.TestMethodContainer.Method;
            var parameter = request.Message;
            try
            {
                var adjustedParameter = parameter;
                var rawResult = actionPoint.Call(adjustedParameter);
                var message = rawResult;
                var result = message.ToProtoMessage();
                return result;
            }
            catch (Exception ex)
            {
                var error = ex.ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }
        }

        public override async Task<BooleanMessage> HoldDbContext(Int32Message request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.OpenActions.TestMethodContainer.HoldDbContext;
            var parameter = request.Message;
            try
            {
                var adjustedParameter = parameter;
                var rawResult = (await actionPoint.CallAsync(adjustedParameter));
                var message = rawResult;
                var result = message.ToProtoMessage();
                return result;
            }
            catch (Exception ex)
            {
                var error = ex.ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }
        }
    }
}