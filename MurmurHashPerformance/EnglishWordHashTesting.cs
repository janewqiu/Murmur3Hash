using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MurmurHashPerformance
{
    public class EnglishWordHashTesting  
    {

         byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public    void InitTestingData()
        {

            englishwordlist = System.IO.File.ReadAllLines("TWL06.txt");
       

        }

         string[] englishwordlist;


        public    byte[] GetTestingDataForIter(long i)
        {
            return GetBytes(englishwordlist[i]);
        }

        class TestHashData
        {
            public long No;
            public string HashString;
        }

        public string title = "\n===  Test 1 -- Random data for 10000 iterations (each block contains 256K)\n\n";

        public void HashTesting()
        {
            Console.WriteLine(title);
            
            InitTestingData();

            long length = 0;

            long iterations = englishwordlist.Length;


            {
                length = 0;
                List<TestHashData> testresult = new List<TestHashData>();
                var timer = Stopwatch.StartNew();
                for (long i = 0; i < iterations; i++)
                {
                    byte[] data = GetTestingDataForIter(i);                    
                    testresult.Add(new TestHashData() { No = i, HashString = HashServices.SHA1Hash(data) });
                    length += data.Length;

                }
                // stop profiling
                timer.Stop();

                Report("SHA  profile...", length, iterations, timer);

                var conflit = testresult.GroupBy(u => u.HashString).Where(u=>u.Count()>1).ToArray();
                
                Console.WriteLine("Conflit count:" + conflit.Count());
                foreach (var x in conflit)
                {
                    Console.Write(x.Key);
                    foreach (var y in x.AsEnumerable())
                    {
                        Console.Write(englishwordlist[y.No] + "\t");
                    }
                    Console.WriteLine();
                }
            }



            {
                length = 0;
                List<TestHashData> testresult = new List<TestHashData>();
                var timer = Stopwatch.StartNew();
                for (long i = 0; i < iterations; i++)
                {
                    byte[] data = GetTestingDataForIter(i);
                    testresult.Add(new TestHashData() { No = i, HashString = HashServices.Murmurhash(data) });
                    length += data.Length;

                }
                // stop profiling
                timer.Stop();

                Report("Murmurhash  profile...", length, iterations, timer);

                var conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();

                Console.WriteLine("Conflit count:" + conflit.Count());
                foreach (var x in conflit)
                {
                    Console.Write(x.Key);
                    foreach (var y in x.AsEnumerable())
                    {
                        Console.Write(englishwordlist[y.No] + "\t");
                    }
                    Console.WriteLine();
                }
            }

           
         

        }



        void Report(string title, long length, long iterations, Stopwatch timer)
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





//englishwordlist = new List<byte[]>();
//byte[] englishword = System.IO.File.ReadAllBytes("TWL06.txt");
//int s = 0;  

//for (int i = 0; i < englishword.Length; i++)
//{                                                           
//    if (englishword[i] == 0x0d||englishword[i] == 0x0a)
//    {
//        if (i - s > 0)
//        {
//            int len = i - s;

//            byte[] data = new byte[len];
//            Array.Copy(englishword, s, data, 0, len);
//            englishwordlist.Add(data);

//        }

//        s = i + 1;
//    }

//}