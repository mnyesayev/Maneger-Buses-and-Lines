using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class LineTripBO
    {
        /// <summary>
        /// Represents the unique inner number of the "Line"
        /// </summary>
        public int IdLine { get; set; }
        /// <summary>
        /// Represents the time of start line
        /// </summary>
        public TimeSpan StartTime { get; set; }
        /// <summary>
        /// Represents the frequency of line per hour
        /// </summary>
        public int Frequency { get; set; }
        /// <summary>
        /// Represents the time of end line
        /// </summary>
        public TimeSpan EndTime { get; set; }
    }
}
