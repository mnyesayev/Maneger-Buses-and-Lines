using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        public int IdLine { get; set; }
        public string NumLine { get; set; }
        public TimeSpan StartTime { get; set; }
        public string LastStopName { get; set; }
        public TimeSpan ArriveTime { get; set; }
    }
}
