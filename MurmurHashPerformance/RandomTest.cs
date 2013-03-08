using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;


namespace MurmurHashPerformance
{
    public class RandomTest : HashTester
    {
             
        public override void InitTestingData()
        {
            RandomData = GenerateRandomData(256 * 1024);
        }

        public  byte[] RandomData;


        public  override byte[] GetTestingDataForIter(long i)
        {
            return RandomData;
        }

        public  string title = "\n===  Test 1 -- Random data for 10000 iterations (each block contains 256K)\n\n";

        public  void HashTesting()
        {
            Console.WriteLine(title);

            InitTestingData();

            long length = RandomData.Length;

            long iterations = 10000;

            {
                HashFunc shaDelegate = new HashFunc(HashServices.CRC32Hash);
                Stopwatch r = Profile(shaDelegate, iterations);
                Report("CRC32Hash  profile...", length, iterations, r);
            }
            {
                HashFunc shaDelegate = new HashFunc(HashServices.CRC32AHash);
                Stopwatch r = Profile(shaDelegate, iterations);
                Report("CRC32AHash  profile...", length, iterations, r);
            }

            {
                HashFunc shaDelegate = new HashFunc(HashServices.ConverToBase64);
                Stopwatch r = Profile(shaDelegate, iterations);
                Report("ConverToBase64 profile...", length, iterations, r);
            }


            {
                HashFunc murmurDelegate = new HashFunc(HashServices.Murmurhash);
                Stopwatch r = Profile(murmurDelegate, iterations);
                Report("Murmurhash profile...", length, iterations, r);
            }

            {
                HashFunc shaDelegate = new HashFunc(HashServices.SHA1Hash);
                Stopwatch r = Profile(shaDelegate, iterations);
                Report("SHA1Hash profile...", length, iterations, r);
            }
                   
        }




        public  byte[] GenerateRandomData(long length)
        {
            byte[] data = new byte[length];

            using (var gen = RandomNumberGenerator.Create())
                gen.GetBytes(data);


            return data;
        }

    }
}
