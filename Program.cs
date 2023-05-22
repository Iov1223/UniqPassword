using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace UniqPassword
{
    internal class Program
    {
        public static string GenPas(int size)
        {
            string str = "", pas = "";
            for (int i = 33; i < 125; i++)
            {
                str += (char)i;
            }
            var random = new Random();
            var buffer = new byte[size];
            for (int i = 0; i < size; i++)
            {
                random.NextBytes(buffer);
                pas += str[buffer[0] % str.Length];
            }
            return pas;
        }
        static void Main(string[] args)
        {
            List<string> listPas = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(GenPas(10).ToString());
                Thread.Sleep(200);
            }
        }
    }
}
