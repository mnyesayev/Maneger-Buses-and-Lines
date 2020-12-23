using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// The class represents a line stop/station, in the DO layer
    /// </summary>
    public class StopLine
    {
        /// <summary>
        /// Represents the inner unique number of the "Line"
        /// </summary>
        public int IdLine { get; set; }
        /// <summary>
        /// Represents the unique code of the "BusStop"
        /// </summary>
        public int CodeStop { get; set; }
        /// <summary>
        /// Represents the number Stop/station in line
        /// </summary>
        public int NumStopInLine { get; set; }

        /// <summary>
        /// Represents the unique code of the prev stop
        /// </summary>
        public int PrevStop { get; set; }
        /// <summary>
        /// Represents the uniqu code of the next stop
        /// </summary>
        public int NextStop { get; set; }
        
    }
}
