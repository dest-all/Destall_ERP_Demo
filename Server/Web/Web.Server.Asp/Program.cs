using Web.Endpoints.Api;
using Web.Endpoints.Grpc;
using Web.Server.Asp;
using Web.Server.Primitive;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddCors();

var serviceProvider = builder.Services.BuildServiceProvider();
var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

var logger = loggerFactory.CreateLogger<Program>();

ApiRequestProcessor processor = ApiServerSetup.CreateRequestProcessor((ex, context) => logger.LogError(ex.Message), out var businessFactory);

builder.ConfigureBuilderForGrpc(businessFactory.Create);

var app = builder.Build();

app.UseRouting();

app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.AddGrpcEndpoints();

var requestHandler = processor.ToPostRequestHandler();

const string prefix = ApiServerSetup.Prefix;

app.MapPost(prefix + "{controller}/{action}", requestHandler);

app.UseHttpLogging();

app.Run();