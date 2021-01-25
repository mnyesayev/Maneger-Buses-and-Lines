using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace Bl
{
    static class BusExtension
    {
        internal static void setNewBus(this Bus bus)
        {
            if (bus == null)
                throw new NullReferenceException("The bus was null");
            bus.LastCare = bus.DateRoadAscent;
            bus.Fuel = 1200;
            bus.LastCareMileage = bus.Mileage;
            bus.State = States.ready;
        }
        internal static bool checkId(this Bus bus)
        {
            if (bus == null)
                throw new NullReferenceException("The bus was null");
            if (bus.DateRoadAscent.Year > 2017)
            {
                if (bus.Id.ToString().Length == 8)
                    return true;
                return false;
            }
            if (bus.DateRoadAscent.Year < 2018)
            {
                if (bus.Id.ToString().Length == 7)
                    return true;
                return false;
            }
            return false;
        }
        internal static void setState(this Bus bus)
        {
            if (bus == null)
                throw new NullReferenceException("The bus was null");

        }
    }
}
