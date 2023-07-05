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
    public class DepositImplementation : Protocol.Grpc.ProtoServices.Deposit.DepositBase
    {
        public override async Task<DepositModelMessage> Save(DepositModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Deposit.Save;
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

        public override async Task<DepositModelListMessage> GetPage(Int32Int32IDepositFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Deposit.GetPage;
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
                var message = rawResult.Select(i => i as Protocol.Models.FinancialDocuments.DepositModel ?? new(i));
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

        public override async Task<DepositReferenceListMessage> GetReferences(Int32Int32IDepositFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Deposit.GetReferences;
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
                var message = rawResult.Select(i => i as Protocol.Models.FinancialDocuments.DepositReference ?? new(i));
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

        public override async Task<UInt32Message> Count(DepositFilterModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Deposit.Count;
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

        public override async Task<DepositModelMessage> Get(Int64Message request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Deposit.Get;
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
            var actionPoint = business.Actions.Deposit.Delete;
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

        public override async Task<DepositModelMessage> ChangeStatus(IDepositReadOnlyModelUInt16CommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Deposit.ChangeStatus;
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