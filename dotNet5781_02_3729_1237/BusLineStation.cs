using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    public class BusLineStation : BusStation
    {
        private double distancePrevStation;
        private TimeSpan minutesTimePrevStation;
        private bool firstStation;
        public double DistancePrevStation { get => distancePrevStation; set => distancePrevStation = value; }
        public TimeSpan MinutesTimePrevStation { get => minutesTimePrevStation; set => minutesTimePrevStation = value; }
        public bool FirstStation { get => firstStation; set => firstStation = value; }
        public BusLineStation(BusStation busStation, double distance = 0, double minutsTime = 0, bool first = false)
        {
            base.Latitude = busStation.Latitude;
            base.Longitude = busStation.Longitude;
            base.BusStationKey = busStation.BusStationKey;
            base.Address = busStation.Address;
            FirstStation = first;
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
    }
}
