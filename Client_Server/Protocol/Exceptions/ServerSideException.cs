using DestallMaterials.WheelProtection.Executions;
using System;

namespace Protocol.Exceptions;


public class ServerSideUnhandledException : System.Exception
{
    public ServerSideUnhandledException(string message) : base(message)
    {
    }

    public ServerSideUnhandledException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class ServerSideException : AggregateException, IExplained
{
    public bool IsHandled => InnerException is Protocol.Exceptions.HandledException;

    public string Explanation 
    {   
        get 
        {
            if (InnerException is Protocol.Exceptions.HandledException he)
            {
                return he.Message;
            }
            return Message;
        } 
    }

    public override string ToString()
    {
        string handledString = IsHandled ? "handled" : "unhandled";
        return $"Server side {handledString} exception: {Message}.";
    }

    public ServerSideException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ServerSideException(Exception innerException, string message) : base(message, innerException)
    {
    }
}
