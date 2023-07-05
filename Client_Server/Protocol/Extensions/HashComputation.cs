using DestallMaterials.WheelProtection.Copying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Extensions
{
    public static class HashComputation
    {
        public static int ComputeChecksum(this string str)
        {
            unchecked
            {
                if (str == null)
                {
                    return 404;
                }
                int result = 400 + str.Length * 112;
                for (int i = 0; i < str.Length; i++)
                {
                    result += str[i].ComputeChecksum();
                }
                return result;
            }
        }

        public static int ComputeChecksum(this char chr)
        {
            unchecked
            {
                var numericValue = (int)chr;
                var result = numericValue * 2 + 14;
                return result;
            }
        }

        public static int ComputeChecksum(this int number)
        {
            unchecked
            {
                return number + 15;
            }
        }

        public static int ComputeChecksum(this byte number)
        {
            unchecked
            {
                return number + 25;
            }
        }

        public static int ComputeChecksum(this ushort number)
        {
            unchecked
            {
                return number + 1308;
            }
        }


        public static int ComputeChecksum(this long number)
        {
            unchecked
            {
                return (int)(number + 1024);
            }
        }

        public static int ComputeChecksum(this ulong number)
        {
            unchecked
            {
                return (int)(number + 2048);
            }
        }
        public static int ComputeChecksum(this uint number)
        {
            unchecked
            {
                return (int)(number + 488);
            }
        }

        public static int ComputeChecksum(this float number)
        {
            unchecked
            {
                return (int)((number + 25) * (number * 2));
            }
        }

        public static int ComputeChecksum(this double number)
        {
            unchecked
            {
                return (int)((number + 55) * (number * 3));
            }
        }
        public static int ComputeChecksum(this decimal number)
        {
            unchecked
            {
                return (int)((number + 34) * (number * 3));
            }
        }

        public static int ComputeChecksum(this DateTime dateTime)
        {
            unchecked
            {
                return dateTime.Ticks.ComputeChecksum();
            }
        }
        public static int ComputeChecksum(this IEnumerable<string> str)
        {
            if (str == null)
            {
                return 101;
            }
            unchecked
            {
                return str.Select(c => c.ComputeChecksum() * 7).SumUnchecked();
            }

        }

        public static int ComputeChecksum(this IEnumerable<char> chrs)
        {
            if (chrs == null)
            {
                return 109;
            }
            unchecked
            {
                return chrs.Select(c => c.ComputeChecksum() * 3).SumUnchecked();
            }
        }

        public static int ComputeChecksum(this IEnumerable<int> numbers)
        {
            if (numbers == null)
            {
                return 120;
            }
            unchecked
            {
                return numbers.Select(c => c.ComputeChecksum() * 15).SumUnchecked();
            }
        }

        public static int ComputeChecksum(this IEnumerable<byte> numbers)
        {
            if (numbers == null)
            {
                return 96;
            }
            unchecked
            {
                return numbers.Select(c => c.ComputeChecksum() * 30).SumUnchecked(); ;
            }
        }

        public static int ComputeChecksum(this IEnumerable<ushort> numbers)
        {
            if (numbers == null)
            {
                return 3452;
            }
            unchecked
            {
                return numbers.Select(c => c.ComputeChecksum() * 1198).SumUnchecked(); ;
            }
        }

        public static int ComputeChecksum(this IEnumerable<long> number)
        {
            if (number == null)
            {
                return 94;
            }
            unchecked
            {
                return number.Select(c => c.ComputeChecksum() * 13).SumUnchecked();
            }
        }

        public static int ComputeChecksum(this IEnumerable<ulong> number)
        {
            if (number == null)
            {
                return 91;
            }
            unchecked
            {
                return number.Select(c => c.ComputeChecksum() * 54).SumUnchecked(); ;
            }
        }
        public static int ComputeChecksum(this IEnumerable<uint> number)
        {
            if (number == null)
            {
                return 87;
            }
            unchecked
            {
                return number.Select(c => c.ComputeChecksum() * 12).SumUnchecked(); ;
            }
        }

        public static int ComputeChecksum(this IEnumerable<float> number)
        {
            if (number == null)
            {
                return 33;
            }
            unchecked
            {
                return number.Select(c => c.ComputeChecksum() * 33).SumUnchecked();
            }
        }

        public static int ComputeChecksum(this IEnumerable<double> number)
        {
            if (number == null)
            {
                return 237;
            }
            unchecked
            {
                return number.Select(c => c.ComputeChecksum() * 3).SumUnchecked();
            }
        }

        public static int ComputeChecksum(this IEnumerable<decimal> numbers)
        {
            if (numbers == null)
            {
                return 237;
            }
            unchecked
            {
                return numbers.Select(c => c.ComputeChecksum() * 2).SumUnchecked();
            }
        }


        public static int ComputeChecksum(this IEnumerable<DateTime> dateTime)
        {
            if (dateTime == null)
            {
                return 111;
            }
            unchecked
            {
                return dateTime.Select(c => c.ComputeChecksum() * 11).SumUnchecked();
            }
        }

        public static int ComputeChecksum(this bool value)
        {
            unchecked
            {
                return value ? 2018 : 1302;
            }
        }

        public static int ComputeChecksum(this IEnumerable<bool> value)
        {
            if (value == null)
            {
                return 918;
            }
            unchecked
            {
                return value.Select(s => s.ComputeChecksum()).SumUnchecked();
            }
        }
    }

    public static class DataCopying
    {
        public static T CopyData<T>(this T input) where T : struct => input;

        public static IEnumerable<T> CopyData<T>(this IEnumerable<T> input) where T : struct => input;

        public static string CopyData(this string input) => input;


        public static IEnumerable<string> CopyData(this IEnumerable<string> input) => input;

        public static T CopyData<T>(this ICopied<T> copied)
            where T : ICopied<T> => copied.Copy();
    }
}
