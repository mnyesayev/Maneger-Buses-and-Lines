using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class StopLineDO
    {
        int idLine;
        int codeStop;
        int numStopInLine;

        public int IdLine { get => idLine; set => idLine = value; }
        public int CodeStop { get => codeStop; set => codeStop = value; }
        public int NumStopInLine { get => numStopInLine; set => numStopInLine = value; }

        public StopLineDO(int idLine, int codeStop, int numStopInLine)
        {
            IdLine = idLine;
            CodeStop = codeStop;
            NumStopInLine = numStopInLine;
        }
    }
}
