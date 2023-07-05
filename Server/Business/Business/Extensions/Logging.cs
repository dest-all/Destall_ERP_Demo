using Microsoft.Extensions.Logging;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions.Logging
{
    public static class LoggingExtensions
    {
        public static void LogException(this ILogger logger, Exception ex, IExecutionContext? executionContext = null)
        {
            logger.LogError($"{ex.Message}\n{ex.InnerException?.Message}", ex);
        }
    }
}
