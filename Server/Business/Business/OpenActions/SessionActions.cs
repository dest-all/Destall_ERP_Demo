using System.Linq;
using Protocol.Exceptions;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using Data.Extensions;
using Business.ModelsComposition;
using Business.Actions;
using Data.Entities;
using System.Threading.Tasks;
using Protocol.Models.Transportables;
using Protocol.Models.Entities;
using Protocol.Models.DataHolders;
using Microsoft.EntityFrameworkCore;
using Business.Selectors;
using Microsoft.Extensions.Logging;
using Business.ActionPoints.OpenActions;
using Business.ActionPoints.Administration;
using System.Linq.Expressions;
using System;
using Data.Entities.DataHolders.AccountingUsers;

namespace Business.OpenActions
{
    [ProcessServerRequest]
    public partial class SessionActions : ActionContainer
    {
        ISessionsManagementActionPointAccessor Session => _business.Administration.SessionsManagement;

        [AllowUnauthorised]
        public async Task<IStartSessionResponseReadOnlyModel> StartSession(string login, string password)
        {
            string passwordHash = (password ?? "").Hash();

            using var repo = await GetRepositoryAsync();

            var user = repo.Users
                .Where(CheckUser(login, passwordHash))
                .Include(u => u.Permissions)
                .Select(UserSelectors.ModelSelector(repo))
                .FirstOrDefault() ?? throw new InvalidCredentialsHandledException();

            
            var session = Session.EnsureCreatedSessionForUser(user.Reference.Id);

            return new StartSessionResponseModel
            {
                SessionKey = session.Key,
                User = user
            };
        }

        static Expression<Func<User, bool>> CheckUser(string login, string passwordHash)
            => u => u.LoginName.ToLower() == login.ToLower() && (passwordHash == u.PasswordHash || string.IsNullOrEmpty(u.PasswordHash));
        

        public bool EndSession(string sessionKey)
        {

            return true;
        }

        [AllowUnauthorised]
        public async Task<IUserReadOnlyModel> VerifySession(IVerifySessionRequestReadOnlyModel verifySession)
        {
            var sessionKey = verifySession.SessionKey;
            var userId = verifySession.SessionUserId;
            var session = Session.GetOpenSessionByKey(sessionKey);
            if (session == null || session.UserId != userId)
            {
                Logger.LogInformation($"Attempt to verify session {verifySession.SessionKey} failed for user {verifySession.SessionUserId}.");
                return null;
            }

            var user = await _business.Actions.User.Get.CallAsync(userId);

            Logger.LogInformation($"Session {verifySession.SessionKey} verified for user {user.Reference.Id} {user.LoginName}.");

            return user;
        }

        static SessionActions()
        {
            IPermissionsReadOnlyModel permissionsModel = new PermissionsModel();
            var entity = permissionsModel.ToEntity();
            foreach (var prop in entity.ToDictionary())
            {
                prop.Value.Setter(true);
            }
            permissionsModel = entity.ComposeModel();
            FullPermissionsModel = permissionsModel;
        }

        readonly static IPermissionsReadOnlyModel FullPermissionsModel;
        public async Task<IPermissionsReadOnlyModel> GetCurrentSessionPermissions()
        {
            var sessionKey = CurrentSession.Key;
            if (sessionKey == default)
            {
                return FullPermissionsModel;
            }
            var session = Session.GetOpenSessionByKey(sessionKey);
            var userId = session.UserId;
            
            if (session == null || session.UserId != userId)
            {
                throw new InvalidSessionKeyHandledException("Session key or user don't match any open session.");
            }

            var dbContext = await GetRepositoryAsync();

            var permissions = await dbContext.Permissions.SingleOrDefaultAsync(p => p.Id == userId);

            return permissions.ComposeModel();
        }
    }
}