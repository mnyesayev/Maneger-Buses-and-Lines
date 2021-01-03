using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// The class represents a station, in the DO layer
    /// </summary>
    public class BusStop
    {
        /// <summary>
        ///  Represents if this class active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        ///  Represents if passe any line in this stop
        /// </summary>
        public bool PassLines { get; set; }
        /// <summary>
        /// Represents the unique number of the "BusStop"
        /// </summary>
        public int Code { get ; set ; }
        /// <summary>
        /// Represents the longitude location
        /// </summary>
        public double Longitude { get ; set ; }
        /// <summary>
        /// Represents the latitude location
        /// </summary>
        public double Latitude { get ; set ; }
        /// <summary>
        /// Represents the name of Bus stop/station
        /// </summary>
        public string Name { get ; set; }
        /// <summary>
        /// Represents the address of Bus stop/station
        /// </summary>
        public string Address { get ; set ; }
        /// <summary>
        /// Represents more info about the stop/station 
        /// such as: Roof, disabled access, digital panel, etc.
        /// </summary>
        public string MoreInfo { get ; set ; }
        
        
    }
}
