using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStop
    {
        /// <summary>
        /// Represents the unique number of the "BusStop"
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Represents the name of Bus stop/station
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Represents the longitude location
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Represents the latitude location
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Represents more info about the stop/station 
        /// such as: Roof, disabled access, digital panel, etc.
        /// </summary>
        public string MoreInfo { get; set; }
        /// <summary>
        /// Represents the lines that pass in the stop/station 
        /// </summary>
        public IEnumerable<LineOnStop> LinesPassInStop { get; set; }

        IEnumerable<LineTiming> lineTimings { get; set; }
    }
}
