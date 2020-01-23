using Sodium;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hashspeed
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = "Hello World!";
            CalcMd5(source, 20);
            CalcArgon2(source, 20);
        }

        private static void CalcArgon2(string source, int total)
        {
            var sw = Stopwatch.StartNew();
            var hash = PasswordHash.ArgonHashString(source, PasswordHash.StrengthArgon.Sensitive);
            Console.WriteLine($"The Argon2 hash of {source} is: {hash}.");
            Parallel.For(0, total, (i) => { PasswordHash.ArgonHashString(source, PasswordHash.StrengthArgon.Sensitive); });
            Console.WriteLine($"{total} Interaction: {sw.ElapsedMilliseconds}");
            sw.Stop();
        }

        private static void CalcMd5(string source, int total)
        {
            var sw = Stopwatch.StartNew();
            using (var md5Hash = MD5.Create())
            {
                var hash = GetMd5Hash(md5Hash, source);
                Console.WriteLine($"The MD5 hash of {source} is: {hash}.");
                Parallel.For(0, total, (i) => { GetMd5Hash(md5Hash, source); });
            }

            Console.WriteLine($"{total} Interaction: {sw.ElapsedMilliseconds}");
            sw.Stop();
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
