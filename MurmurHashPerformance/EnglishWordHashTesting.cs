using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MurmurHashPerformance
{
    public class EnglishWordHashTesting  
    {

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public    void InitTestingData()
        {
           string[] datafile =System.IO.File.ReadAllLines("TWL06d.txt");
           englishwordlist = new string[datafile.Length];

           int i=0;
           foreach (string s in datafile)
           {
               englishwordlist[i++] = s;//.ToUpper();
               //englishwordlist[i++] = s.ToLower();
               //englishwordlist[i++] = s[0].ToString().ToUpper() + s.ToLower().Substring(1);
           }
       

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
        int iterationIter = 30;
        public void HashTesting()
        {
            Console.WriteLine(title);
            
            InitTestingData();

            double length = 0;

            long iterations = englishwordlist.Length;

            {
                length = 0;


                var timer = Stopwatch.StartNew();
                IGrouping<string, TestHashData>[] conflit = null;
                for (int x = 0; x < iterationIter; x++)
                {
                    List<TestHashData> testresult = new List<TestHashData>();
                    for (long i = 0; i < iterations; i++)
                    {
                        byte[] data = GetTestingDataForIter(i);
                        testresult.Add(new TestHashData() { No = i, HashString = HashServices.CPP_FNVHash(data) });
                        length += data.Length;

                    }
                    if (x == 0)
                    {
                        conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                    }
                }
                // stop profiling
                timer.Stop();

                Report("CPP_FNVHash  profile...", length, iterations, timer);
                if (conflit != null)
                {
                    Console.WriteLine("Conflit count:" + conflit.Length);
                    foreach (var x in conflit.Take(6))
                    {
                        Console.Write(x.Key);
                        foreach (var y in x.AsEnumerable())
                        {
                            Console.Write(englishwordlist[y.No] + " | ");
                        }
                        Console.WriteLine();
                    }
                }
            }


            {
                length = 0;


                var timer = Stopwatch.StartNew();
                IGrouping<string, TestHashData>[] conflit = null;
                for (int x = 0; x < iterationIter; x++)
                {
                    List<TestHashData> testresult = new List<TestHashData>();
                    for (long i = 0; i < iterations; i++)
                    {
                        byte[] data = GetTestingDataForIter(i);
                        testresult.Add(new TestHashData() { No = i, HashString = HashServices.FNV1A32(data) });
                        length += data.Length;

                    }
                    if (x == 0)
                    {
                        conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                    }
                }
                // stop profiling
                timer.Stop();

                Report("FNV1A32  profile...", length, iterations, timer);
                if (conflit != null)
                {
                    Console.WriteLine("Conflit count:" + conflit.Length);
                    foreach (var x in conflit.Take(6))
                    {
                        Console.Write(x.Key);
                        foreach (var y in x.AsEnumerable())
                        {
                            Console.Write(englishwordlist[y.No] + " | ");
                        }
                        Console.WriteLine();
                    }
                }
            }


            {
                length = 0;


                var timer = Stopwatch.StartNew();
                IGrouping<string, TestHashData>[] conflit = null;
                for (int x = 0; x < iterationIter; x++)
                {
                    List<TestHashData> testresult = new List<TestHashData>();
                    for (long i = 0; i < iterations; i++)
                    {
                        byte[] data = GetTestingDataForIter(i);
                        testresult.Add(new TestHashData() { No = i, HashString = HashServices.MD4HashTest(data) });
                        length += data.Length;

                    }
                    if (x == 0)
                    {
                        conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                    }
                }
                // stop profiling
                timer.Stop();

                Report("MD4HashTest  profile...", length, iterations, timer);
                if (conflit != null)
                {
                    Console.WriteLine("Conflit count:" + conflit.Length);
                    foreach (var x in conflit.Take(6))
                    {
                        Console.Write(x.Key);
                        foreach (var y in x.AsEnumerable())
                        {
                            Console.Write(englishwordlist[y.No] + " | ");
                        }
                        Console.WriteLine();
                    }
                }
            }



            {
                length = 0;


                var timer = Stopwatch.StartNew();
                IGrouping<string, TestHashData>[] conflit = null;
                for (int x = 0; x < iterationIter; x++)
                {
                    List<TestHashData> testresult = new List<TestHashData>();
                    for (long i = 0; i < iterations; i++)
                    {
                        byte[] data = GetTestingDataForIter(i);
                        testresult.Add(new TestHashData() { No = i, HashString = HashServices.Murmur3_64Bit(data) });
                        length += data.Length;

                    }
                    if (x == 0)
                    {
                        conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                    }
                }
                // stop profiling
                timer.Stop();

                Report("Murmur3_64Bit  profile...", length, iterations, timer);
                if (conflit != null)
                {
                    Console.WriteLine("Conflit count:" + conflit.Length);
                    foreach (var x in conflit.Take(6))
                    {
                        Console.Write(x.Key);
                        foreach (var y in x.AsEnumerable())
                        {
                            Console.Write(englishwordlist[y.No] + " | ");
                        }
                        Console.WriteLine();
                    }
                }
            }
            /*
            
           {
               length = 0;

                
               var timer = Stopwatch.StartNew();
               IGrouping<string, TestHashData>[] conflit = null;
               for (int x = 0; x < iterationIter; x++)
               {
                   List<TestHashData> testresult = new List<TestHashData>();
                   for (long i = 0; i < iterations; i++)
                   {
                       byte[] data = GetTestingDataForIter(i);
                       testresult.Add(new TestHashData() { No = i, HashString = HashServices.FNV1A32(data) });
                       length += data.Length;

                   }
                   if (x == 0)
                   {
                       conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                   }
               }
               // stop profiling
               timer.Stop();

               Report("FNV1A32  profile...", length, iterations, timer);
               if (conflit != null)
               {
                   Console.WriteLine("Conflit count:" + conflit.Length);
                   foreach (var x in conflit.Take(6))
                   {
                       Console.Write(x.Key);
                       foreach (var y in x.AsEnumerable())
                       {
                           Console.Write(englishwordlist[y.No] + " | ");
                       }
                       Console.WriteLine();
                   }
               }
           }

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
                       Console.Write(englishwordlist[y.No] + " | ");
                   }
                   Console.WriteLine();
               }
           }



           {
               length = 0;


               var timer = Stopwatch.StartNew();
               IGrouping<string, TestHashData>[] conflit = null;
               for (int x = 0; x < iterationIter; x++)
               {
                   List<TestHashData> testresult = new List<TestHashData>();
                   for (long i = 0; i < iterations; i++)
                   {
                       byte[] data = GetTestingDataForIter(i);
                       testresult.Add(new TestHashData() { No = i, HashString = HashServices.FNV1A64(data) });
                       length += data.Length;

                   }
                   if (x == 0)
                   {
                       conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                   }
               }
               // stop profiling
               timer.Stop();

               Report("FNV1A64  profile...", length, iterations, timer);
               if (conflit != null)
               {
                   Console.WriteLine("Conflit count:" + conflit.Length);
                   foreach (var x in conflit.Take(6))
                   {
                       Console.Write(x.Key);
                       foreach (var y in x.AsEnumerable())
                       {
                           Console.Write(englishwordlist[y.No] + " | ");
                       }
                       Console.WriteLine();
                   }
               }
           }


           {
               length = 0;


               var timer = Stopwatch.StartNew();
               IGrouping<string, TestHashData>[] conflit = null;
               for (int x = 0; x < iterationIter; x++)
               {
                   List<TestHashData> testresult = new List<TestHashData>();
                   for (long i = 0; i < iterations; i++)
                   {
                       byte[] data = GetTestingDataForIter(i);
                       testresult.Add(new TestHashData() { No = i, HashString = HashServices.CRC32Hash(data) });
                       length += data.Length;

                   }
                   if (x == 0)
                   {
                       conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                   }
               }
               // stop profiling
               timer.Stop();

               Report("CRC32Hash  profile...", length, iterations, timer);
               if (conflit != null)
               {
                   Console.WriteLine("Conflit count:" + conflit.Length);
                   foreach (var x in conflit.Take(6))
                   {
                       Console.Write(x.Key);
                       foreach (var y in x.AsEnumerable())
                       {
                           Console.Write(englishwordlist[y.No] + " | ");
                       }
                       Console.WriteLine();
                   }
               }
           }
        



           {
               length = 0;
               List<TestHashData> testresult = new List<TestHashData>();
               var timer = Stopwatch.StartNew();
               for (long i = 0; i < iterations; i++)
               {
                   byte[] data = GetTestingDataForIter(i);
                   testresult.Add(new TestHashData() { No = i, HashString = HashServices.MD4HashTest(data) });
                   length += data.Length;

               }
               // stop profiling
               timer.Stop();

               Report("MD4  profile...", length, iterations, timer);

               var conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();

               Console.WriteLine("Conflit count:" + conflit.Count());
               foreach (var x in conflit)
               {                                                                                   
                   Console.Write(x.Key);
                   foreach (var y in x.AsEnumerable())
                   {
                       Console.Write(englishwordlist[y.No] + " | ");
                   }
                   Console.WriteLine();
               }
           }



  

           {
               length = 0;


               var timer = Stopwatch.StartNew();
               IGrouping<string, TestHashData>[] conflit = null;
               for (int x = 0; x < iterationIter; x++)
               {
                   List<TestHashData> testresult = new List<TestHashData>();
                   for (long i = 0; i < iterations; i++)
                   {
                       byte[] data = GetTestingDataForIter(i);
                       testresult.Add(new TestHashData() { No = i, HashString = HashServices.Murmur2_32bit(data) });
                       length += data.Length;

                   }
                   if (x == 0)
                   {
                       conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                   }
               }
               // stop profiling
               timer.Stop();


               Report("Murmur2 -32bit  profile...", length, iterations, timer);
               if (conflit != null)
               {
                   Console.WriteLine("Conflit count:" + conflit.Length);
                   foreach (var x in conflit.Take(6))
                   {
                       Console.Write(x.Key);
                       foreach (var y in x.AsEnumerable())
                       {
                           Console.Write(englishwordlist[y.No] + " | ");
                       }
                       Console.WriteLine();
                   }
               }
           }



           {
               length = 0;


               var timer = Stopwatch.StartNew();
               IGrouping<string, TestHashData>[] conflit = null;
               for (int x = 0; x < iterationIter; x++)
               {
                   List<TestHashData> testresult = new List<TestHashData>();
                   for (long i = 0; i < iterations; i++)
                   {
                       byte[] data = GetTestingDataForIter(i);
                       testresult.Add(new TestHashData() { No = i, HashString = HashServices.Murmur3_64Bit(data) });
                       length += data.Length;

                   }
                   if (x == 0)
                   {
                       conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();
                   }
               }
               // stop profiling
               timer.Stop();


               Report("Murmur3_64Bit profile...", length, iterations, timer);
               if (conflit != null)
               {
                   Console.WriteLine("Conflit count:" + conflit.Length);
                   foreach (var x in conflit.Take(6))
                   {
                       Console.Write(x.Key);
                       foreach (var y in x.AsEnumerable())
                       {
                           Console.Write(englishwordlist[y.No] + " | ");
                       }
                       Console.WriteLine();
                   }
               }
           }
      

           {
               length = 0;
               List<TestHashData> testresult = new List<TestHashData>();
               var timer = Stopwatch.StartNew();
               for (long i = 0; i < iterations; i++)
               {
                   byte[] data = GetTestingDataForIter(i);
                   testresult.Add(new TestHashData() { No = i, HashString = HashServices.MD5HashCode(data) });
                   length += data.Length;

               }
               // stop profiling
               timer.Stop();

               Report("MD5HashCode  profile...", length, iterations, timer);

               var conflit = testresult.GroupBy(u => u.HashString).Where(u => u.Count() > 1).ToArray();

               Console.WriteLine("Conflit count:" + conflit.Count());
               foreach (var x in conflit)
               {
                   Console.Write(x.Key);
                   foreach (var y in x.AsEnumerable())
                   {
                       Console.Write(englishwordlist[y.No] + " | ");
                   }
                   Console.WriteLine();
               }
           }
  
/*

            {
                length = 0;
                List<TestHashData> testresult = new List<TestHashData>();
                var timer = Stopwatch.StartNew();
                for(int x =0;x<iterationIter;x++)
                for (long i = 0; i < iterations; i++)
                {
                    byte[] data = GetTestingDataForIter(i);
                    HashServices.Murmur3_64Bit(data);
                    length += data.Length;
                }
                // stop profiling
                timer.Stop();
                Report("Murmur3_64Bit Speed profile...", length, iterations, timer);
            }



            {
                length = 0;
                List<TestHashData> testresult = new List<TestHashData>();
                var timer = Stopwatch.StartNew();
                for (int x = 0; x < iterationIter; x++)
                for (long i = 0; i < iterations; i++)
                {
                    byte[] data = GetTestingDataForIter(i);
                    HashServices.FNV1A64(data);
                    length += data.Length;
                }
                // stop profiling
                timer.Stop();
                Report("FNV1A64 Speed profile...", length, iterations, timer);
            }


            {
                length = 0;
                List<TestHashData> testresult = new List<TestHashData>();
                var timer = Stopwatch.StartNew();
                for (int x = 0; x < iterationIter; x++)
                    for (long i = 0; i < iterations; i++)
                    {
                        byte[] data = GetTestingDataForIter(i);
                        HashServices.Murmur3_Ariso_64Bit(data);
                        length += data.Length;
                    }
                // stop profiling
                timer.Stop();
                Report("Murmur3_Ariso_64Bit Speed profile...", length, iterations, timer);
            }

         
*/
        }



        void Report(string title, double length, long iterations, Stopwatch timer)
        {
            double totalBytes = length ;
            double totalSeconds = timer.ElapsedMilliseconds / 1000.0;

            double bytesPerSecond = totalBytes / totalSeconds;
            double mbitsPerSecond = (bytesPerSecond / (1024.0 * 1024.0));

            Console.WriteLine("\n" + title);
            Console.WriteLine(" Total Bytes    :" + IntHelpers.ConverFilesizeFormat((long)totalBytes));
            Console.WriteLine(" Call Times     :" + iterations);
            Console.WriteLine(" totalSeconds   :" + totalSeconds);

            Console.WriteLine(" bytesPerSecond :" + IntHelpers.ConverFilesizeFormat((long)bytesPerSecond));

   //         Console.WriteLine(" mbitsPerSecond :" + (long)mbitsPerSecond);

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