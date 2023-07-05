using Business.ActionPoints;
using DestallMaterials.WheelProtection.Extensions.Strings;
using Protocol.Exceptions;
using Protocol.Models.Entities;

namespace Business.Administration.Extensions
{
    public static partial class CheckPermissionsExtensions
    {
        public static HandledException? ToException(this ActionAccessibility actionAccessibility)
        {
            if (actionAccessibility.MayAccess)
            {
                return null;
            }
            if (!actionAccessibility.MissingPermissions.Any())
            {
                return new UnauthorisedHandledException("Action is not allowed without authorisation.");
            }
            return new PermissionLackException($"Session user does not have the necessary permissions to execute action: {actionAccessibility.MissingPermissions.Join(", ")}");
        }
    }
}
