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
        public static List<StopLine> StopLines;
        public static List<Driver> Drivers;
        public static List<BusOnTrip> BusOnTrips;
        public static List<LineTrip> LineTrips;
        public static List<User> Users;
        public static List<ConsecutiveStops> LstConsecutiveStops;

        static DataSource()
        {
            Buses = new List<Bus>();
            initBuses();
            BusStops = new List<BusStop>();
            initBusStops();
            Lines = new List<Line>();
            initLines();
            Drivers = new List<Driver>();
            initDrivers();
            StopLines = new List<StopLine>();
            initStopLines();
            LstConsecutiveStops = new List<ConsecutiveStops>();
            initConsecutiveStops();
            BusOnTrips = new List<BusOnTrip>();
            LineTrips = new List<LineTrip>();
            Users = new List<User>();
            initUsers();
        }


        private static void initUsers()
        {
            Users.Add(new User()
            {
                Active = true,
                Authorization = Authorizations.MainAdmin,
            });
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

        private static void initDrivers()
        {
            Drivers.Add(new Driver() { Active = true, Id = 123456789, Name = "Israel Israely", Seniority = 4 });
            Drivers.Add(new Driver() { Active = true, Id = 123466789, Name = "Yakov azulay", Seniority = 4 });
            Drivers.Add(new Driver() { Active = true, Id = 123476789, Name = "Yosy Levi", Seniority = 4 });
            Drivers.Add(new Driver() { Active = true, Id = 123486789, Name = "Asher buzaglo", Seniority = 4 });
        }

        private static void initBusStops()
        {
            int i = 0;
            foreach (var item
                in File.ReadLines(@"stops.txt"))
            {
                if (i++ == 0) continue;
                BusStops.Add(new BusStop() { Active = true });
                var subitem = item.Split(',');
                BusStops[i - 2].Code = int.Parse(subitem[1]);
                BusStops[i - 2].Name = subitem[2];
                BusStops[i - 2].Address = subitem[3];
                BusStops[i - 2].Latitude = double.Parse(subitem[4]);
                BusStops[i - 2].Longitude = double.Parse(subitem[5]);
            }
        }

        private static void initLines()
        {

            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "1",
                Area = Areas.Center,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 35335,
                CodeLastStop = 32742
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "1",
                Area = Areas.Center,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 31377,
                CodeLastStop = 35315
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "76",
                Area = Areas.Center,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 33599,
                CodeLastStop = 35335
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "76",
                Area = Areas.Center,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 35315,
                CodeLastStop = 33598
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "36",
                Area = Areas.Center,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 20277,
                CodeLastStop = 35315
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "36",
                Area = Areas.Center,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 31313,
                CodeLastStop = 25867
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "200",
                Area = Areas.General,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 60320,
                CodeLastStop = 21165
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "200",
                Area = Areas.Center,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 21730,
                CodeLastStop = 60314
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "300",
                Area = Areas.General,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 60320,
                CodeLastStop = 9807
            });
            Lines.Add(new Line()
            {
                Active = true,
                IdLine = Config.LineCounter,
                NumLine = "300",
                Area = Areas.General,
                CodeAgency = Agency.קווים,
                CodeFirstStop = 9810,
                CodeLastStop = 60314
            });
        }

        private static void initStopLines()
        {
            #region numLine1
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 35335, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 31898, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 31207, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 38159, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 31653, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 31164, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 31604, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 31065, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 31738, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 1, CodeStop = 32742, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 31377, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 38156, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 38160, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 31816, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 31442, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 38121, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 31641, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 31663, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 31596, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 2, CodeStop = 35315, NumStopInLine = 10 });
            #endregion
            #region numLine76
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 33599, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 31372, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 31419, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 31481, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 38153, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 38121, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 38123, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 31338, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 31335, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 3, CodeStop = 35335, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 35315, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 31409, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 31676, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 38120, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 31065, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 31053, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 38112, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 31674, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 36764, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 4, CodeStop = 33598, NumStopInLine = 10 });
            #endregion
            #region numLine36
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 20277, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 21516, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 21028, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 21486, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 31640, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 31663, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 31664, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 31596, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 31725, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 5, CodeStop = 35315, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 31313, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 31898, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 31335, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 38159, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 31662, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 21639, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 21595, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 21022, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 21123, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 6, CodeStop = 25867, NumStopInLine = 10 });
            #endregion
            #region numLine200
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 30320, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 60290, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 34017, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 33740, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 33432, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 23060, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 20057, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 20059, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 20207, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 7, CodeStop = 20165, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 21730, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 22273, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 22277, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 23053, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 21560, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 20068, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 34075, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 60352, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 60294, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 8, CodeStop = 60314, NumStopInLine = 10 });
            #endregion
            #region numLine300
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 60320, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 60290, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 60292, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 60001, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 64005, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 4009, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 4133, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 5060, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 1352, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 9, CodeStop = 9807, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 9810, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 1375, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 4012, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 4116, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 5130, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 65134, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 34075, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 60352, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 60294, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { IdLine = 10, CodeStop = 60314, NumStopInLine = 10 });
            #endregion
        }

        private static void initConsecutiveStops()
        {
            #region routesLine1
            int index = 1;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index-1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(0.5, 1.2),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            ++index;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(0.5, 1.2),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            #endregion
            #region routesLine76
            ++index;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(0.5, 1.2),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            ++index;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(0.5, 1.2),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            #endregion
            #region routesLine36
            ++index;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(0.5, 1.2),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            ++index;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(0.5, 1.2),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            #endregion
            #region routesLine200
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(1.8, 8),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            ++index;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(0.5, 1.2),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            #endregion
            #region routesLine300
            ++index;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(1.8, 8),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            ++index;
            for (int i = 0; i < 9; i++)
            {
                LstConsecutiveStops.Add(new ConsecutiveStops()
                {
                    CodeBusStop1 = StopLines[index - 1].CodeStop,
                    CodeBusStop2 = StopLines[index].CodeStop,
                    Distance = MyRandom.GetDoubleRandom(0.5, 1.2),
                    AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                });
                ++index;
            }
            #endregion
        }


    }
}
