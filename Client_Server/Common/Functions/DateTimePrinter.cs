using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Functions
{
    public static class DateTimePrinter
    {
        public static string OnlyDate(DateTime dateTime) => $"{dateTime:d}";
        public static string Full(DateTime dateTime) => $"{dateTime:d-HH-mm}";
    }
}
