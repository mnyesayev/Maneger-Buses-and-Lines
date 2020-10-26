using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3729_1237
{
    public class Bus
    {
        uint id;
        uint mileage;
        uint fuel;
        uint lastCareMileage;
        DateTime dateRoadAscent;
        DateTime lastCare;

        public uint Id
        {
            get => id;
            set
            {
                if (DateRoadAscent.Year < 2018 && value <= 9999999)
                    id = value;
                else if (DateRoadAscent.Year >= 2018 && value <= 99999999)
                    id = value;
                else
                    Console.WriteLine( "This id not valid!");

            }
        }
        public DateTime DateRoadAscent 
        { get => dateRoadAscent; private set => dateRoadAscent = value; }
        public uint Mileage { get => mileage; set => mileage = value; }
        public uint Fuel { get => fuel; set => fuel = value; }
        public DateTime LastCare { get => lastCare; set => lastCare = value; }
        public uint LastCareMileage { get => lastCareMileage; set => lastCareMileage = value; }

        public Bus(DateTime dateRoadAscent=default,uint id=0,uint mileage=0,uint fuel=0)
        {
            DateRoadAscent = dateRoadAscent;
            Id=id;
            Mileage = mileage;
            Fuel = fuel;
            LastCare = default;
            LastCareMileage = mileage;
        }
        public void Care()
        {
            LastCare = DateTime.Now;
            LastCareMileage = mileage;
        }
        public bool CheckCare()
        {
            if(Mileage-LastCareMileage>=20000)
                return false;
            //else if(DateTime.Now- lastCare>TimeSpan)
                //return false;
            return true;
        }
    }
}
