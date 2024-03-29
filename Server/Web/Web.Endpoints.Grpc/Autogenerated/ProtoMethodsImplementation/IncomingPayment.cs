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
    public class IncomingPaymentImplementation : Protocol.Grpc.ProtoServices.IncomingPayment.IncomingPaymentBase
    {
        public override async Task<IncomingPaymentModelMessage> Save(IncomingPaymentModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingPayment.Save;
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

        public override async Task<IncomingPaymentModelListMessage> GetPage(Int32Int32IIncomingPaymentFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingPayment.GetPage;
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
                var message = rawResult.Select(i => i as Protocol.Models.FinancialDocuments.IncomingPaymentModel ?? new(i));
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

        public override async Task<IncomingPaymentReferenceListMessage> GetReferences(Int32Int32IIncomingPaymentFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingPayment.GetReferences;
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
                var message = rawResult.Select(i => i as Protocol.Models.FinancialDocuments.IncomingPaymentReference ?? new(i));
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

        public override async Task<UInt32Message> Count(IncomingPaymentFilterModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingPayment.Count;
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

        public override async Task<IncomingPaymentModelMessage> Get(Int64Message request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingPayment.Get;
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
            var actionPoint = business.Actions.IncomingPayment.Delete;
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

        public override async Task<IncomingPaymentModelMessage> ChangeStatus(IIncomingPaymentReadOnlyModelUInt16CommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.IncomingPayment.ChangeStatus;
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