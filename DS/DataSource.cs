using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    static class MyRandom
    {
        public static Random r = new Random(DateTime.Now.Millisecond);
        /// <summary>
        /// Returns an real random number between two ranges(min,max)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>an real random number between two ranges(min,max)</returns>
        public static double GetDoubleRandom(double min, double max)
        {
            return (r.NextDouble() * (max - min)) + min;
        }
    }
    public static class DataSource
    {
       
        public static List<Bus> Buses;
        public static List<BusStop> BusStops;
        public static List<Line> Lines;
        static DataSource()
        {
            Buses = new List<Bus>();
            initBuses();
            BusStops = new List<BusStop>();
            initBusStops();
            Lines = new List<Line>();
            initLines();
        }

        private static void initLines()
        {
            for (int i = 0; i < 10; i++)
            {
                Lines.Add(new Line() { Active = true, IdLine = Config.LineCounter });
            }
        }

        private static void initBusStops()
        {
            int i = 0;
            foreach (var item
                in File.ReadLines(@"stops.txt"))
            {
                if (i++ == 0) continue;
                BusStops.Add(new BusStop() { Active=true});
                var subitem = item.Split(',');
                BusStops[i - 2].Code = int.Parse(subitem[1]);
                BusStops[i - 2].Name = subitem[2];
                BusStops[i - 2].Address = subitem[3];
                BusStops[i - 2].Latitude = double.Parse(subitem[4]);
                BusStops[i - 2].Longitude = double.Parse(subitem[5]);
            }
        }

        private static void initBuses()
        {
            int numBuses = 20;
            for (int i = 0; i < numBuses; i++)
            {
                Buses.Add(new Bus()
                {
                    DateRoadAscent = new DateTime(2020, 2, 23),
                    Id = (uint)MyRandom.r.Next(1000000, 99999999),
                    Fuel = MyRandom.r.Next(50, 1201),
                    LastCare = new DateTime(2020, MyRandom.r.Next(3, 12), MyRandom.r.Next(1, 29)),
                    LastCareMileage = 22000,
                    Mileage = (uint)MyRandom.r.Next(22000, 42000),
                    State = States.ready,
                    Active = true
                });
            }
        }



    }
}
