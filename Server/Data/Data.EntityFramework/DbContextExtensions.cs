using Data.Entities;
using Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.EntityFramework;

public static class DbContextExtensions
{
    public static async Task EnsureSuperUserPresenceAsync(this ApplicationDbContext dbContext)
    {
        const string superUserName = "TechAdmin";
        var superUser = await dbContext.Users.Include(u => u.Permissions).FirstOrDefaultAsync(u => u.Id == 1);

        bool createNew = false;

        if (superUser == null)
        {
            superUser = new Entities.DataHolders.AccountingUsers.User();
            superUser.PasswordHash = "".Hash();
            createNew = true;
            superUser.Id = 1;
            superUser.PermissionsId = 1;
            dbContext.Add(superUser);
        }

        superUser.LoginName = superUserName;
        var superPermissions = superUser.Permissions ?? new Permissions();
        foreach (var p in superPermissions.ToDictionary())
        {
            p.Value.Setter(true);
        }
        superPermissions.TechnicalAdministrating = true;

        superUser.Permissions = superPermissions;

        superUser.Representation = superUserName;

        await dbContext.SaveChangesAsync();

        dbContext.Users.Update(superUser);

        ApplicationDbContext.SuperUserId = superUser.Id;
    }

    public static Expression<Func<ReferrableEntity, bool>> FitsPattern(this ApplicationDbContext dbContext, string pattern)
        => re => dbContext.OwnedStrings.Any(os => os.Id == re.Id && re.Representation == pattern);
}