using Microsoft.Extensions.Logging;

namespace Business.Tests.Setup
{
    class TestLogger : ILogger
    {
        class Disposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
        public IDisposable BeginScope<TState>(TState state) where TState : notnull => new Disposable();

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) => Console.WriteLine(formatter(state, exception));
    }
}