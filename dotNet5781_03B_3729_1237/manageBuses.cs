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
        public void AddBus(DateTime dateRoadAscent, uint idBus)
        {
            Buses.Add(new Bus(dateRoadAscent, idBus));
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


