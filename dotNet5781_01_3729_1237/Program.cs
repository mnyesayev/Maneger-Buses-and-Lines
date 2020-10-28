using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3729_1237
{
    public enum Choises { exit, addbus, chooseBus, driverService, lastcare };

    class Program
    {
        static void Main(string[] args)
        {

            ManageBuses Buses = new ManageBuses();
            string[] options = new string[5]
            {
                "Exit the program",
                "Add a bus to the bus fleet",
                "Choose bus to start new drive",
                "get service for the bus",
                "Get information on the last care mileage of the all buses",
            };

            Choises userChoise;
            do
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine("Press {0} to " + options[i], i);
                }
                
                // get the number

                int temp;
                while (!int.TryParse(Console.ReadLine(), out temp))
                    Console.WriteLine("Wrong input! Try Again.");
                userChoise = (Choises)temp;

                uint id;
                DateTime time;
                switch (userChoise)
                {
                    case Choises.addbus:
                        Console.WriteLine("Please enter the date of road ascent");
                        while (!DateTime.TryParse(Console.ReadLine(), out time))
                            Console.WriteLine("Wrong input! Try Again.");

                        Console.WriteLine("Pleace enter id for the bus");
                        while (!uint.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine("Wrong input! Try Again.");

                        Buses.AddBus(time, id);
                        break;
                    case Choises.chooseBus:

                        Console.WriteLine("Pleace enter id for the bus");
                        while (!uint.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine("Wrong input! Try Again.");

                        Buses.ChooseBus(id);

                        break;
                    case Choises.driverService:

                        Console.WriteLine("Pleace enter id for the bus");
                        while (!uint.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine("Wrong input! Try Again.");

                        Console.WriteLine("Pleace enter 'f' for refuling or 'c' for care");
                        char serviceOptions;
                        char.TryParse(Console.ReadLine(), out serviceOptions);
                        Buses.DriverService(id, serviceOptions);
                        break;
                    case Choises.lastcare:
                        Buses.LastCareAllBuses();
                        break;
                    case Choises.exit:
                        Console.WriteLine("GoodBye");
                        break;
                    default:
                        Console.WriteLine("Your input {0}, please enter number between 0 - 4", userChoise);
                        break;
                }
            } while (userChoise != Choises.exit);
            Console.ReadKey();
        }
    }
}
