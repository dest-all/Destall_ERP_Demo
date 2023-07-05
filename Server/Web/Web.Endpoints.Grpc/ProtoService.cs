using Business.ActionPoints;
using Protocol;

namespace Web.Endpoints.Grpc
{
    public abstract class ProtoService
    {
        protected static Action<Exception, IExecutionContext> LogException { get; private set; } = (ex, context) => Console.WriteLine($"Error {ex.Message} in context {context.SessionKey}.");
        protected static Func<IExecutionContext, IBusinessActionsNet> BusinessFactory { get; private set; }
        public static void ConfigureBusinessFactory(Func<IExecutionContext, IBusinessActionsNet> factory, Action<Exception, IExecutionContext> logException = null)
        {
            BusinessFactory = factory;
            LogException = logException;
        }
    }
}
