using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class TripOnLine
    {
        public int Id { get; set; }
        public int IdLine { get; set; }
        public TimeSpan Time { get; set; }
        public string StartAneEnd { get; set; }
        /// <summary>
        /// Represents the frequency of line per hour
        /// </summary>
        public int Frequency { get; set; }
    }
}
