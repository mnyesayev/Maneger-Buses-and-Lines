using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    /// <summary>
    /// the class manager counters for DO objects
    /// </summary>
    public static class Config
    {
        static int lineCounter = 0;
        public static int LineCounter => ++lineCounter;
        static int userTripCounter = 0;
        public static int UserTripCounter => ++userTripCounter;
        static int busOnTripCounter = 0;
        public static int BusOnTripCounter => ++busOnTripCounter;
        static int lineTripCounter = 0;
        public static int LineTripCounter => ++lineTripCounter;

    }
}
