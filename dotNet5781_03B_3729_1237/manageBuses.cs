using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_3729_1237
{
    /// <summary>
    /// A class that manages a fleet of buses and is responsible for
    /// selecting buses and adding them and services for its drivers.
    /// </summary>
    public class ManageBuses
    {
        private ObservableCollection<Bus> buses;

        public ObservableCollection<Bus> Buses { get => buses; set => buses = value; }

        /// <summary>
        /// A default Ctor that creates an empty bus list
        /// </summary>
        public ManageBuses()
        {
            Buses = new ObservableCollection<Bus>();
        }
        /// <summary>
        /// The function adds a new bus and receives the date of its ascent 
        /// to the road and its license
        /// </summary>
        /// <param name="dateRoadAscent"></param>
        /// <param name="id"></param>
        public void AddBus(DateTime dateRoadAscent, int idBus)
        {
            uint id=this.CheckId(dateRoadAscent, (uint)idBus);
            Buses.Add(new Bus(dateRoadAscent, id));
        }
        /// <summary>
        /// Checks the correctness of the license number in the system
        /// and handles irregular cases
        /// </summary>
        /// <param name="dateRoadAscent"></param>
        /// <param name="id"></param>
        /// <returns>Proper id</returns>
        public uint CheckId(DateTime dateRoadAscent, uint id)
        {
            bool temp = false;
            do
            {
                temp = false;
                Bus bus = this.SearchBus(id);
                if (dateRoadAscent.Year < 2018 && (id <= 9999999 && id >= 1000000) && bus == null)
                    return id;
                else if (dateRoadAscent.Year >= 2018 && (id <= 99999999 && id >= 10000000) && bus == null)
                    return id;
                else
                {
                    temp = true;
                    if (bus != null)
                        Console.WriteLine("This id already exists! Try again");
                    else
                        Console.WriteLine("Wrong input! Try Again.");
                    uint.TryParse(Console.ReadLine(), out id);
                }
            } while (temp);
            return 1;//If an unexpected fault has occurred
        }

        /// <summary>
        /// Looking for a bus according to his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The bus if it found otherwise null</returns>
        public Bus SearchBus(uint id)
        {

            foreach (var bus in Buses)
            {
                if (bus.Id != id)
                    continue;
                else
                    return bus;
            }
            return null;
        }
        /// <summary>
        /// A function selects a bus according to its id and checks if it can 
        /// make the trip otherwise it prints a message accordingly
        /// </summary>
        /// <param name="id"></param>
        public void ChooseBus(uint id)
        {
            Bus bus = this.SearchBus(id);
            if (bus == null)
            {
                Console.WriteLine("This bus not exsits");
                return;
            }
            Random r = new Random(DateTime.Now.Millisecond);
            uint lengthRoute = (uint)r.Next(1200);
            if (bus.CheckCare(lengthRoute) && bus.CheckFuel(lengthRoute))
                bus.StartDrive(lengthRoute);
            else
                Console.WriteLine("The bus cannot make the trip");
        }
        /// <summary>
        /// The function offers service to the driver according to his request.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="choice"></param>
        public void DriverService(uint id, char choice)
        {
            Bus bus = this.SearchBus(id);
            if (bus == null)
            {
                Console.WriteLine("This bus not exsits");
                return;
            }
            if (choice == 'f' || choice == 'F')
                bus.Refueling();
            if (choice == 'C' || choice == 'c')
                bus.Care();
            return;
        }
        /// <summary>
        /// The function prints the bus license how many miles each bus has performed 
        /// since the last care. 
        /// </summary>
        public void LastCareAllBuses()
        {
            foreach (var Bus in Buses)
            {
                Console.WriteLine(Bus.PrintId + " ------ " + Bus.ReturnLastCare());
            }
        }
    }

}


