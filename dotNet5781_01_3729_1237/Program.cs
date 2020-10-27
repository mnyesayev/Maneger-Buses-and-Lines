using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3729_1237
{
    public enum Choises {exit, addbus, chooseBus, driverService, lastcare };
    
    class Program
    {
        static void Main(string[] args)
        {
             string[] options = new string[5]
             {
                "Exit the program",
                "Add a bus to the bus fleet",
                "Choose bus to start new drive",
                "get service for the bus",
                "Get information on the mileage of the bus",
             };

           
            Choises userChoise;
            do
            {
               for (int i=0; i<options.Length; i++)
	           {
                    Console.WriteLine("Press {0} to " + options[i], i);
	           }

               // get the number
               
               int temp;
               while ( !int.TryParse(Console.ReadLine(), out temp) )
               Console.WriteLine("Wrong input! Try Again.");
               userChoise = (Choises)temp;

               switch (userChoise)
               {
                   case Choises.addbus:
                       Console.WriteLine("heyy");
                       break;
                   case Choises.chooseBus:
                       break;
                   case Choises.driverService:
                       break;
                   case Choises.lastcare:
                       break;
                   case Choises.exit:
                       break;
                   default:
                       Console.WriteLine(userChoise + " lol");
                       break;
               }
            
            
            } while (userChoise != Choises.exit);

            Console.ReadKey(); 
        }
    }
}
