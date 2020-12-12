using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum States
    {
        ready, drive, refueling, care, mustCare, mustRefuel
    }
    /// <summary>
    /// The class represents a single bus of DO.
    /// </summary>
    public class BusDO
    {
        uint id;
        uint mileage;
        int fuel;
        uint lastCareMileage;
        DateTime dateRoadAscent;
        DateTime lastCare;
        States state;
        /// <summary>
        /// Represents the bus license number
        ///  by years as provided by law
        /// </summary>
        public uint Id { get => id; private set => id = value; }

        /// <summary>
        ///  Represents the date of ascent to the road.
        /// </summary>
        public DateTime DateRoadAscent
        { get => dateRoadAscent; private set => dateRoadAscent = value; }

        /// <summary>
        ///  Represents the amount of mileage that the bus passed from entering the Road
        /// </summary>
        public uint Mileage { get => mileage; set => mileage = value; }
        /// <summary>
        ///  Represents how many miles the bus can travel further
        /// </summary>
        public int Fuel { get => fuel; set => fuel = value; }
        /// <summary>
        ///  Represents the date of the last treatment in the garage
        /// </summary>
        public DateTime LastCare { get => lastCare; set => lastCare = value; }
        /// <summary>
        ///  Represents the amount of bus mileage in the last care
        /// </summary>
        public uint LastCareMileage { get => lastCareMileage; set => lastCareMileage = value; }
        /// <summary>
        /// Represents the state of the bus
        /// </summary>
        public States State { get => state; set => state = value; }

        /// <summary>
        /// A Ctor who creates a bus and also serves as a default Ctor
        /// </summary>
        /// <param name="dateRoadAscent"></param>
        /// <param name="id"></param>
        /// <param name="mileage"></param>
        /// <param name="fuel"></param>
        public BusDO(DateTime dateRoadAscent = default, uint id = 0, uint mileage = 0, int fuel = 1200)
        {
            DateRoadAscent = dateRoadAscent;
            Id = id;
            Mileage = mileage;
            Fuel = fuel;
            LastCare = DateRoadAscent;
            LastCareMileage = mileage;
        }


    }
}



