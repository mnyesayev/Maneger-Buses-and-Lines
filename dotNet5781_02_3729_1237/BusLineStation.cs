using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    public class BusLineStation 
    {
        private double distancePrevStation;
        private TimeSpan minutesTimePrevStation;
        BusStation busStation;
        public double DistancePrevStation { get => distancePrevStation; set => distancePrevStation = value; }
        public TimeSpan MinutesTimePrevStation { get => minutesTimePrevStation; set => minutesTimePrevStation = value; }
        public BusStation BusStation { get => busStation; set => busStation = value; }

        public BusLineStation(BusStation busStation, double distance = 0, double minutsTime = 0)
        {
            BusStation = busStation;
            DistancePrevStation = distance;
            MinutesTimePrevStation = TimeSpan.FromMinutes(minutsTime);
        }
    }
}
