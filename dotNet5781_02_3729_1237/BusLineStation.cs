using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    public class BusLineStation:BusStation 
    {
        private double distancePrevStation;
        private TimeSpan minutesTimePrevStation;
        public double DistancePrevStation { get => distancePrevStation; set => distancePrevStation = value; }
        public TimeSpan MinutesTimePrevStation { get => minutesTimePrevStation; set => minutesTimePrevStation = value; }
        public BusLineStation(BusStation busStation, double distance = 0, double minutsTime = 0)
        {
            base.Latitude = busStation.Latitude;
            base.Longitude = busStation.Longitude;
            base.BusStationKey = busStation.BusStationKey;
            base.Address = busStation.Address;
            DistancePrevStation = distance;
            MinutesTimePrevStation = TimeSpan.FromMinutes(minutsTime);
        }
    }
}
