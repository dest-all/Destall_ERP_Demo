using Business.Tests.Setup;
using Protocol.Models.Transportables;

namespace Business.Tests;


public class SessionTests : ActionBase
{
    [Test]
    public async Task Test1()
    {
        var login = "Carton Cole";
        var password = "";

        var result = await Business.OpenActions.SessionActions.StartSession.CallAsync(login, password);
    }

    [Test]
    public async Task ChangePassword()
    {
        const string login = "TechAdmin";

        const string oldPassword = "";
        const string newPassword = "1";

        var techAdminSession = await Business.OpenActions.SessionActions.StartSession.CallAsync(login, oldPassword);

        var changePasswordResult = await Business.OpenActions.AccountActions.ChangePassword.CallAsync(new ChangePasswordRequestModel
        {
            NewPassword = newPassword,
            OldPassword = oldPassword
        });


    }
}