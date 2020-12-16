using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// The class represents a Consecutive Stops, in the DO layer
    /// </summary>
    public class ConsecutiveStops
    {

        /// <summary>
        /// Represents the unique number of the "CodeBusStop1"
        /// </summary>
        public int CodeBusStop1 { get; set; }
        /// <summary>
        /// Represents the unique number of the "CodeBusStop2"
        /// </summary>
        public int CodeBusStop2 { get; set; }
        /// <summary>
        /// Represents the distance beetwen of the "ConsecutiveStops"
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Represents the avrege drive time beetwen of the "ConsecutiveStops"
        /// </summary>
        public TimeSpan AvregeDriveTime { get; set; }

    }
}
