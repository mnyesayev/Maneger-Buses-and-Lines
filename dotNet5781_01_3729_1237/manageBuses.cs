using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3729_1237
{
    class manageBuses
    {

        private List<Bus> buses;

        public List<Bus> Buses { get => buses; set => buses = value; }

        public manageBuses()
        {
            Buses = new List<Bus>();
        }

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
        public  Bus SearchBus(uint id)
        {
            foreach (var bus in Buses)
            {
                if (bus.Id != id)
                    continue;
                else
                    return  bus;
            }
            return  null;
        }
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
            {
                bus.startDrive(lengthRoute);
            }

        }
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
        
        // returns how meny mileage the bus was driving -
        // - from the last care
        public uint LastCare(uint id)
        {
            Bus bus = this.SearchBus(id);
            return (bus.Mileage - bus.LastCareMileage);
        }
    }
}
