using System;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using Data.Extensions;
using System.Linq;
using Protocol.Exceptions;
using Business.Actions;
using System.Threading.Tasks;
using Protocol.Models.Transportables;
using Data.Repository;

namespace Business.OpenActions
{
    [ProcessServerRequest]
    public partial class AccountActions : ActionContainer
    {
        public async Task<bool> ChangePassword(IChangePasswordRequestReadOnlyModel passwords)
        {
            var oldPassword = passwords.OldPassword ?? "";
            var newPassword = passwords.NewPassword ?? "";

            using var repo = await GetRepositoryAsync();
            var oldHash = oldPassword.Hash();
            var currentUser = CurrentSession.UserId;
            var user = repo.Users.FirstOrDefault(u => u.Id == currentUser && (oldHash == u.PasswordHash || string.IsNullOrEmpty(u.PasswordHash)));

            if (user != null)
            {
                user.PasswordHash = newPassword.Hash();
                await repo.UpdateAsync(user);
                return true;
            }
            else 
            {
                throw new InvalidCredentialsHandledException($"Password specified as current doesn't fit the current user.");
            }
        }
    }
}