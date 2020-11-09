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
    class Lines : IEnumerable
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
                // the bus not exsist
                allLines[line.BusLine];
                return true;
            
            }
            catch // the bus alrady exsist
            {

                return false;
            }   
           
            

        }

        public bool DelLine(Line line)
        {
            if (!ChackLine(line.BusLine)) // the bus not exsist
                return false;
            else // the bus exsist
            {
                foreach (var item in )
                {

                }
                return false;
            }
        }

        public bool ChackLine(int busLine)
        {
            try
            {
                Line temp = allLines[busLine];
            }
            catch
            { }
        }

        public List<Line> this[int busLine]
        {
            get
            {
                List<Line> temp = new List<Line>();
                for (int i = 0; i < allLines.Count(); i++)
                {
                    if (allLines[i].BusLine == busLine)
                        temp.Add(allLines[i]);

                    if (temp.Count == 2) return temp;
                }
                if (temp.Count == 0)
                    throw new IndexOutOfRangeException("the line not exsist");
                return temp;
            }
        }

        public List<Line> GetLinesPerStation(int station)
        {

        }

        public List<Line> LowToHigh()
        {

        }

        public MyEnmrtr GetEnumerator()
        {
            return new MyEnmrtr(this);
        }

        public class MyEnmrtr : IEnumerator
        {
            Lines cool;
            int cntr = -1; //  before the first element!!!
            internal MyEnmrtr(Lines coll) { this.cool = coll; }

            public void Reset() { } // deprecated can be empty

            public object Current { get { return cool.allLines[cntr]; } }
            public bool MoveNext()
            {
                return ++cntr < cool.allLines.Count();
            }
        }
    }
}
