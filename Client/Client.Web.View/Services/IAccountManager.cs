using DestallMaterials.Blazor.Services;
using Protocol.Models.DataHolders;
using Protocol.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public interface IAccountManager : IDisposable
    {
        bool Authorised { get; }
        IPermissionsReadOnlyModel Permissions { get; }
        string UserName { get; }
        Task<bool> AuthoriseStartedSessionAsync();
        Task<Exception> AuthoriseNewSessionAsync(string login, string password); 

        DisposableCallback SubscribeForPermissionsChange(Action<IPermissionsReadOnlyModel> callback);

        Task EndCurrentSessionAsync();

        /// <summary>
        /// Changes password for login. Returns error message, if doesn't succeed.
        /// </summary>
        /// <returns></returns>
        Task ChangePasswordAsync(string oldPassword, string newPassword);

        IUserReadOnlyModel User { get; }
    }
}
