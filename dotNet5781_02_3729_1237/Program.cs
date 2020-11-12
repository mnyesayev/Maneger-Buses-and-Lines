using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    static class MyRandom
    {
        public static Random r = new Random(DateTime.Now.Millisecond);
        public static double GetDoubleRandom(double min, double max)
        {
            return (r.NextDouble() * (max - min)) + min;
        }
    }
    public enum Choises { exit, addLine, addStation, delLine, delStation, search, noReplacements, printLines, printStations };
    class Program
    {

        static void Cin(out int t)
        {
            while (!int.TryParse(Console.ReadLine(), out t))
                Console.WriteLine("Wrong input! Try Again.");
        }
        static void Main(string[] args)
        {
            Lines allTheLines = new Lines();
            List<BusStation> listStations = new List<BusStation>();

            // create rendom busStation
            for (int i = 1; i < 41; i++)
                listStations.Add(new BusStation(i));
            // create 10 lines
            for (int i = 52, x = 1, y = 11; i < 62; ++i, ++x, ++y)
                allTheLines.AddLine(new Line(listStations[x], listStations[y], i));

            // add to evry line 4 stations
            int j = 0, h = 0;

            foreach (Line item in allTheLines)
            {
                item.AddStation(listStations[j], j);
                ++j;
            }
            foreach (Line item in allTheLines)
            {
                item.AddStation(listStations[j], h + 1);
                ++j;
            }
            --j;
            foreach (Line item in allTheLines)
            {
                item.AddStation(listStations[j], h + 1);
                --j;
            }
            /*foreach (Line item in allTheLines)
            {
                item.AddStation(new BusLineStation(listStations[j]), h + 1);
                ++j;
            }*/
            string[] options = new string[]
            {
                "Exit the program",
                "Add a new line",
                "Add a new station to an existing line",
                "Delete an existing line",
                "Delete a station from an existing line",
                "Search lines that pass through the station by station number",
                "To find a route without changing buses",
                "Print all the lines in the system",
                "Print all the stations in the system and the lines that pass through them"
            };
            Choises userChoise;
            do
            {
                for (int i = 0; i < options.Length; i++)
                    Console.WriteLine("Press {0} to " + options[i], i);

                // get the number
                int choise;
                //while (!int.TryParse(Console.ReadLine(), out choise))
                //  Console.WriteLine("Wrong input! Try Again.");
                Cin(out choise);
                userChoise = (Choises)choise;
                int temp, index, stationKey, stationKey2;
                BusStation busStation1, busStation2;
                switch (userChoise)
                {
                    case Choises.exit:
                        Console.WriteLine("GoodBye");
                        break;
                    case Choises.addLine:
                        Console.WriteLine("Plese enter the number of line:");
                        Cin(out temp);
                        Console.WriteLine("Enter a departure station number");
                        Cin(out stationKey);
                        Console.WriteLine("Enter a destination station number");
                        Cin(out stationKey2);
                        busStation1 = listStations.Find(delegate (BusStation station)
                        { return station.BusStationKey == stationKey; });
                        busStation2 = listStations.Find(delegate (BusStation station)
                        { return station.BusStationKey == stationKey2; });
                        if (busStation1 != null && busStation2 != null)
                        {
                            if (allTheLines.AddLine(new Line(busStation1, busStation2, temp)))
                                Console.WriteLine("success");
                            else
                                Console.WriteLine("not success");
                            continue;
                        }
                        if(busStation1!=null&&busStation2==null)
                        {
                            listStations.Add(new BusStation(stationKey2));
                            if (allTheLines.AddLine(new Line(busStation1, listStations[listStations.Count-1], temp)))
                                Console.WriteLine("success");
                            else
                                Console.WriteLine("not success");
                            continue;
                        }
                        if (busStation1 == null && busStation2 != null)
                        {
                            listStations.Add(new BusStation(stationKey));
                            if (allTheLines.AddLine(new Line(listStations[listStations.Count - 1],busStation2, temp)))
                                Console.WriteLine("success");
                            else
                                Console.WriteLine("not success");
                            continue;
                        }
                        if (busStation1 == null && busStation2 == null)
                        {
                            listStations.Add(new BusStation(stationKey));
                            listStations.Add(new BusStation(stationKey2));
                            if (allTheLines.AddLine(new Line(listStations[listStations.Count - 2], listStations[listStations.Count - 1], temp)))
                                Console.WriteLine("success");
                            else
                                Console.WriteLine("not success");
                            continue;
                        }
                        break;
                    case Choises.addStation:
                        Console.WriteLine("Plese enter the number of line:");
                        Cin(out temp);
                        try
                        {
                            Lines line = allTheLines[temp];
                            Console.WriteLine("please enter the key of stop:");
                            Cin(out stationKey);
                            BusStation busStation = listStations.Find(delegate (BusStation station)
                            { return station.BusStationKey == stationKey; });
                            Console.WriteLine("Please enter a station index that will be in the new route");
                            Cin(out index);
                            if (line.AllLines.Count == 2)
                            {
                                Console.WriteLine("In which direction do you want to add a station?");
                                Console.WriteLine("Press 1 in the direction of this final stop");
                                Console.WriteLine(line.AllLines[0].LastStation.ToString());
                                Console.WriteLine("Press 2 in the direction of this final stop");
                                Console.WriteLine(line.AllLines[1].LastStation.ToString());
                                //scan choise
                                Cin(out temp);
                                if (temp != 1 && temp != 2)
                                {
                                    Console.WriteLine("Wrong input!");
                                    continue;
                                }
                                if (busStation == null)
                                {
                                    listStations.Add(new BusStation(stationKey));
                                    line.AddStaion(listStations[listStations.Count - 1], index, temp);
                                }
                                else
                                    line.AddStaion(busStation, index, temp);
                            }
                            else//If the line has meanwhile only one direction
                            {
                                if (busStation != null && line.AllLines[0].AddStation(busStation, index))
                                {
                                    Console.WriteLine("success");
                                    continue;
                                }
                                else
                                {
                                    listStations.Add(new BusStation(stationKey));
                                    if (line.AllLines[0].AddStation(listStations[listStations.Count - 1], index))
                                    {
                                        Console.WriteLine("success");
                                        continue;
                                    }
                                }
                                Console.WriteLine("not success");
                            }
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case Choises.delLine:
                        break;
                    case Choises.delStation:
                        break;
                    case Choises.search:
                        Console.WriteLine("Please enter the station key:");
                        Cin(out stationKey);
                        try
                        {
                            var tmp = allTheLines.GetLinesPerStation(stationKey);
                            Console.WriteLine("The lines that pass:");
                            foreach (var line in tmp)
                            {
                                Console.Write(line.NumLine + " ");
                            }
                            Console.WriteLine();
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case Choises.noReplacements:
                        Console.WriteLine("Enter a departure station number");
                        Cin(out stationKey);
                        Console.WriteLine("Enter a destination station number");
                        Cin(out stationKey2);
                        var t = new Lines();
                        busStation1 = listStations.Find(delegate (BusStation station)
                       { return station.BusStationKey == stationKey; });
                        busStation2 = listStations.Find(delegate (BusStation station)
                       { return station.BusStationKey == stationKey2; });
                        foreach (Line item in allTheLines)
                        {
                            try
                            {
                                t.AllLines.Add(item.SubLine(busStation1, busStation2));
                            }
                            catch (KeyNotFoundException)
                            {
                                continue;
                            }
                            catch (ArgumentException)
                            {
                                continue;
                            }
                        }
                        if (t.AllLines.Count == 0)
                        {
                            Console.WriteLine("No line was found with a direct route between the 2 given stations");
                            continue;
                        }
                        t.LowToHigh();
                        foreach (Line item in t)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case Choises.printLines:
                        foreach (Line item in allTheLines)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case Choises.printStations:
                        foreach (BusStation item in listStations)
                        {
                            try
                            {
                                var tmp = allTheLines.GetLinesPerStation(item.BusStationKey);
                                Console.WriteLine(item.ToString() + "lines:");
                                foreach (var line in tmp)
                                {
                                    Console.Write(line.NumLine + " ");
                                }
                                Console.WriteLine();
                            }
                            catch (KeyNotFoundException)
                            {
                                continue;
                            }
                        }

                        break;
                    default:
                        break;
                }
            } while (userChoise != Choises.exit);
            Console.ReadKey();
        }
    }
}
