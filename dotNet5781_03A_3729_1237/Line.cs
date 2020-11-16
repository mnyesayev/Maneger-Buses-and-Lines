using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03A_3729_1237
{
    /// <summary>
    /// The class represents a bus line containing up to two routes 
    /// provided they are in the same area
    /// as well as an interface of comparison.
    /// </summary>
    public class Line : IComparable
    {
        public enum Areas
        {
            General, North, South, Center, Jerusalem, Lowland, JudeaAndSamaria
        }
        private List<BusLineStation> stations;
        private int numLine;
        private BusLineStation firstStation;
        private BusLineStation lastStation;
        private Areas area;



        internal List<BusLineStation> Stations { get => stations; set => stations = value; }
        internal BusLineStation FirstStation { get => firstStation; set => firstStation = value; }
        internal BusLineStation LastStation { get => lastStation; set => lastStation = value; }
        public Areas Area { get => area; set => area = value; }
        public int NumLine { get => numLine; set => numLine = value; }
        /// <summary>
        /// A constructor for internal needs who builds an empty line
        /// </summary>
        /// <param name="numLine"></param>
        /// <param name="area"></param>
        private Line(int numLine = 0, Areas area = Areas.General)
        {
            Stations = new List<BusLineStation>();
            NumLine = numLine;
            Area = area;
        }
        /// <summary>
        /// A public constructor who receives two physical stations (departure,destination), 
        /// a line number and area of activity
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <param name="numLine"></param>
        /// <param name="area"></param>
        public Line(BusStation station1, BusStation station2, int numLine, Areas area = Areas.General)
        {
            Stations = new List<BusLineStation>();
            var lStation1 = new BusLineStation(busStation: station1, first: true);
            var lStation2 = new BusLineStation(station2);
            Stations.Add(lStation1);
            Stations.Add(lStation2);
            FirstStation = stations[0];
            LastStation = stations[1];
            NumLine = numLine;
            Area = area;
        }
        /// <summary>
        /// Returns a string of line stations
        /// </summary>
        /// <returns>A string of line stations</returns>
        private string StringStations()
        {
            string temp = null;
            foreach (var state in Stations)
                temp += state.ToString();

            return temp;
        }

        public override string ToString()
        {
            return $"Bus line: {NumLine} \nArea: {Area} \nStations:\n{StringStations()}";
        }
        /// <summary>
        /// Receives a line station designated for the first station and updates 
        /// its data accordingly
        /// </summary>
        /// <param name="lineStation"></param>
        private void setFirst(BusLineStation lineStation)
        {
            lineStation.DistancePrevStation = 0;
            lineStation.MinutesTimePrevStation = TimeSpan.Zero;
            FirstStation = lineStation;
        }
        /// <summary>
        ///Returns a boolean variable whether the station addition was successful
        /// </summary>
        /// <param name="station"></param>
        /// <param name="index"> </param>
        /// <returns>a boolean variable whether the station addition was successful</returns>
        public bool AddStation(BusStation station, int index)
        {
            if (CheckStation(station.BusStationKey) || index > Stations.Count || index < 0)
                return false;
            if (index == 0)//wiil be new first stop
            {
                var newfirst = new BusLineStation(station, first: true);
                Stations[0].DistancePrevStation = MyRandom.GetDoubleRandom(0.5, 10);
                Stations[0].MinutesTimePrevStation = TimeSpan.FromMinutes(MyRandom.GetDoubleRandom(1, 10));
                Stations.Insert(index, newfirst);
                FirstStation = newfirst;
                return true;
            }
            var newStop = new BusLineStation(station);
            if (index == Stations.Count)//wiil be new last stop
            {
                Stations.Add(newStop);
                LastStation = newStop;
                return true;
            }
            Stations.Insert(index, newStop);
            return true;
        }
        /// <summary>
        /// Gets a station key and tries to delete the station from the line
        /// Exception:If you try to delete a station from a line that contains only two stations
        /// , an throw "NotSupportedException"
        /// </summary>
        /// <param name="stationKey"></param>
        /// <returns>a boolean variable whether the station addition was successful</returns>
        public bool DelStation(int stationKey)
        {
            int i = 0;
            if (Stations.Count == 2)
            {
                throw new NotSupportedException("You can not delete a station from a line with only two stations!");
            }
            foreach (var station in Stations)
            {
                if (station.BusStationKey == stationKey)
                {
                    if (FirstStation.BusStationKey == station.BusStationKey && stations.Count() > 1)
                        setFirst(Stations[i + 1]);
                    if (LastStation.BusStationKey == station.BusStationKey && stations.Count() > 1)
                        LastStation = stations[i - 1];
                    if (stations.Count() == 1)
                        FirstStation = LastStation = null;
                    this.Stations.RemoveAt(i);
                    return true;
                }
                ++i;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationKey"></param>
        /// <returns>if station exist return true else false</returns>
        public bool CheckStation(int stationKey)
        {
            foreach (var station in Stations)
            {
                if (station.BusStationKey == stationKey)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Returns an index of the station in the list of stations 
        /// if it does not find it then returns -1
        /// </summary>
        /// <param name="stationKey"></param>
        /// <returns></returns>
        private int getIndex(int stationKey)
        {
            int i = 0;
            foreach (var element in Stations)
            {
                if (element.BusStationKey == stationKey)
                    return i;
                ++i;
            }
            return -1;
        }
        /// <summary>
        /// Calculate the length between given stations
        /// Exceptions:"KeyNotFoundException",ArgumentException
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns>double number</returns>
        public double GetDistance(BusStation station1, BusStation station2)
        {
            int index1 = getIndex(station1.BusStationKey);
            int index2 = getIndex(station2.BusStationKey);
            if (index1 == -1 || index2 == -1)
                throw new KeyNotFoundException($"One or more of the stations does not exist in this direction of the line {this.NumLine}");
            if (index1 < index2)
            {
                double distance = 0;
                for (int i = index1; i < index2; i++)
                {
                    distance += Stations[i + 1].DistancePrevStation;
                }
                return distance;
            }
            else
            {
                throw new ArgumentException("You cannot measure distance between stations " +
                    "\nthat are not in chronological order of the line in that direction");
            }

        }
        /// <summary>
        /// Calculates drive time between two given stations
        /// Exceptions:" KeyNotFoundException" ,"ArgumentException"
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns> struct of TimeSpan</returns>
        public TimeSpan GetMinutesTime(BusStation station1, BusStation station2)
        {
            int index1 = getIndex(station1.BusStationKey);
            int index2 = getIndex(station2.BusStationKey);
            if (index1 == -1 || index2 == -1)
            {
                throw new KeyNotFoundException($"One or more of the stations does not exist in this direction of the line {this.NumLine}");
            }
            if (index1 < index2)
            {
                TimeSpan minutesTime = TimeSpan.Zero;//is now 00:00:00
                for (int i = index1; i < index2; i++)
                {
                    minutesTime += Stations[i + 1].MinutesTimePrevStation;
                }
                return minutesTime;
            }
            else
            {
                throw new ArgumentException("You cannot measure time between stations " +
                    "\nthat are not in chronological order of the line in that direction");
            }
        }
        /// <summary>
        /// Returns a route between two stations on the line
        /// Exceptions: "ArgumentException","KeyNotFoundException"
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns>Line from 2 BusStation</returns>
        public Line SubLine(BusStation station1, BusStation station2)
        {
            int index1 = getIndex(station1.BusStationKey);
            int index2 = getIndex(station2.BusStationKey);
            if (index1 == -1 || index2 == -1)
            {
                throw new KeyNotFoundException($"One or more of the stations does not exist in this direction of the line {this.NumLine}");
            }
            if (index1 < index2)
            {
                Line temp = new Line
                {
                    area = this.area,
                    numLine = this.numLine
                };
                int j = 0;
                for (int i = index1; i <= index2; i++)
                {
                    temp.Stations.Insert(j, stations[i]);
                    ++j;
                }
                temp.FirstStation = stations[index1];
                temp.LastStation = stations[index2];
                return temp;
            }
            else
            {
                throw new ArgumentException("You can not find a subway between stations " +
                    "\nthat are not in chronological order of the line in that direction");
            }
        }
        /// <summary>
        /// Implementation of the IComparable interface by comparing drive times between 2 lines
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>1 or 0 or -1</returns>
        int IComparable.CompareTo(object obj)
        {
            Line line = (Line)obj;
            TimeSpan time1 = this.GetMinutesTime(FirstStation, LastStation);
            TimeSpan time2 = line.GetMinutesTime(line.FirstStation, line.LastStation);
            if (time1 > time2)
                return 1;
            if (time1 == time2)
                return 0;
            else
                return -1;
        }
    }
}
