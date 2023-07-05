using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class Encryption
    {
        static SHA256 HashComputer { get; } = SHA256.Create();
        public static string Hash(this string str)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            byte[] hash = HashComputer.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
