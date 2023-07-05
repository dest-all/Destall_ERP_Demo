using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalStore.LiteDb
{
    static class Extensions
    {
        public static bool TryDelete(this ILiteDatabase db)
        {
            var files = db.FileStorage.FindAll().Select(f => f.Filename);
            db.Dispose();

            try
            {
                foreach (var file in files)
                {
                    File.Delete(file);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
