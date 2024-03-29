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
    public class CustomerImplementation : Protocol.Grpc.ProtoServices.Customer.CustomerBase
    {
        public override async Task<CustomerModelMessage> Save(CustomerModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Customer.Save;
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

        public override async Task<CustomerModelListMessage> GetPage(Int32Int32ICustomerFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Customer.GetPage;
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
                var message = rawResult.Select(i => i as Protocol.Models.Counterparties.CustomerModel ?? new(i));
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

        public override async Task<CustomerReferenceListMessage> GetReferences(Int32Int32ICustomerFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Customer.GetReferences;
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
                var message = rawResult.Select(i => i as Protocol.Models.Counterparties.CustomerReference ?? new(i));
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

        public override async Task<UInt32Message> Count(CustomerFilterModelMessage request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Customer.Count;
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

        public override async Task<CustomerModelMessage> Get(Int64Message request, Grpc.Core.ServerCallContext grpcContext)
        {
            var executionContext = grpcContext.ToExecutionContext();
            using var business = BusinessFactory(executionContext);
            var actionPoint = business.Actions.Customer.Get;
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
            var actionPoint = business.Actions.Customer.Delete;
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