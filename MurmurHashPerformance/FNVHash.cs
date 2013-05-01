using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MurmurHashPerformance
{


    public class FNVHash
    {

        public static ulong fnv64Offset = 0xcbf29ce484222325u;//14695981039346656037;
        public static ulong fnv64Prime = 0x100000001b3;

        // FNV-1a (64-bit) non-cryptographic hash function.
        // Adapted from: http://github.com/jakedouglas/fnv-java
        public static ulong HashFNV1a(byte[] bytes)
        {

            ulong hash = fnv64Offset;
            //unchecked
            //{
                for (var i = 0; i < bytes.Length; i++)
                {
                    hash = hash ^ bytes[i];
                    hash *= fnv64Prime;
                }
            //}
            return hash;
        }



        public static uint fnv32Prime = 16777619;  //139969
        const uint fnv32Offset = 2147483647;// 2147483647;

        // FNV-1a (32-bit) non-cryptographic hash function.    --ariso
        // Adapted from: http://github.com/jakedouglas/fnv-java
        public static uint Hash32FNV1a(byte[] bytes)
        {
            // Prime :   139969 , Offset    2147483647 : Conflit: 7
            // Prime :   139907 , Offset    2147483647 : Conflit: 5      
            // Prime :   16777619 , Offset    2147483647 : Conflit: 4   
            uint hash = fnv32Offset;

            for (var i = 0; i < bytes.Length; i++)
            {
                hash = hash ^ bytes[i];
                hash *= fnv32Prime;
            }

            return hash;
        }


          

        // FNV-1a (32-bit) non-cryptographic hash function.    --ariso
        // Adapted from: http://github.com/jakedouglas/fnv-java
        public static uint Hash32FNV1bx(byte[] bytes)
        {
            uint fnv32Prime = 139969;  //139969
            uint fnv32Prime1 = 16777619;  //139969
            uint fnv32Offset = 2166136261;// 2147483647;
            uint[] fnv32PrimeArray = new uint[] { 
            
16974499,
16974511,
16974523,
16974539,
16974547,
16974553,
16974563,
16974569,
16974589,
16974611,
16974631,
16974647,
16974649,
16974677,
16974689,
16974719,
16974761,
16974781,
16974799,
16974817,
16974827,
16974829,
16974863,
16974877,
16974883,
16974901,
16974907,
16974913,
16974917,
16974931,
16974953,
16975001,
16975003,
16975009,
16975039,
16975043,
16975069,
16975087,
16975093,
16975099,
16975109,
16975117,
16975139,
16975157,
16975183,
16975207,
16975213,
16975223,
16975253,
16975261,
16975307,
16975331,
16975337,
16975349,
16975363,
16975369,
16975393,
16975397,
16975457,
16975459,
16975471,
16975477,
16975499,
16975513,
16975523,
16975549,
16975559,
16975561,
16975577,
16975579,
16975591,
16975613,
16975649,
16975661,
16975663,
16975697,
16975703,
16975727,
16975729,
16975733,
16975781,
16975787,
16975813,
16975831,
16975867,
16975891,
16975913,
16975921             

            
            
            };
            int lenP = fnv32PrimeArray.Length -1;

       //     fnv32Prime = fnv32PrimeArray[ bytes.Length % lenP];

            // Prime        Offset          Conflit
            //  139969      2147483647          7         
            //  139907      2147483647          5      
            // 16777619     2147483647          4   
            // 0x01000193   2147483647         34
            uint hash = fnv32Offset;

            //for (uint i = 0; i < bytes.Length; i++)
            //    hash = (hash ^ bytes[i]) * fnv32Prime;
  // 1

            //foreach (byte x in bytes)
            //    if (x % 2 == 0)
            //        hash = (hash ^ x) * fnv32Prime;
            //    else
            //        hash = (hash ^ x) * fnv32Prime1;
// 2

            //for (var i = 0; i < bytes.Length; i++)
            //{
            //    hash = hash ^ bytes[i];
            //    hash *= fnv32Prime;
            //}

            //3

            for (var i = 0; i < bytes.Length; i++)
            {
                hash = hash ^ bytes[i] ;
                hash *= fnv32Prime;
            }

       //   uint   hashx =hash ^ (uint)(bytes[0] * bytes[1]);
           
            return hash;
                                 
        }



        public static ulong Hash64FNV1ax(byte[] bytes)  
      {
            ulong hash = fnv64Offset;
 
                //for (var i = 0; i < bytes.Length; i++)
                //{
                //    hash = hash ^ bytes[i];
                //    hash *= fnv64Prime;
                //}

            foreach (byte x in bytes)
                    hash = (hash ^ x) * fnv64Prime;

                return hash>>32 ^ (uint)hash ;
        }
    }
}



//for (byte i = 0; i < bytes.Length; i+=2)
//{
//    if (i % 2 == 0)
//    {
//        hash = (hash << 8) | bytes[i];
//    }
//    else
//        hash = hash ^ bytes[i];

//    hash *= fnv32Prime;
//}
