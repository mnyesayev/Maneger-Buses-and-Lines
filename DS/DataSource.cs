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
            Lines = new List<Line>();
            initLines();
            Drivers = new List<Driver>();
            initDrivers();
            StopLines = new List<StopLine>();
            initStopLines();
            BusStops = new List<BusStop>();
            initBusStops();
            LstConsecutiveStops = new List<ConsecutiveStops>();
            initConsecutiveStops();
            BusOnTrips = new List<BusOnTrip>();
            LineTrips = new List<LineTrip>();
            initLineTrips();
            Users = new List<User>
            {
                new User()
                {
                    Active = true,
                    Authorization = Authorizations.Admin,
                    Birthday = DateTime.Today,
                    FirstName = "Admin",
                    UserName = "1",
                    Password = "1",
                    Phone = "052-1234567"
                },

               new User()
                {
                    Active = true,
                    Authorization = Authorizations.User,
                    Birthday = DateTime.Today,
                    FirstName = "Admin",
                    UserName = "2",
                    Password = "2",
                    Phone = "052-3456789"
                }
            };
            initUsers();
        }

        private static void initLineTrips()
        {
            int i = 0;
            foreach (var item in Lines)
            {
                LineTrips.Add(new LineTrip
                {
                    Active = true,
                    StartTime = new TimeSpan(6, 0, 0),
                    EndTime = new TimeSpan(20, 0, 0),
                    Frequency = MyRandom.r.Next(2, 11),
                    IdLine =++i,
                    Id=Config.LineTripCounter
            });
            }
            //i = 0;
            //foreach (var item in Lines)
            //{
            //    LineTrips.Add(new LineTrip
            //    {
            //        Active = true,
            //        StartTime = new TimeSpan(20, 0, 0),
            //        EndTime = new TimeSpan(24, 0, 0),
            //        Frequency = MyRandom.r.Next(1, 3),
            //        IdLine = ++i,
            //        Id = Config.LineTripCounter
            //    });
            //}
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
                    Id = (uint)MyRandom.r.Next(10000000, 99999999),
                    Fuel = MyRandom.r.Next(50, 1201),
                    LastCare = new DateTime(2020, MyRandom.r.Next(3, 13), MyRandom.r.Next(1, 29)),
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
            StopLines.Add(new StopLine() { NextStop = 31898, IdLine = 1, CodeStop = 35335, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 35335, NextStop = 31207, IdLine = 1, CodeStop = 31898, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 31898, NextStop = 38159, IdLine = 1, CodeStop = 31207, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 31207, NextStop = 31653, IdLine = 1, CodeStop = 38159, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 38159, NextStop = 31164, IdLine = 1, CodeStop = 31653, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 31653, NextStop = 31604, IdLine = 1, CodeStop = 31164, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 31164, NextStop = 31065, IdLine = 1, CodeStop = 31604, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 31604, NextStop = 31738, IdLine = 1, CodeStop = 31065, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 31065, NextStop = 32742, IdLine = 1, CodeStop = 31738, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 31738, IdLine = 1, CodeStop = 32742, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { NextStop = 38156, IdLine = 2, CodeStop = 31377, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 31377, NextStop = 38160, IdLine = 2, CodeStop = 38156, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 38156, NextStop = 31816, IdLine = 2, CodeStop = 38160, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 38160, NextStop = 31442, IdLine = 2, CodeStop = 31816, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 31816, NextStop = 38121, IdLine = 2, CodeStop = 31442, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 31442, NextStop = 31641, IdLine = 2, CodeStop = 38121, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 38121, NextStop = 31663, IdLine = 2, CodeStop = 31641, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 31641, NextStop = 31596, IdLine = 2, CodeStop = 31663, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 31663, NextStop = 35315, IdLine = 2, CodeStop = 31596, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 31596, IdLine = 2, CodeStop = 35315, NumStopInLine = 10 });
            #endregion
            #region numLine76
            StopLines.Add(new StopLine() { NextStop = 31372, IdLine = 3, CodeStop = 33599, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 33599, NextStop = 31419, IdLine = 3, CodeStop = 31372, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 31372, NextStop = 31481, IdLine = 3, CodeStop = 31419, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 31419, NextStop = 38153, IdLine = 3, CodeStop = 31481, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 31481, NextStop = 38121, IdLine = 3, CodeStop = 38153, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 38153, NextStop = 38123, IdLine = 3, CodeStop = 38121, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 38121, NextStop = 31338, IdLine = 3, CodeStop = 38123, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 38123, NextStop = 31335, IdLine = 3, CodeStop = 31338, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 31338, NextStop = 35335, IdLine = 3, CodeStop = 31335, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 31335, IdLine = 3, CodeStop = 35335, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { NextStop = 31409, IdLine = 4, CodeStop = 35315, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 35315, NextStop = 31676, IdLine = 4, CodeStop = 31409, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 31409, NextStop = 38120, IdLine = 4, CodeStop = 31676, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 31676, NextStop = 31065, IdLine = 4, CodeStop = 38120, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 38120, NextStop = 31053, IdLine = 4, CodeStop = 31065, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 31065, NextStop = 38112, IdLine = 4, CodeStop = 31053, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 31053, NextStop = 31674, IdLine = 4, CodeStop = 38112, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 38112, NextStop = 36764, IdLine = 4, CodeStop = 31674, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 31674, NextStop = 33598, IdLine = 4, CodeStop = 36764, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 36764, IdLine = 4, CodeStop = 33598, NumStopInLine = 10 });
            #endregion
            #region numLine36
            StopLines.Add(new StopLine() { NextStop = 21516, IdLine = 5, CodeStop = 20277, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 20277, NextStop = 21028, IdLine = 5, CodeStop = 21516, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 21516, NextStop = 21486, IdLine = 5, CodeStop = 21028, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 21028, NextStop = 31640, IdLine = 5, CodeStop = 21486, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 21486, NextStop = 31663, IdLine = 5, CodeStop = 31640, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 31640, NextStop = 31664, IdLine = 5, CodeStop = 31663, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 31663, NextStop = 31596, IdLine = 5, CodeStop = 31664, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 31664, NextStop = 31725, IdLine = 5, CodeStop = 31596, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 31596, NextStop = 35315, IdLine = 5, CodeStop = 31725, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 31725, IdLine = 5, CodeStop = 35315, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { NextStop = 31898, IdLine = 6, CodeStop = 31313, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 31313, NextStop = 31335, IdLine = 6, CodeStop = 31898, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 31898, NextStop = 38159, IdLine = 6, CodeStop = 31335, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 31335, NextStop = 31662, IdLine = 6, CodeStop = 38159, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 38159, NextStop = 21639, IdLine = 6, CodeStop = 31662, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 31662, NextStop = 21595, IdLine = 6, CodeStop = 21639, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 21639, NextStop = 21022, IdLine = 6, CodeStop = 21595, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 21595, NextStop = 21123, IdLine = 6, CodeStop = 21022, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 21022, NextStop = 25867, IdLine = 6, CodeStop = 21123, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 21123, IdLine = 6, CodeStop = 25867, NumStopInLine = 10 });
            #endregion
            #region numLine200
            StopLines.Add(new StopLine() { NextStop = 60290, IdLine = 7, CodeStop = 60320, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 60320, NextStop = 34017, IdLine = 7, CodeStop = 60290, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 60290, NextStop = 33740, IdLine = 7, CodeStop = 34017, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 34017, NextStop = 33432, IdLine = 7, CodeStop = 33740, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 33740, NextStop = 23060, IdLine = 7, CodeStop = 33432, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 33432, NextStop = 20057, IdLine = 7, CodeStop = 23060, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 23060, NextStop = 20059, IdLine = 7, CodeStop = 20057, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 20057, NextStop = 20207, IdLine = 7, CodeStop = 20059, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 20059, NextStop = 20165, IdLine = 7, CodeStop = 20207, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 20207, IdLine = 7, CodeStop = 20165, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { NextStop = 22273, IdLine = 8, CodeStop = 21730, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 21730, NextStop = 22277, IdLine = 8, CodeStop = 22273, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 22273, NextStop = 23053, IdLine = 8, CodeStop = 22277, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 22277, NextStop = 21560, IdLine = 8, CodeStop = 23053, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 23053, NextStop = 20068, IdLine = 8, CodeStop = 21560, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 21560, NextStop = 34075, IdLine = 8, CodeStop = 20068, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 20068, NextStop = 60352, IdLine = 8, CodeStop = 34075, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 34075, NextStop = 60294, IdLine = 8, CodeStop = 60352, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 60352, NextStop = 60314, IdLine = 8, CodeStop = 60294, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 60294, IdLine = 8, CodeStop = 60314, NumStopInLine = 10 });
            #endregion
            #region numLine300
            StopLines.Add(new StopLine() { NextStop = 60290, IdLine = 9, CodeStop = 60320, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 60320, NextStop = 60292, IdLine = 9, CodeStop = 60290, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 60290, NextStop = 60001, IdLine = 9, CodeStop = 60292, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 60292, NextStop = 64005, IdLine = 9, CodeStop = 60001, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 60001, NextStop = 4009, IdLine = 9, CodeStop = 64005, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 64005, NextStop = 4133, IdLine = 9, CodeStop = 4009, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 4009, NextStop = 5060, IdLine = 9, CodeStop = 4133, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 4133, NextStop = 1352, IdLine = 9, CodeStop = 5060, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 5060, NextStop = 9807, IdLine = 9, CodeStop = 1352, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 1352, IdLine = 9, CodeStop = 9807, NumStopInLine = 10 });

            StopLines.Add(new StopLine() { NextStop = 1375, IdLine = 10, CodeStop = 9810, NumStopInLine = 1 });
            StopLines.Add(new StopLine() { PrevStop = 9810, NextStop = 4012, IdLine = 10, CodeStop = 1375, NumStopInLine = 2 });
            StopLines.Add(new StopLine() { PrevStop = 1375, NextStop = 4116, IdLine = 10, CodeStop = 4012, NumStopInLine = 3 });
            StopLines.Add(new StopLine() { PrevStop = 4012, NextStop = 5130, IdLine = 10, CodeStop = 4116, NumStopInLine = 4 });
            StopLines.Add(new StopLine() { PrevStop = 4116, NextStop = 65134, IdLine = 10, CodeStop = 5130, NumStopInLine = 5 });
            StopLines.Add(new StopLine() { PrevStop = 5130, NextStop = 34075, IdLine = 10, CodeStop = 65134, NumStopInLine = 6 });
            StopLines.Add(new StopLine() { PrevStop = 65134, NextStop = 60352, IdLine = 10, CodeStop = 34075, NumStopInLine = 7 });
            StopLines.Add(new StopLine() { PrevStop = 34075, NextStop = 60294, IdLine = 10, CodeStop = 60352, NumStopInLine = 8 });
            StopLines.Add(new StopLine() { PrevStop = 60352, NextStop = 60314, IdLine = 10, CodeStop = 60294, NumStopInLine = 9 });
            StopLines.Add(new StopLine() { PrevStop = 60294, IdLine = 10, CodeStop = 60314, NumStopInLine = 10 });
            #endregion
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
                BusStops[i - 2].PassLines = (StopLines.Any((stopLine) => stopLine.CodeStop == int.Parse(subitem[1]))) ? true : false;
                BusStops[i - 2].Name = subitem[2];
                BusStops[i - 2].Address = subitem[3];
                BusStops[i - 2].Latitude = double.Parse(subitem[4]);
                BusStops[i - 2].Longitude = double.Parse(subitem[5]);
            }
        }

        private static void initConsecutiveStops()
        {
            #region routesLine76&1&36
            int index=1;
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (LstConsecutiveStops.Any((ConsecutiveStops) =>
                       ConsecutiveStops.CodeBusStop1 == StopLines[index - 1].CodeStop
                       && ConsecutiveStops.CodeBusStop2 == StopLines[index].CodeStop))
                    {
                        ++index;
                        continue;
                    }
                    LstConsecutiveStops.Add(new ConsecutiveStops()
                    {
                        CodeBusStop1 = StopLines[index - 1].CodeStop,
                        CodeBusStop2 = StopLines[index].CodeStop,
                        Distance =Math.Round(MyRandom.GetDoubleRandom(0.5, 1.2),2),
                        AvregeDriveTime = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(2, 5))
                    });
                    ++index;
                }
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
                    Distance = Math.Round(MyRandom.GetDoubleRandom(1.8, 8),2),
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
                    Distance =Math.Round( MyRandom.GetDoubleRandom(0.5, 1.2),2),
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
