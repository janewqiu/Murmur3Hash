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


        public static string CRC32Hash(byte[] data)
        {

            Crc32 crc32 = new Crc32();
            return Convert.ToBase64String(crc32.ComputeHash(data));

        }
        //public static string CRC32AHash(byte[] data)
        //{

        //    Crc32A crc32 = new Crc32A();
        //    return Convert.ToBase64String(crc32.ComputeHash(data));

        //}

        public static string ConverToBase64(byte[] data)
        {
            return Convert.ToBase64String(data,0,1000);

        }
    }
}
