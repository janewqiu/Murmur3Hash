using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
 
namespace MurmurHashPerformance
{
    public class HashServices
    {

        static HashAlgorithm ShahashClass = new SHA1CryptoServiceProvider();

        static HashAlgorithm md5hash = new MD5CryptoServiceProvider();

        public static string SHA1Hash(byte[] data)
        {
            string hashedValue = string.Empty;
            try
            {
                byte[] hashedData = ShahashClass.ComputeHash(data);
                return Convert.ToBase64String(hashedData);
            }
            catch
            {
            }
            return hashedValue;
        }

        public static string MD5HashCode(byte[] data)
        {
            string hashedValue = string.Empty;
            try
            {
                byte[] hashedData = md5hash.ComputeHash(data);
                return Convert.ToBase64String(hashedData);
            }
            catch
            {
            }
            return hashedValue;
        }


        public static string Murmur3_64Bit(byte[] data)
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


        public static string Murmur3_Ariso_64Bit(byte[] data)
        {
            string hashedValue = string.Empty;
            try
            {
                Murmur3_Ariso hashClass = new Murmur3_Ariso();
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

        //static MACTripleDES DeshashClass = new MACTripleDES();
        //public static string DES32Hash_bak(byte[] data)
        //{
                                               
        //    return Convert.ToBase64String(DeshashClass.ComputeHash(data));

        //}


        //static HMACMD5 md54 = new HMACMD5();
        //public static string DES32Hash(byte[] data)
        //{

        //    return Convert.ToBase64String(md54.ComputeHash(data));

        //}

        static MD4Context md4 = new MD4Context();
        public static string MD4HashTest(byte[] data)
        {
              md4.Reset();
           md4.Update(data,0,data.Length);
     //       byte[] intBytes = BitConverter.GetBytes(the value);
            return Convert.ToBase64String(md4.GetDigest().ToArray());

        }


        public static string FNV1A64(byte[] data)
        {                                 
            //       byte[] intBytes = BitConverter.GetBytes(the value);
          return Convert.ToBase64String(GetBytesUInt64(FNVHash.HashFNV1a(data)));

        }

        public static string FNV1A32(byte[] data)
        {
            //       byte[] intBytes = BitConverter.GetBytes(the value);
         //   return FNVHash.Hash32FNV1bx(data).ToString();
            return Convert.ToBase64String(GetBytesUInt32(FNVHash.Hash32FNV1bx(data)));
        //    return  (char) data[0] + Convert.ToBase64String(GetBytesUInt32(FNVHash.Hash32FNV1bx(data)));
        }

        public static string FNV1A64x(byte[] data)
        {
            //       byte[] intBytes = BitConverter.GetBytes(the value);
            return Convert.ToBase64String(GetBytesUInt64(FNVHash.Hash64FNV1ax(data)));
        }
        // Convert a ulong argument to a byte array and display it. 
        public static byte[] GetBytesUInt64(ulong argument)
        {
            byte[] byteArray = BitConverter.GetBytes(argument);
            return byteArray;
        }

        // Convert a ulong argument to a byte array and display it. 
        public static byte[] GetBytesUInt32(uint argument)
        {
            byte[] byteArray = BitConverter.GetBytes(argument);
            return byteArray;
        }


        public static string Murmur2_32bit(byte[] data)
        {
            ulong datxa = Murmur2.Hash_02(data, (ulong)data.Length, 0L);

            byte[] intBytes = BitConverter.GetBytes(datxa);
            return Convert.ToBase64String(intBytes);

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
