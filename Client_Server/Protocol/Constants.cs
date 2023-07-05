using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol;

public static class Constants
{
    public static class Headers
    {
        public const string SessionKey = "sessionkey";
        public const string ApplyCompression = "apply-compression";

        public static class MemoryPack
        {
            public const string Key = "body-format";
            public const string Affirmative = "MP";
            public const string Negative = "json";
        }
    }

    public static class Languages
    {
        public const string English = "En";
        public const string Russian = "Ru";
        public const string German = "De";
    }

}
