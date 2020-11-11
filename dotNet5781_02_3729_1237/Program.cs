using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    public enum Choises { exit, addLine, addStation, delLine, delStation, search, noReplacements, printLines, printStations };
    class Program
    {
        static void Main(string[] args)
        {
            Lines allTheLines = new Lines();
            List<BusStation> ListBusStation = new List<BusStation>();

            // create rendom busStation
            for (int i = 1; i < 41; i++)
                ListBusStation.Add(new BusStation(i));

            // create random line
            for (int i = 52; i < 63; i++)
                allTheLines.AddLine(new Line(i));

            // add to evry line 4 stations
            int j = 0;
            int h = 0;
            foreach (Line item in allTheLines)
            {
                item.addStation(new BusLineStation(ListBusStation[j]), h);
                ++j;
            }
            foreach (Line item in allTheLines)
            {
                item.addStation(new BusLineStation(ListBusStation[j]), h + 1);
                ++j;
            }
            foreach (Line item in allTheLines)
            {
                item.addStation(new BusLineStation(ListBusStation[j]), h + 1);
                ++j;
            }





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
                int temp;
                while (!int.TryParse(Console.ReadLine(), out temp))
                    Console.WriteLine("Wrong input! Try Again.");
                userChoise = (Choises)temp;

                switch (userChoise)
                {
                    case Choises.exit:
                        Console.WriteLine("GoodBye");
                        break;
                    case Choises.addLine:

                        break;
                    case Choises.addStation:
                        break;
                    case Choises.delLine:
                        break;
                    case Choises.delStation:
                        break;
                    case Choises.search:
                        break;
                    case Choises.noReplacements:
                        break;
                    case Choises.printLines:
                        foreach (Line item in allTheLines)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case Choises.printStations:

                        foreach (BusStation item in ListBusStation)
                        {
                            try
                            {
                                var tmp=allTheLines.GetLinesPerStation(item.BusStationKey);
                                Console.WriteLine(item.ToString());
                                foreach (var line in tmp)
                                {
                                    Console.WriteLine(line.NumLine +" ");
                                }
                            }
                            catch(KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
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
