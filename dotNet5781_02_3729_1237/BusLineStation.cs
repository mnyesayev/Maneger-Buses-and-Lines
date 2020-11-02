using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    class BusLineStation : BusStation
    {
        private double distancePrevStation;
        private int minutesTimePrevStation;
        public double DistancePrevStation { get => distancePrevStation; set => distancePrevStation = value; }
        public int MinutesTimePrevStation { get => minutesTimePrevStation; set => minutesTimePrevStation = value; }
        public BusLineStation(int busStationKey = 0, double longitude = 180,
            double latitude = 90, string address = null, double distance = 0, int minutsTime = 0)
            : base(busStationKey, longitude, latitude, address)
        {
            DistancePrevStation = distance;
            MinutesTimePrevStation = minutsTime;
        }
    }
}
