using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03A_3729_1237
{
    /// <summary>
    /// A class representing a collection of lines where each line
    /// can contain a list of lines of the line (currently up to two lines per line) 
    /// includes an implementation for the iterator, its own index
    /// </summary>
    class Lines : IEnumerable
    {
        private List<Line> allLines;

        public List<Line> AllLines { get => allLines; set => allLines = value; }
        /// <summary>
        /// defaul Ctor.
        /// </summary>
        public Lines()
        {
            AllLines = new List<Line>();
        }
        /// <summary>
        /// Gets a line and tries to add it to the collection provided there are no longer two tracks
        /// for the same line
        /// </summary>
        /// <param name="line"></param>
        /// <returns>a bool var.</returns>
        public bool AddLine(Line line)
        {
            try
            {
                // we check if the line exsist
                Lines tmp = this[line.NumLine];
                if (tmp.AllLines.Count == 1 && tmp.AllLines[0].Area == line.Area)
                {
                    AllLines.Add(line);//we add a line in the opposite direction
                    return true;
                }
                return false;//There is already such a line with in two directions 
            }
            catch (KeyNotFoundException) // the line not exsist
            {
                AllLines.Add(line);
                return true;
            }
        }
        /// <summary>
        /// Adds a stop to a particular route on the line
        /// </summary>
        /// <param name="busStation"></param>
        /// <param name="index"></param>
        /// <param name="direction"></param>
        /// <returns>a bool var.</returns>
        public bool AddStaion(BusStation busStation, int index, int direction)
        {
            if (this.AllLines[direction - 1].AddStation(busStation, index))
                return true;
            return false;
        }
        /// <summary>
        /// Deletes a line / track from a line from the collection.
        /// Exception:"KeyNotFoundException"
        /// </summary>
        /// <param name="numLine"></param>
        /// <param name="dirction"></param>
        /// <returns>a bool var.</returns>
        public bool DelLine(int numLine, int dirction = 0)
        {
            try
            {
                Lines temp = this[numLine];
                if (temp.AllLines.Count == 1)
                    AllLines.Remove(temp.AllLines[0]);
                if (temp.AllLines.Count >= 2)
                {
                    AllLines.Remove(temp.AllLines[dirction - 1]);
                }
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Delets a stop from a particular route on the line
        /// </summary>
        /// <param name="stationKey"></param>
        /// <param name="direction"></param>
        /// <returns>a bool var.</returns>
        public bool DelStation(int stationKey, int direction)
        {
            if (this.AllLines[direction - 1].DelStation(stationKey))
                return true;
            return false;
        }
        /// <summary>
        /// indexr only get. 
        /// Exeception: if number line not found throw "KeyNotFoundException"
        /// </summary>
        /// <param name="numLine"></param>
        /// <returns>lines with this number line</returns>
        public Lines this[int numLine]
        {
            get
            {
                Lines temp = new Lines();
                foreach (Line item in this)//we use myEnumerator
                {
                    if (item.NumLine == numLine)
                        temp.AllLines.Add(item);
                    if (temp.AllLines.Count == 2)
                        return temp;
                }
                if (temp.AllLines.Count == 0)
                    throw new KeyNotFoundException("the line not exsist");
                return temp;
            }
        }
        /// <summary>
        /// Gets a station number and finds the lines that pass through it
        ///If no line is found an exception of "KeyNotFoundException"
        /// </summary>
        /// <param name="station"></param>
        /// <returns>List of lines passing through this station</returns>
        public List<Line> GetLinesPerStation(int station)
        {
            List<Line> temp = new List<Line>();
            foreach (Line line in this)
            {
                foreach (var item2 in line.Stations)
                {
                    if (item2.BusStationKey == station)
                        temp.Add(line);
                }
            }
            if (temp.Count == 0)
                throw new KeyNotFoundException("there is no line in the station");
            else return temp;
        }
        /// <summary>
        /// Returns a list of lines sorted by fastest arrival time
        /// </summary>
        /// <returns></returns>
        public List<Line> LowToHigh()
        {
            var temp = this.AllLines;
            temp.Sort();
            return temp;
        }

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(this);
        }
        /// <summary>
        /// Self-implemetation of an IEnumerator interface
        /// </summary>
        public class MyEnumerator : IEnumerator
        {
            Lines cool;
            int cntr = -1; //  before the first element!!!
            internal MyEnumerator(Lines coll) { this.cool = coll; }

            public void Reset() { }
            public object Current { get { return cool.AllLines[cntr]; } }
            public bool MoveNext()
            {
                return ++cntr < cool.AllLines.Count;
            }
        }
    }
}
