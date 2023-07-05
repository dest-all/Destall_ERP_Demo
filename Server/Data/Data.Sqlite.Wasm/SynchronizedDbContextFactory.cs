using Data.EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace Data.Sqlite.Wasm
{
    public class SynchronizedDbContextFactory : IApplicationDbContextFactory
    {
        readonly Func<Task<WasmSqliteDbContext>> _dbContextFactory;
        readonly IJSRuntime _js;
        Task<int>? _lastTask = null;
        int _lastStatus = -2;
        bool _init = false;
        string _backupName = backup;

        public const string backup = $"{dbFilename}_bak";
        public const string dbFilename = "db.sqlite3";

        readonly Task _synchronizationScriptLaunch;

        public SynchronizedDbContextFactory(
                IJSRuntime js,
                Func<Task<WasmSqliteDbContext>> dbContextFactory
            )
        {
            _js = js;
            this._dbContextFactory = dbContextFactory;
            
            _synchronizationScriptLaunch = js.InvokeVoidAsync("dbSync", "embedded_db").AsTask();

            _lastTask = SynchronizeAsync();
            this._dbContextFactory = dbContextFactory;
        }

        public async Task<ApplicationDbContext> CreateAsync()
        {
            await CheckForPendingTasksAsync();
            var ctx = await _dbContextFactory();

            if (!_init)
            {
                Console.WriteLine($"Last status: {_lastStatus}");
                await ctx.Database.EnsureCreatedAsync();
                _init = true;
            }

            ctx.SavedChanges += Ctx_SavedChanges;

            return ctx;
        }

        private async Task CheckForPendingTasksAsync()
        {
            if (_lastTask != null)
            {
                _lastStatus = await _lastTask;
                _lastTask.Dispose();
                _lastTask = null;
                if (_lastStatus == 0)
                {
                    Restore();
                }
            }
        }

        private void Ctx_SavedChanges(object? sender, SavedChangesEventArgs e) =>
            _lastTask = SynchronizeAsync();

        private async Task<int> SynchronizeAsync()
        {
            if (_init)
            {
                Backup();
            }

            await _synchronizationScriptLaunch;

            var result = await _js.InvokeAsync<int>(
                "db.synchronizeDbWithCache", _backupName);
            var resultText = result == -1 ? "Failure" : result == 0 ? "Restored" : "Cached";
            Console.WriteLine($"Synchronization status: {resultText}");
            return result;
        }

        private void Backup() => DoSwap(false);
        private void Restore() => DoSwap(true);

        private void DoSwap(bool restore)
        {
            _backupName = restore ? backup : $"{backup}-{Guid.NewGuid().ToString().Split('-')[0]}";
            var dir = restore ? nameof(restore) : nameof(backup);
            Console.WriteLine($"Begin {dir}.");

            var source = restore ? $"Data Source={_backupName}" : $"Data Source={dbFilename}";
            var target = restore ? $"Data Source={dbFilename}" : $"Data Source={_backupName}";
            using var src = new SqliteConnection(source);
            using var tgt = new SqliteConnection(target);

            src.Open();
            tgt.Open();

            src.BackupDatabase(tgt);

            tgt.Close();
            src.Close();

            Console.WriteLine($"End {dir}.");
        }
    }
}
