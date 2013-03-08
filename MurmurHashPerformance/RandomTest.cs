using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;


namespace MurmurHashPerformance
{
    public class RandomTest  
    {

        public static void InitTestingData()
        {
            RandomData = GenerateRandomData(256 * 1024);
        }

        public static byte[] RandomData;


        public static byte[] GetTestingDataForIter(long i)
        {
            return RandomData;
        }

        public static string title = "\n===  Test 1 -- Random data for 10000 iterations (each block contains 256K)\n\n";

        public static void HashTesting()
        {
            Console.WriteLine(title);

            InitTestingData();

            long length = RandomData.Length;

            long iterations = 10000;

            //{
            //    HashFunc shaDelegate = new HashFunc(HashServices.CRC32Hash);
            //    Stopwatch r = Profile(shaDelegate, iterations);
            //    Report("CRC32Hash  profile...", length, iterations, r);
            //}
            //{
            //    HashFunc shaDelegate = new HashFunc(HashServices.CRC32AHash);
            //    Stopwatch r = Profile(shaDelegate, iterations);
            //    Report("CRC32AHash  profile...", length, iterations, r);
            //}

            //{
            //    HashFunc oBase64 = new HashFunc(HashServices.ConverToBase64);
            //    Stopwatch r = Profile(oBase64, iterations);
            //    Report("ConverToBase64 profile...", length, iterations, r);
            //}


            {
                HashFunc murmurDelegate = new HashFunc(HashServices.Murmurhash);

                var timer = Stopwatch.StartNew();
                for (long i = 0; i < iterations; i++)
                {
                    HashServices.Murmurhash(GetTestingDataForIter(i));
                }
                // stop profiling
                timer.Stop();
                                       
                Report("Murmurhash profile...", length, iterations, timer);
            }

            //{
            //    HashFunc shaDelegate = new HashFunc(HashServices.SHA1Hash);
            //    Stopwatch r = Profile(shaDelegate, iterations);
            //    Report("SHA1Hash profile...", length, iterations, r);
            //}
                   
        }




        public static byte[] GenerateRandomData(long length)
        {
            byte[] data = new byte[length];

            var gen = RandomNumberGenerator.Create();
                gen.GetBytes(data);


            return data;
        }


        //internal Stopwatch Profile(HashFunc hashFunc, long iterations)
        //{
        //    var timer = Stopwatch.StartNew();
        //    for (long i = 0; i < iterations; i++)
        //    {
        //        hashFunc(GetTestingDataForIter(i));
        //    }
        //    // stop profiling
        //    timer.Stop();
        //    return timer;
        //}


        static void Report(string title, long length, long iterations, Stopwatch timer)
        {
            double totalBytes = length * iterations;
            double totalSeconds = timer.ElapsedMilliseconds / 1000.0;

            double bytesPerSecond = totalBytes / totalSeconds;
            double mbitsPerSecond = (bytesPerSecond / (1024.0 * 1024.0));

            Console.WriteLine("\n" + title);
            Console.WriteLine(" test Bytes     :" + totalBytes);
            Console.WriteLine(" iterations     :" + iterations);
            Console.WriteLine(" totalSeconds   :" + totalSeconds);

            Console.WriteLine(" bytesPerSecond :" + bytesPerSecond);

            Console.WriteLine(" mbitsPerSecond :" + mbitsPerSecond);

        }

    }
}
