using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3729_1237
{
    class Program
    {
        static void Main(string[] args)
        {

            manageBuses buses=new manageBuses();
            buses.AddBus(new DateTime(2018, 8, 3), 12368896);
            buses.AddBus(new DateTime(2018, 8, 3), 12345678);
            buses.ChooseBus(12345678);
            buses.DriverService(12345678, 'c');
            buses.ChooseBus(12345678);

            Console.ReadKey(); 
        }
    }
}
