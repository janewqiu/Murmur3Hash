using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MurmurHashPerformance
{
    public abstract class HashTester
    {
        public abstract void InitTestingData();
        
                               
        public abstract  byte[] GetTestingDataForIter(long i);


        internal  Stopwatch Profile(HashFunc hashFunc, long iterations)
        {
            var timer = Stopwatch.StartNew();
            for (long i = 0; i < iterations; i++)
            {
                hashFunc(GetTestingDataForIter(i));
            }
            // stop profiling
            timer.Stop();
            return timer;
        }


        internal  void Report(string title, long length, long iterations, Stopwatch timer)
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
