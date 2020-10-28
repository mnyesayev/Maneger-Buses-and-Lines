using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3729_1237
{
    /// <summary>
    /// A class that manages a fleet of buses and is responsible for
    /// selecting buses and adding them and services for its drivers.
    /// </summary>
    class ManageBuses
    {
        private List<Bus> buses;

        public List<Bus> Buses { get => buses; set => buses = value; }

        /// <summary>
        /// A default Ctor that creates an empty bus list
        /// </summary>
        public ManageBuses()
        {
            Buses = new List<Bus>();
        }
        /// <summary>
        /// The function adds a new bus and receives the date of its ascent 
        /// to the road and its license
        /// </summary>
        /// <param name="dateRoadAscent"></param>
        /// <param name="id"></param>
        public void AddBus(DateTime dateRoadAscent, uint id)
        {
            if (Buses.Count() == 0)
                Buses.Add(new Bus(dateRoadAscent, id));
            else
            {
                Bus bus = this.SearchBus(id);
                if (bus == null)
                    Buses.Add(new Bus(dateRoadAscent, id));
            }
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
                Console.WriteLine(Bus.PrintId() + " ------ " + Bus.ReturnLastCare());
            }
        }
    }
}
