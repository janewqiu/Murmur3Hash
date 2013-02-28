using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace MurmurHashPerformance
{
    public class HashServices
    {
        public static string SHA1Hash(byte[] data)
        {
            string hashedValue = string.Empty;
            try
            {
                HashAlgorithm hashClass = new SHA1CryptoServiceProvider();
                byte[] hashedData = hashClass.ComputeHash(data);
                return Convert.ToBase64String(hashedData);
            }
            catch
            {
            }
            return hashedValue;
        }


        public static string Murmurhash(byte[] data)
        {
            string hashedValue = string.Empty;
            try
            {
                Murmur3 hashClass = new Murmur3();
                byte[] hashedData = hashClass.ComputeHash(data);
                return Convert.ToBase64String(hashedData);
            }
            catch
            {
            }
            return hashedValue;
        }
    }
}
