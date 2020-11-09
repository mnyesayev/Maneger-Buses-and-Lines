using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3729_1237
{
    class Lines:IEnumerable 
    {
        private List<Line> allLines;

        public Lines()
        {
            allLines = new List<Line>();
        }

        public bool AddLine(Line line)
        {
            try
            {
                // we check if the line exsist
                Lines tmp = this[line.NumLine];
                if (tmp.allLines.Count() == 1 && line.FirstStation == tmp.allLines[0].LastStation && line.LastStation == tmp.allLines[0].FirstStation)
                {
                    allLines.Add(line);//we add a line in the opposite direction
                    return true;
                }
                return false;//There is already such a line with in two directions
            }
            catch (KeyNotFoundException) // the line not exsist
            {
                allLines.Add(line);
                return true;
            }   
        }

        public bool DelLine(int numLine)
        {
            try
            {
                Lines temp = this[numLine];

            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

       /* public bool ChackLine(int busLine)
        {
            try
            {
                Line temp = allLines[busLine];
            }
            catch
            { }
        }*/

        public Lines this[int numLine]
        {
            get
            {
                Lines temp = new Lines();
                int i = 0;
                foreach (Line item in this)//we use myEnumerator
                {
                    if (item.NumLine == numLine)
                        temp.allLines.Add(item);
                    if (temp.allLines.Count() == 2)
                        return temp;
                    ++i;
                }
                if (temp.allLines.Count == 0)
                    throw new KeyNotFoundException("the line not exsist");
                return temp;
            }
        }

        public List<Line> GetLinesPerStation(int station)
        {

        }

        public List<Line> LowToHigh()
        {

        }

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(this);
        }

        public class MyEnumerator : IEnumerator
        {
            Lines cool;
            int cntr = -1; //  before the first element!!!
            internal MyEnumerator(Lines coll) { this.cool = coll; }

            public void Reset() { cntr = -1; } 
            public object Current { get { return cool.allLines[cntr]; } }
            public bool MoveNext()
            {
                return ++cntr < cool.allLines.Count();
            }
        }
    }
}
