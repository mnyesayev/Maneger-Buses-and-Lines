using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{

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
        private Areas Area { get => area; set => area = value; }
        public int NumLine { get => numLine; set => numLine = value; }

        public Line(int busLine = 0, Areas area = Areas.General)
        {
            Stations = new List<BusLineStation>();
            NumLine = busLine;
            Area = area;
        }
        public string StringStations()
        {
            string temp = null;
            foreach (var state in Stations)
            {

                temp += state.ToString();
            }
            return temp;
        }

        public override string ToString()
        {
            return $"Bus line: {NumLine} \nArea: {Area} \nStations:\n{StringStations()}";
        }
        public bool addStation(BusLineStation station, int index)
        {
            if (checkStation(station.BusStationKey) || index > Stations.Count || index < 0)
                return false;
            if (index == 0)
            {
                Stations.Insert(index, station);
                FirstStation = station;
                if (Stations.Count() == 1)
                    LastStation = station;
                return true;
            }
            if (index == Stations.Count())
            {
                Stations.Add(station);
                LastStation = station;
                return true;
            }
            Stations.Insert(index, station);
            return true;
        }
        public bool DelStation(int stationKey)
        {
            int i = 0;
            foreach (var station in Stations)
            {
                if (station.BusStationKey == stationKey)
                {

                    if (FirstStation == station && stations.Count() > 1)
                        FirstStation = stations[i + 1];
                    if (LastStation == station && stations.Count() > 1)
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
        public bool checkStation(int stationKey)
        {
            foreach (var station in Stations)
            {
                if (station.BusStationKey == stationKey)
                    return true;
            }
            return false;
        }
        private int getIndex(BusLineStation station)
        {
            int i = 0;
            foreach (var element in Stations)
            {
                if (element.BusStationKey == station.BusStationKey)
                    return i;
                ++i;
            }
            return -1;
        }
        public double GetDistance(BusLineStation station1, BusLineStation station2)
        {
            int index1 = getIndex(station1);
            int index2 = getIndex(station2);
            if (index1 == -1 || index2 == -1)
                throw new KeyNotFoundException("One or more stations do not exist on this line!");
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
        public TimeSpan GetMinutesTime(BusLineStation station1, BusLineStation station2)
        {
            int index1 = getIndex(station1);
            int index2 = getIndex(station2);
            if (index1 == -1 || index2 == -1)
            {
                throw new KeyNotFoundException("One or more stations do not exist on this line!");
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
        public Line SubLine(BusLineStation station1, BusLineStation station2)
        {
            int index1 = getIndex(station1);
            int index2 = getIndex(station2);
            if (index1 == -1 || index2 == -1)
            {
                throw new KeyNotFoundException("One or more stations do not exist on this line!");
            }
            if (index1 < index2)
            {
                Line temp = new Line();
                for (int i = index1; i < index2; i++)
                {
                    temp.Stations.Insert(i, stations[i]);
                }
                temp.FirstStation = station1;
                temp.LastStation = station2;
                return temp;
            }
            else
            {
                throw new ArgumentException("You can not find a subway between stations " +
                    "\nthat are not in chronological order of the line in that direction");
            }
        }

        int IComparable.CompareTo(object obj)
        {
            Line line = (Line)obj;
            TimeSpan time1 = this.GetMinutesTime(FirstStation, LastStation);
            TimeSpan time2 = line.GetMinutesTime(FirstStation, LastStation);
            if (time1 > time2)
                return 1;
            if (time1 == time2)
                return 0;
            else
                return -1;
        }
    }
}
