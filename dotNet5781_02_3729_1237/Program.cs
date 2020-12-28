using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    /// <summary>
    /// Static class for generating random numbers
    /// </summary>
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
    /// <summary>
    /// Static class for Receiving Numbers from Console
    /// </summary>
    static class In
    {
        /// <summary>
        /// A function for read an integer number, including checking the format on the spot
        /// </summary>
        /// <param name="t"></param>
        public static void Cin(out int t)
        {
            while (!int.TryParse(Console.ReadLine(), out t))
                Console.WriteLine("Wrong input! Try Again.");
        }
    }

    public enum Choises { exit, addLine, addStation, delLine, delStation, search, noReplacements, printLines, printStations };
    class Program
    {

        static void Main(string[] args)
        {
            Lines allTheLines = new Lines();
            List<BusStation> listStations = new List<BusStation>();
            void init(int beg, int size, int indexLine)
            {
                if (indexLine == allTheLines.AllLines.Count)//stop condition
                    return;
                for (int i = beg; i < size; i++)
                {
                    allTheLines.AllLines[indexLine].AddStation(listStations[i], allTheLines.AllLines[indexLine].Stations.Count - 1);
                }
                init(beg, --size, ++indexLine);//call recursive
            }

            // create rendom busStation
            for (int i = 1; i < 41; i++)
                listStations.Add(new BusStation(i));
            // create 10 lines
            for (int i = 52, x = 0, y = 10; i < 62; ++i, ++x, ++y)
                allTheLines.AddLine(new Line(listStations[x], listStations[y], i));
            init(20, 40, 0);
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
                In.Cin(out int choise);
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
                        In.Cin(out temp);
                        while (temp <= 0)
                        {
                            Console.WriteLine("Invalid nember Line,Try again!");
                            In.Cin(out temp);
                        }
                        Console.WriteLine("Enter a departure station number");
                        In.Cin(out stationKey);
                        while (stationKey <= 0 || stationKey > 999999)
                        {
                            Console.WriteLine("Invalid station key,Try again!");
                            In.Cin(out stationKey);
                        }
                        Console.WriteLine("Enter a destination station number");
                        In.Cin(out stationKey2);
                        while (stationKey2 <= 0 || stationKey2 > 999999)
                        {
                            Console.WriteLine("Invalid station key,Try again!");
                            In.Cin(out stationKey2);
                        }
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
                        if (busStation1 != null && busStation2 == null)
                        {
                            listStations.Add(new BusStation(stationKey2));
                            if (allTheLines.AddLine(new Line(busStation1, listStations[listStations.Count - 1], temp)))
                                Console.WriteLine("success");
                            else
                                Console.WriteLine("not success");
                            continue;
                        }
                        if (busStation1 == null && busStation2 != null)
                        {
                            listStations.Add(new BusStation(stationKey));
                            if (allTheLines.AddLine(new Line(listStations[listStations.Count - 1], busStation2, temp)))
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
                        In.Cin(out temp);
                        while (temp <= 0)
                        {
                            Console.WriteLine("Invalid nember Line,Try again!");
                            In.Cin(out temp);
                        }
                        try
                        {
                            Lines line = allTheLines[temp];
                            Console.WriteLine("please enter the key of stop:");
                            In.Cin(out stationKey);
                            while (stationKey <= 0 || stationKey > 999999)
                            {
                                Console.WriteLine("Invalid station key,Try again!");
                                In.Cin(out stationKey);
                            }
                            BusStation busStation = listStations.Find(delegate (BusStation station)
                            { return station.BusStationKey == stationKey; });
                            Console.WriteLine("Please enter a station index(1 to n) that will be in the new route");
                            In.Cin(out index);
                            if (line.AllLines.Count == 2)
                            {
                                Console.WriteLine("In which direction do you want to add a station?");
                                Console.WriteLine("Press 1 in the direction of this final stop");
                                Console.WriteLine(line.AllLines[0].LastStation.ToString());
                                Console.WriteLine("Press 2 in the direction of this final stop");
                                Console.WriteLine(line.AllLines[1].LastStation.ToString());
                                //scan choise
                                In.Cin(out temp);
                                if (temp != 1 && temp != 2)
                                {
                                    Console.WriteLine("Wrong input!");
                                    continue;
                                }
                                if (busStation == null)
                                {
                                    listStations.Add(new BusStation(stationKey));
                                    if (line.AddStaion(listStations[listStations.Count - 1], index - 1, temp))
                                    {
                                        Console.WriteLine("success");
                                        continue;
                                    }
                                }
                                else if (line.AddStaion(busStation, index - 1, temp))
                                {
                                    Console.WriteLine("success");
                                    continue;
                                }
                            }
                            else if (line.AllLines.Count == 1)//If the line has meanwhile only one direction
                            {
                                if (busStation != null && line.AllLines[0].AddStation(busStation, index - 1))
                                {
                                    Console.WriteLine("success");
                                    continue;
                                }
                                else
                                {
                                    listStations.Add(new BusStation(stationKey));
                                    if (line.AllLines[0].AddStation(listStations[listStations.Count - 1], index - 1))
                                    {
                                        Console.WriteLine("success");
                                        continue;
                                    }
                                }
                            }
                            Console.WriteLine("not success");
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case Choises.delLine:
                        Console.WriteLine("Plese enter the number of line:");
                        In.Cin(out temp);
                        while (temp <= 0)
                        {
                            Console.WriteLine("Invalid nember Line,Try again!");
                            In.Cin(out temp);
                        }
                        try
                        {
                            Lines line1 = allTheLines[temp];
                            if (line1.AllLines.Count > 1)
                            {
                                Console.WriteLine("Which route of line do you want to delete?");
                                int i = 1;
                                foreach (Line item in line1)
                                {
                                    Console.WriteLine("Press {0} to ", i);
                                    Console.WriteLine("first stop: " + item.FirstStation.ToString());
                                    Console.WriteLine("last stop: " + item.LastStation.ToString());
                                    ++i;
                                }
                                In.Cin(out index);
                                if (index != 1 && index != 2)
                                {
                                    Console.WriteLine("Wrong input!");
                                    continue;
                                }
                                if (allTheLines.DelLine(temp, index))
                                {
                                    Console.WriteLine("success");
                                    continue;
                                }
                            }
                            if (line1.AllLines.Count == 1)
                                if (allTheLines.DelLine(temp))
                                {
                                    Console.WriteLine("success");
                                    continue;
                                }
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                        Console.WriteLine("not success");
                        break;
                    case Choises.delStation:
                        Console.WriteLine("Plese enter the number of line:");
                        In.Cin(out temp);
                        while (temp <= 0)
                        {
                            Console.WriteLine("Invalid nember Line,Try again!");
                            In.Cin(out temp);
                        }
                        try
                        {
                            Lines line = allTheLines[temp];
                            Console.WriteLine("please enter the key of stop:");
                            In.Cin(out stationKey);
                            while (stationKey <= 0 || stationKey > 999999)
                            {
                                Console.WriteLine("Invalid station key,Try again!");
                                In.Cin(out stationKey);
                            }
                            if (line.AllLines.Count == 1)
                            {
                                try
                                {
                                    if (line.AllLines[0].DelStation(stationKey))
                                    {
                                        Console.WriteLine("success");
                                        continue;
                                    }
                                }
                                catch (NotSupportedException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                            if (line.AllLines.Count > 1)
                            {
                                Console.WriteLine("Which route of line do you want to delete?");
                                int i = 1;
                                foreach (Line item in line)
                                {
                                    Console.WriteLine("Press {0} to ", i);
                                    Console.WriteLine("first stop: " + item.FirstStation.ToString());
                                    Console.WriteLine("last stop: " + item.LastStation.ToString());
                                    ++i;
                                }
                                In.Cin(out index);
                                if (index != 1 && index != 2)
                                {
                                    Console.WriteLine("Wrong input!");
                                    continue;
                                }
                                try
                                {
                                    if (line.DelStation(stationKey, index))
                                    {
                                        Console.WriteLine("success");
                                        continue;
                                    }
                                }
                                catch (NotSupportedException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            Console.WriteLine("not success");
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case Choises.search:
                        Console.WriteLine("Please enter the station key:");
                        In.Cin(out stationKey);
                        while (stationKey <= 0 || stationKey > 999999)
                        {
                            Console.WriteLine("Invalid station key,Try again!");
                            In.Cin(out stationKey);
                        }
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
                        In.Cin(out stationKey);
                        while (stationKey <= 0 || stationKey > 999999)
                        {
                            Console.WriteLine("Invalid station key,Try again!");
                            In.Cin(out stationKey);
                        }
                        Console.WriteLine("Enter a destination station number");
                        In.Cin(out stationKey2);
                        while (stationKey2 <= 0 || stationKey2 > 999999)
                        {
                            Console.WriteLine("Invalid station key,Try again!");
                            In.Cin(out stationKey2);
                        }
                        var t = new Lines();
                        busStation1 = listStations.Find(delegate (BusStation station)
                       { return station.BusStationKey == stationKey; });
                        busStation2 = listStations.Find(delegate (BusStation station)
                       { return station.BusStationKey == stationKey2; });
                        if (busStation1 == null || busStation2 == null)
                        {
                            Console.WriteLine("One or more stations do not exist in the system!");
                            continue;
                        }
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
                        Console.WriteLine("what do you not understand?");
                        Console.WriteLine("ENTER NUMBER BETWEEN 0-8!!");
                        break;
                }
            } while (userChoise != Choises.exit);
            Console.ReadKey();
        }
    }
}
