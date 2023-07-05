using DestallMaterials.WheelProtection.DataWorks;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Endpoints.Grpc
{
    public static class CallContextUnifier
    {
        static IdsGenerator IdsGenerator { get; } = new();

        class ExecutionContext : IExecutionContext
        {
            public required long OperationId { get; init; }

            public required string SessionKey { get; init; }
            
        }
        public static IExecutionContext ToExecutionContext(this ServerCallContext? context)
        {
            var operationId = IdsGenerator.Generate();
            if (context is null)
            {
                return new ExecutionContext()
                {
                    OperationId = operationId,
                    SessionKey = ""
                };
            }
            string sessionKey = context.RequestHeaders.FirstOrDefault(h => h.Key == "sessionkey")?.Value ?? "";
            return new ExecutionContext
            {
                SessionKey = sessionKey,
                OperationId = operationId
            };
        }
    }
}
