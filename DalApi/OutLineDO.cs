using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class OutLineDO
    {
        int idLine;
        TimeSpan startTime;
        TimeSpan frequency;
        TimeSpan endTime;
        public int IdLine { get => idLine; set => idLine = value; }
        public TimeSpan StartTime { get => startTime; set => startTime = value; }
        public TimeSpan Frequency { get => frequency; set => frequency = value; }
        public TimeSpan EndTime { get => endTime; set => endTime = value; }

        public OutLineDO(int idLine,TimeSpan startTime,TimeSpan frequency,TimeSpan endTime)
        {
             IdLine=idLine;
             StartTime=startTime;
             Frequency=frequency;
             EndTime=endTime;
        }
    }
}
