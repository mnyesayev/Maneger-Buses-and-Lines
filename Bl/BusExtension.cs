using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace Bl
{
    public static class BusExtension
    {
        private static readonly Random r=new Random();
        internal static void setNewBus(this Bus bus)
        {
            if (bus == null)
                throw new NullReferenceException("The bus was null");
            bus.LastCare = bus.DateRoadAscent;
            bus.Fuel = 1200;
            bus.LastCareMileage = bus.Mileage;            
            bus.State = States.ready;
        }
        internal static void setOldBus(this Bus bus)
        {
            if (bus == null)
                throw new NullReferenceException("The bus was null");
            bus.Fuel = r.Next(1, 1201);
            if (DateTime.Compare(bus.LastCare, DateTime.Now.AddYears(-1)) <= 0
                || bus.Mileage - bus.LastCareMileage >= 20000)
                bus.State = States.mustCare;           
        }
        internal static void Care(this Bus bus)
        {
            if (bus == null)
                throw new NullReferenceException("The bus was null");
            bus.LastCareMileage = bus.Mileage;
            bus.LastCare = DateTime.Today;
            bus.State = States.ready;
            bus.Fuel = 1200;
        }
        internal static void Fuel(this Bus bus)
        {
            if (bus == null)
                throw new NullReferenceException("The bus was null");
            bus.Fuel=1200;
        }
    }
}
