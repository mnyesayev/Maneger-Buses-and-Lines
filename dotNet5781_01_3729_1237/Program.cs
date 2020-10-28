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

            manageBuses Buses = new manageBuses();
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

                switch (userChoise)
                {
                    case Choises.addbus:


                        int[] date = new int[3];

                        Console.WriteLine("Please enter the date of road ascent");
                        Console.WriteLine("enter -- year space month space day");

                        for (int i = 0; i < 3; i++)
                        {
                            while (!int.TryParse(Console.ReadLine(), out date[i]))
                                Console.WriteLine("Wrong input! Try Again.");
                        }

                        Console.WriteLine("pleace enter id for the bus");
                        while (!uint.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine("Wrong input! Try Again.");

                        Buses.AddBus(new DateTime(date[0], date[1], date[2]), id);
                       
                        break;
                    case Choises.chooseBus:

                        Console.WriteLine("pleace enter id for the bus");
                        while (!uint.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine("Wrong input! Try Again.");

                        Buses.ChooseBus(id);

                        break;
                    case Choises.driverService:
                       
                        Console.WriteLine("pleace enter id for the bus");
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
                        Console.WriteLine("goodBye");
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
