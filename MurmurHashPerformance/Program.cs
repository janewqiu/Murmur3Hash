using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace MurmurHashPerformance
{
    

    class Program
    {
        static void Main(string[] args)
        {
            long length = 256 * 1024;

            byte[] data = GenerateRandomData(length);

            long iterations = 10000;
            {
                HashFunc murmurDelegate = new HashFunc(HashServices.Murmurhash);
                Stopwatch r = Profile(murmurDelegate, iterations, data);
                Report("Murmur Hash profile...", length, iterations, r);
            }

            {
                HashFunc shaDelegate = new HashFunc(HashServices.SHA1Hash);
                Stopwatch r = Profile(shaDelegate, iterations, data);
                Report("BuildinSHA Hash profile...", length, iterations, r);
            }


        }

        public delegate string HashFunc(byte[] data);

        public static byte[] GenerateRandomData(long length)
        {
            byte[] data = new byte[length];

            using (var gen = RandomNumberGenerator.Create())
                gen.GetBytes(data);


            return data;
        }

        private static Stopwatch Profile(HashFunc hashFunc, long iterations,byte[] testData)
        {                                                       
            var timer = Stopwatch.StartNew();
            for (long i = 0; i < iterations; i++)
            {
                    hashFunc(testData);                             
            }
            // stop profiling
            timer.Stop();
            return timer;
        }



        private static void Report(string title ,long length, long iterations, Stopwatch timer)
        {
            double totalBytes = length * iterations;
            double totalSeconds = timer.ElapsedMilliseconds / 1000.0;

            double bytesPerSecond = totalBytes / totalSeconds;
            double mbitsPerSecond = (bytesPerSecond / (1024.0 * 1024.0));

            Console.WriteLine("\n\n"+title);
            Console.WriteLine(" test Bytes     :" + totalBytes);
            Console.WriteLine(" iterations     :" + iterations);
            Console.WriteLine(" totalSeconds   :" + totalSeconds);

            Console.WriteLine(" bytesPerSecond :" + bytesPerSecond);

            Console.WriteLine(" mbitsPerSecond :" + mbitsPerSecond);
                                                                        
        }

    }
}
