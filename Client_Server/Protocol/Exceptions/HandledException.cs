using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Exceptions
{
    [HandledException]
    public class HandledException : Exception
    {
        public virtual ushort HttpStatusCode => 400;
        public HandledException(string message) : base(message)
        {
            
        }
        public HandledException() : base()
        { 
        }
    }

    public class InvalidArgumentHandledException : HandledException
    {
        public InvalidArgumentHandledException(string message) : base(message)
        {
        }
    }

    public class InvalidSessionKeyHandledException : UnauthorisedHandledException
    {   
        public InvalidSessionKeyHandledException(string message) : base(message)
        {
        }
    }

    public class InvalidCredentialsHandledException : HandledException
    {   
        public InvalidCredentialsHandledException() : base("Invalid credentials supplied.")
        {
        }

        public InvalidCredentialsHandledException(string message) : base(message)
        {
        }
    }

    public class UnauthorisedHandledException : HandledException
    {
        public override ushort HttpStatusCode => 401;
        
        public UnauthorisedHandledException(string message = "Authorisation is required to execute action.") : base(message)
        {
        }
    }

    public class InsufficientResourceException : HandledException
    {
        public InsufficientResourceException(string message) : base(message)
        { 
        }
    }

    public class PermissionLackException : HandledException
    {
        public override ushort HttpStatusCode => 403;
        public PermissionLackException(string actionName = "User doesn't have permission to execute action.") : base(actionName)
        {
        }
    }

    public class InvalidStateException : HandledException
    {
        public InvalidStateException(string message) : base(message)
        {
        }
    }
}