using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MurmurHashPerformance
{
    public class EnglishWordHashTesting   : HashTester
    {

         byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public override  void InitTestingData()
        {

            englishwordlist = System.IO.File.ReadAllLines("TWL06.txt");
       

        }

         string[] englishwordlist;


        public  override byte[] GetTestingDataForIter(long i)
        {
            return GetBytes(englishwordlist[i]);
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