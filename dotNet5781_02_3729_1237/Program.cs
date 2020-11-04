using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    class Program
    {
        static void Main(string[] args)
        {
            BusLineStation[] stations = new BusLineStation[5];
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 5; i++)
            {
                stations[i] = new BusLineStation(r.Next(9999));
            }
            Line st = new Line();
            int j;
            for (int i = 0; i < 5; i++)
            {
                j=int.Parse(Console.ReadLine());
                st.addStation(stations[i], j);
            }

                Console.WriteLine(st.ToString());
            Console.ReadKey();
        }
    }
}
