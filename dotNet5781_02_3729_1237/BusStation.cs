using System;
using System.CodeDom.Compiler;
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
        /// Exception: "NotSupportedException" when thh value in set not in the supported range
        /// </summary>
        public int BusStationKey
        {
            get => busStationKey;
            set
            {
                if (value <= 999999 && value >= 0)
                    busStationKey = value;
                else
                {
                    throw new NotSupportedException("Our system not support to set this value at this range.");
                }
            }
        }
        /// <summary>
        /// Displays / updates a global longitude
        /// </summary>
        public double Longitude
        {
            get => longitude;
            set
            {
                if (value <= 180 || value >= -180)
                    longitude = value;
            }
        }
        /// <summary>
        /// Displays / updates a global latitude
        /// </summary>
        public double Latitude
        {
            get => latitude;
            set
            {
                if (value <= 90 || value >= -90)
                    latitude = value;
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
        /// <param name="longitude">
        /// If the parameter in the range of Israel is drawn a random number</param>
        /// <param name="latitude">
        /// If the parameter in the range of Israel is drawn a random number</param>
        /// <param name="address">City, street and number</param>
        public BusStation(int busStationKey = 0, double longitude = 180, double latitude = 90, string address = null)
        {
            try
            {
                BusStationKey = busStationKey;
            }
            catch (NotSupportedException ex)
            {
                BusStationKey = 0;
                Console.WriteLine(ex.Message+ "\nwarning! The station ID is 0, so we recommend that you change it " +
                    "\nto change press 1");
                In.Cin(out int ch);
                if(ch==1)
                {
                    In.Cin(out busStationKey);
                    this.busStationKey = busStationKey;
                }    
            }
            if (longitude <= 35.5 && longitude >= 34.3)
                Longitude = longitude;
            else
                Longitude = MyRandom.GetDoubleRandom(34.3, 35.5);
            if (latitude <= 33.3 && latitude >= 31)
                Latitude = latitude;
            else
                Latitude = MyRandom.GetDoubleRandom(31, 33.3);
            Address = address;
        }
        
        public override string ToString()
        {
            return $"Bus Station Code: {busStationKey} {Latitude}°N {Longitude}°E\n";
        }
    }
}
