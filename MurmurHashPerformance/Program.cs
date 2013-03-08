using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace MurmurHashPerformance
{


    public delegate string HashFunc(byte[] data);
    public delegate byte[] GetHashTestingData(int index);

  

    class Program
    {

        static void Main(string[] args)
        {
            EnglishWordHashTesting e1 = new EnglishWordHashTesting();
            e1.InitTestingData();
            RandomTest r1 = new RandomTest();
            r1.HashTesting();


            Console.ReadLine();

        }

    }
}
