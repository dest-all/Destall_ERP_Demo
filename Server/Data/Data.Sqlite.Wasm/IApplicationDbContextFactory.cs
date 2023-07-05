using Data.EntityFramework;

namespace Data.Sqlite.Wasm
{
    public interface IApplicationDbContextFactory
    {
        Task<ApplicationDbContext> CreateAsync();
    }
}
