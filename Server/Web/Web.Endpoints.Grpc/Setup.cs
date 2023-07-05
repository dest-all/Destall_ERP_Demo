using Business.ActionPoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Endpoints.Grpc
{
    public static class Setup
    {
        const string _policyAllowAllName = "AllowAll";

        public static void ConfigureBuilderForGrpc(this WebApplicationBuilder builder, Func<IExecutionContext, IBusinessActionsNet> businessFactory, Action<Exception, IExecutionContext> handleException = null)
        {
            var services = builder.Services;
            services.AddGrpc();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            services.AddCors(o => o.AddPolicy(_policyAllowAllName, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                //.WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));

            services.AddHttpContextAccessor();
            services.AddLogging(builder => builder.AddConsole());

            ProtoService.ConfigureBusinessFactory(businessFactory, handleException);
        }

        public static void AddGrpcEndpoints(this WebApplication app)
        {
            app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                foreach (var endpoint in endpoints.MapAutos())
                {
                    endpoint.EnableGrpcWeb().RequireCors(_policyAllowAllName);
                }
            });
        }
    }
}
