using Client.Communication;
using Client.Communication.OpenActions;
using Client.Communication.Actions;
using Microsoft.Extensions.Logging;
using Protocol.Exceptions;
using Protocol.Models.DataHolders;
using Protocol.Models.Entities;
using Protocol.Models.Transportables;
using Client.Communication.Extensions;
using LocalStore;
using DestallMaterials.Blazor.Services;
using Protocol.Models;

namespace Client.Web.View.Services
{
    public class SessionInfo
    {
        public string SessionKey;
        public IPermissionsReadOnlyModel Permissions;
        public IUserReadOnlyReference UserReference;

        public bool IsValid => UserReference != null && SessionKey != default;
        public long UserId => UserReference?.Id ?? 0;
        public string LoginName => UserReference?.Representation;


        public SessionInfo(UserReference userReference, PermissionsModel permissions, string sessionKey)
        {
            Permissions = permissions;
            SessionKey = sessionKey;
            UserReference = userReference;
        }

        public SessionInfo()
        {
        }

        public SessionInfo(IUserReadOnlyModel user, string sessionKey)
        {
            SessionKey = sessionKey;
            Permissions = user.Permissions;
            UserReference = user.Reference;
        }

    }
    public class AccountManager : IAccountManager
    {
        readonly IAccountActionsActionPointAccessor _accountInteractor;
        readonly ISessionActionsActionPointAccessor _sessionInteractor;
        readonly IUserActionPointAccessor _userDataInteractor;
        readonly ILocalStore _localStore;
        readonly ILogger _logger;
        readonly CallConfigurator _clientConfigurator;

        const string _sessionInfoLocalStoreKey = "sessionInfo";

        long CurrentUserId => _sessionInfo?.UserId ?? 0;
        string SessionKey => _sessionInfo?.SessionKey;


        SessionInfo _sessionInfo = new();

        public AccountManager(
                IBusinessServerActionInvokersNet invokersNet,
                ILocalStore localStore,
                ILogger logger,
                CallConfigurator callConfigurator
            )
        {
            _accountInteractor = invokersNet.OpenActions.AccountActions;
            _sessionInteractor = invokersNet.OpenActions.SessionActions;
            _userDataInteractor = invokersNet.Actions.User;
            _localStore = localStore;
            _logger = logger;

            _clientConfigurator = callConfigurator;
        }

        readonly List<Action<IPermissionsReadOnlyModel>> _permissionChangeCallbacks = new List<Action<IPermissionsReadOnlyModel>>();

        public bool Authorised => CurrentUserId != 0;

        public IPermissionsReadOnlyModel Permissions => _sessionInfo?.Permissions ?? new PermissionsModel();

        public string UserName => _sessionInfo?.LoginName;

        public IUserReadOnlyModel User { get; private set; }

        public async Task<bool> AuthoriseStartedSessionAsync()
        {
            return false;
            var oldPermissions = Permissions;
            SessionInfo sessionFromStore = await ImportSessionFromStoreAsync();
            if (sessionFromStore == null)
            {
                return false;
            }

            try
            {
                _logger.LogInformation($"Logging in as {sessionFromStore.LoginName}.");

                IUserReadOnlyModel verification = await VerifySessionAsync(sessionFromStore.SessionKey, sessionFromStore.UserId);

                if (verification == null || verification?.Reference.IsEmpty() != false)
                {
                    return false;
                }
                _logger.LogInformation($"Successfully logged in as {sessionFromStore.LoginName}.");

                _sessionInfo = new(verification, sessionFromStore.SessionKey);

                FireAllCallbacks();

                return true;
            }
            catch (ServerSideException ex)
            {
                _logger.LogInformation($"Failed logging in as {sessionFromStore.LoginName}.");
                return false;
            }
        }

        async Task<IUserReadOnlyModel> VerifySessionAsync(string sessionKey, long userId)
            => await _sessionInteractor.VerifySession.CallAsync(new VerifySessionRequestModel
            {
                SessionKey = sessionKey,
                SessionUserId = userId
            });

        public async Task StartNewSessionAsync(string login, string password)
        {
            login = login ?? "";
            password = password ?? "";

            _logger.LogInformation($"Logging in as {login}.");
            var startSessionResponse = await _sessionInteractor.StartSession.CallAsync(login, password);
            var user = startSessionResponse.User;

            var sessionKey = startSessionResponse.SessionKey;

            var oldPermissions = Permissions;

            User = user;

            _logger.LogInformation($"Success logging in as {user.Reference.Id} {user.Reference.Representation}.");

            _sessionInfo = new(user, sessionKey);

            _clientConfigurator.AssignSessionKeyHeader(() => sessionKey);

            //await PutSessionDataToStoreAsync();
            if (Permissions?.ComputeChecksum() != oldPermissions?.ComputeChecksum())
            {
                FireAllCallbacks();
            }
        }

        public DisposableCallback SubscribeForPermissionsChange(Action<IPermissionsReadOnlyModel> callback)
        {
            _permissionChangeCallbacks.Add(callback);
            return new DisposableCallback(c => _permissionChangeCallbacks.Remove(callback));
        }

        async Task<SessionInfo> ImportSessionFromStoreAsync()
        {
            _logger.LogInformation($"Retrieving session info from store...");
            var sessionInfo = await _localStore.GetAsync<SessionInfo>(_sessionInfoLocalStoreKey);

            if (sessionInfo == null)
            {
                _logger.LogInformation($"No previous session found in store.");
                return null;
            }

            return sessionInfo;
        }

        async Task PutSessionDataToStoreAsync()
        {
            await _localStore.PutAsync(_sessionInfoLocalStoreKey, _sessionInfo);
        }

        void FireAllCallbacks()
        {
            foreach (var callback in _permissionChangeCallbacks.ToArray())
            {
                callback(Permissions);
            }
        }

        bool _checkPermissions;
        const int _waitBetweenChecksSeconds = 15;
        public void InitializeRepetitivePermissionsCheck()
        {
            _checkPermissions = true;
            Task.Run(async () =>
            {
                while (_checkPermissions)
                {
                    await Task.Delay(_waitBetweenChecksSeconds * 1000);
                    var user = await _userDataInteractor.Get.CallAsync(CurrentUserId);
                    if (user.Permissions?.ComputeChecksum()
                        != Permissions?.ComputeChecksum())
                    {
                        _sessionInfo = new(user, SessionKey);
                        FireAllCallbacks();
                    }
                }
            });
        }
        public void StopRepetitivePermissionsCheck()
        {
            _checkPermissions = false;
        }

        public void Dispose()
        {
            StopRepetitivePermissionsCheck();
        }

        public async Task EndCurrentSessionAsync()
        {
            if (SessionKey == default)
            {
                return;
            }
            var result = await _sessionInteractor.EndSession.CallAsync(SessionKey);
            if (result)
            {
                _sessionInfo = null;
                User = new UserModel
                {
                    LoginName = "Incognito"
                };

                FireAllCallbacks();
            }
        }

        public async Task<Exception> AuthoriseNewSessionAsync(string login, string password)
        {
            try
            {
                await StartNewSessionAsync(login, password);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task ChangePasswordAsync(string oldPassword, string newPassword) 
            => await _accountInteractor.ChangePassword.CallAsync(new ChangePasswordRequestModel
        {
            NewPassword = newPassword,
            OldPassword = oldPassword
        });
    }
}
