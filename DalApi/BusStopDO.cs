using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// The class presents a station, in the DO layer
    /// </summary>
    public class BusStopDO
    {
        int codeBusStop;
        double longitude;
        double latitude;
        string nameBusStop;
        string address;
        string moreInfo;
        /// <summary>
        /// Represents the unique number of the "BusStop"
        /// </summary>
        public int CodeBusStop { get => codeBusStop; set => codeBusStop = value; }
        /// <summary>
        /// Represents the longitude location
        /// </summary>
        public double Longitude { get => longitude; set => longitude = value; }
        /// <summary>
        /// Represents the latitude location
        /// </summary>
        public double Latitude { get => latitude; set => latitude = value; }
        /// <summary>
        /// Represents the name of Bus stop/station
        /// </summary>
        public string NameBusStop { get => nameBusStop; set => nameBusStop = value; }
        /// <summary>
        /// Represents the address of Bus stop/station
        /// </summary>
        public string Address { get => address; set => address = value; }
        /// <summary>
        /// Represents more info about the stop/station 
        /// such as: Roof, disabled access, digital panel, etc.
        /// </summary>
        public string MoreInfo { get => moreInfo; set => moreInfo = value; }
        /// <summary>
        /// Ctor of BusStopDO
        /// </summary>
        /// <param name="codeBusStop">a unique number of the "BusStop"</param>
        /// <param name="longitude">a longitude location</param>
        /// <param name="latitude">a latitude location</param>
        /// <param name="nameBusStop">a name of Bus stop/station</param>
        /// <param name="address">a address of Bus stop/station</param>
        /// <param name="moreInfo">a more info about the stop/station</param>
        public BusStopDO(int codeBusStop = 0, double longitude = 0, double latitude = 0,
        string nameBusStop = "", string address = "", string moreInfo = "")
        {
            CodeBusStop = codeBusStop;
            Longitude = longitude;
            Latitude = latitude;
            NameBusStop = nameBusStop;
            Address = address;
            MoreInfo = moreInfo;
        }
    }
}
