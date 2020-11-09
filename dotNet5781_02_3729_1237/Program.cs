using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    public enum Choises { exit, addLine, addStation, delLine, delStation, search, noReplacements, printLines, printStations };
    class Program
    {
        static void Main(string[] args)
        {
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
                j = int.Parse(Console.ReadLine());
                st.addStation(stations[i], j);
            }
            Console.WriteLine(st.ToString());
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
                        break;
                    case Choises.printStations:
                        break;
                    default:
                        break;
                }
            } while (userChoise != Choises.exit);
            Console.ReadKey();
        }
    }
}
