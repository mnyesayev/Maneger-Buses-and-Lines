using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    public class BusStation
    {
        private int busStationKey;
        private double longitude;
        private double latitude;
        private string address;
        /// <summary>
        /// Represents / updates the station number.
        /// </summary>
        protected int BusStationKey
        {
            get => busStationKey;
            set
            {
                if (value <= 999999)
                    busStationKey = value;
                else
                { }//throw
            }
        }
        /// <summary>
        /// Displays / updates a global longitude
        /// </summary>
        protected double Longitude
        {
            get => longitude;
            set
            {
                if (value <= 180 || value >= -180)
                    longitude = value;
                else
                { }//throw
            }
        }
        /// <summary>
        /// Displays / updates a global latitude
        /// </summary>
        protected double Latitude
        {
            get => latitude;
            set
            {
                if (value <= 90 || value >= -90)
                    latitude = value;
                else
                { }//throw
            }
        }
        /// <summary>
        /// Displays / updates the station address.
        /// </summary>
        public string Address { get => address; set => address = value; }
        /// <summary>
        /// Builder with / without parameters who builds a bus station.
        /// </summary>
        /// <param name="busStationKey"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="address"></param>
        public BusStation(int busStationKey = 0, double longitude = 180, double latitude = 90, string address = null)
        {
            BusStationKey = busStationKey;
            if (longitude <= 35.5 && longitude >= 34.3)
                Longitude = longitude;
            else
                Longitude = israelRandom(34.3, 35.5);
            if (latitude <= 33.3 && latitude >= 31)
                Latitude = latitude;
            else
                Latitude = israelRandom(31, 33.3);
            Address = address;
        }
        /// <summary>
        /// Gets two parameters that represent the range of the number lottery
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>Returns a double random number within the range of the State of Israel</returns>
        protected double israelRandom(double min, double max)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            return r.NextDouble() * (max - min) + min;
        }
        public override string ToString()
        {
            return $"Bus Station Code: {busStationKey} {Latitude}°N {Longitude}°E";
        }
    }
}
