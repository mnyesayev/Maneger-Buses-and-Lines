using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03A_3729_1237
{
    /// <summary>
    /// This class represents a logical bus line station 
    /// with the physical station database data (so we inherited), 
    /// and also includes distance and time information from the previous station (currently random values)
    /// and a Boolean variable that indicates if this station is first on 
    /// the line (to initial it with zero distance and time)
    /// </summary>
    public class BusLineStation : BusStation
    {
        private double distancePrevStation;
        private TimeSpan minutesTimePrevStation;
        private bool isFirst;
        public double DistancePrevStation { get => distancePrevStation; set => distancePrevStation = value; }
        public TimeSpan MinutesTimePrevStation { get => minutesTimePrevStation; set => minutesTimePrevStation = value; }
        /// <summary>
        /// property to indicate if is first stop of this line
        /// </summary>
        public bool IsFirst { get => isFirst; set => isFirst = value; }
        /// <summary>
        /// A constructor who must get at least a "physical station"
        /// </summary>
        /// <param name="busStation"></param>
        /// <param name="distance">Distance in kilometers from the previous station</param>
        /// <param name="minutsTime">Distance in minutes from previous station</param>
        /// <param name="first">bool var. to indicate if is first stop of this line</param>
        public BusLineStation(BusStation busStation, double distance = 0, double minutsTime = 0, bool first = false)
        {
            base.Latitude = busStation.Latitude;
            base.Longitude = busStation.Longitude;
            base.BusStationKey = busStation.BusStationKey;
            base.Address = busStation.Address;
            IsFirst = first;
            if (!first && distance == 0 && minutsTime == 0)
            {
                DistancePrevStation = MyRandom.GetDoubleRandom(0.5, 10);
                MinutesTimePrevStation = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(1, 10));
            }
            else
            {
                DistancePrevStation = distance;
                MinutesTimePrevStation = TimeSpan.FromMinutes(minutsTime);
            }
        }
        public override string ToString()
        {
            return base.ToString()+"  "+this.MinutesTimePrevStation.ToString(@"hh\:mm\:ss");
        }
    }
}
