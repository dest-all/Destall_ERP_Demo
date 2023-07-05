using System.Collections.Generic;

namespace Protocol;

public interface IExecutionContext
{
    long OperationId { get; }
    string SessionKey { get; }
}