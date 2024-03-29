// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Grpc.ProtoModels;
using Protocol.Grpc.ProtoModels.Conversion;
using Web.Endpoints.Grpc;
using Protocol.Exceptions;
using Business.Administration.Extensions;
using DestallMaterials.WheelProtection.Extensions.Collections;
using Google.Protobuf.WellKnownTypes;

namespace Business.Actions
{
    public class IncomingOrderImplementation : Protocol.Grpc.ProtoServices.IncomingOrder.IncomingOrderBase
    {
        public override async Task<IncomingOrderModelMessage> Save(IncomingOrderModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingOrder.Save;
            var actionAccessibility = await business.CalculateActionAccessibilityAsync(actionPoint);
            if (!actionAccessibility.MayAccess)
            {
                var error = actionAccessibility.ToException().ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }

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

        public override async Task<IncomingOrderModelListMessage> GetPage(Int32Int32IIncomingOrderFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingOrder.GetPage;
            var actionAccessibility = await business.CalculateActionAccessibilityAsync(actionPoint);
            if (!actionAccessibility.MayAccess)
            {
                var error = actionAccessibility.ToException().ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }

            var parameter = request.Message;
            try
            {
                var adjustedParameter = parameter;
                var rawResult = (await actionPoint.CallAsync(adjustedParameter));
                var message = rawResult.Select(i => i as Protocol.Models.Documents.IncomingOrderModel ?? new(i));
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

        public override async Task<IncomingOrderReferenceListMessage> GetReferences(Int32Int32IIncomingOrderFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingOrder.GetReferences;
            var actionAccessibility = await business.CalculateActionAccessibilityAsync(actionPoint);
            if (!actionAccessibility.MayAccess)
            {
                var error = actionAccessibility.ToException().ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }

            var parameter = request.Message;
            try
            {
                var adjustedParameter = parameter;
                var rawResult = (await actionPoint.CallAsync(adjustedParameter));
                var message = rawResult.Select(i => i as Protocol.Models.Documents.IncomingOrderReference ?? new(i));
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

        public override async Task<UInt32Message> Count(IncomingOrderFilterModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingOrder.Count;
            var actionAccessibility = await business.CalculateActionAccessibilityAsync(actionPoint);
            if (!actionAccessibility.MayAccess)
            {
                var error = actionAccessibility.ToException().ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }

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

        public override async Task<IncomingOrderModelMessage> Get(Int64Message request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingOrder.Get;
            var actionAccessibility = await business.CalculateActionAccessibilityAsync(actionPoint);
            if (!actionAccessibility.MayAccess)
            {
                var error = actionAccessibility.ToException().ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }

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

        public override async Task<BooleanMessage> Delete(Int64ListMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingOrder.Delete;
            var actionAccessibility = await business.CalculateActionAccessibilityAsync(actionPoint);
            if (!actionAccessibility.MayAccess)
            {
                var error = actionAccessibility.ToException().ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }

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

        public override async Task<IncomingOrderModelMessage> ChangeStatus(IIncomingOrderReadOnlyModelUInt16CommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingOrder.ChangeStatus;
            var actionAccessibility = await business.CalculateActionAccessibilityAsync(actionPoint);
            if (!actionAccessibility.MayAccess)
            {
                var error = actionAccessibility.ToException().ToExceptionModel().ToProto();
                return new()
                {Addin = new()
                {Error = error}};
            }

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