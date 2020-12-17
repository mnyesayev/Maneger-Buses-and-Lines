using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// The class represents a single ,in the BO layer.
    /// </summary>
    public class Bus
    {

        /// <summary>
        ///  Represents if this class active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Represents the bus license number
        ///  by years as provided by law
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        ///  Represents the date of ascent to the road.
        /// </summary>
        public DateTime DateRoadAscent { get; set; }
        /// <summary>
        ///  Represents the amount of mileage that the bus passed from entering the Road
        /// </summary>
        public uint Mileage { get; set; }
        /// <summary>
        ///  Represents how many miles the bus can travel further
        /// </summary>
        public int Fuel { get; set; }
        /// <summary>
        ///  Represents the date of the last treatment in the garage
        /// </summary>
        public DateTime LastCare { get; set; }
        /// <summary>
        ///  Represents the amount of bus mileage in the last care
        /// </summary>
        public uint LastCareMileage { get; set; }
        /// <summary>
        /// Represents the state of the bus
        /// </summary>
        public States State { get; set; }
    }


}
