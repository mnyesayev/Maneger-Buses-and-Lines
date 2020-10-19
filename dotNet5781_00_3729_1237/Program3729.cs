using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_3729_1237
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome3729();
            Welcome1237();
            Console.ReadKey();
        }
        static partial void Welcome1237();
        private static void Welcome3729()
        {
            Console.WriteLine("Enter your name:");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}
