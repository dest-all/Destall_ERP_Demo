using Business.ActionPoints;
using Business.Administration;
using Protocol.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static class SessionExtensions
    {
        public static Session? GetCurrentSession(this IBusinessActionsNet business)
            => business.Administration.SessionsManagement.GetOpenSessionByKey.Call(business.ExecutionContext.SessionKey);

        public static async Task<IPermissionsReadOnlyModel> GetCurrentUserPermissionsAsync(this IBusinessActionsNet business, Session? currentSession = null)
        {
            var userId = currentSession?.UserId ?? business.GetCurrentSession().UserId;
            var user = await business.Actions.User.Get.CallAsync(userId);
            return user.Permissions;
        }
    }
}
