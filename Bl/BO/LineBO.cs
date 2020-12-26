using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        /// <summary>
        /// Represents the inner unique number of the "Line"
        /// </summary>
        public int IdLine { get; set; }
        /// <summary>
        /// Represents the number of the Line
        /// </summary>
        public string NumLine { get; set; }
        /// <summary>
        /// Represents the agency of the Line
        /// </summary>
        public Agency CodeAgency { get; set; }
        /// <summary>
        /// Represents the area of the Line
        /// </summary>
        public Areas Area { get; set; }
        /// <summary>
        /// Represents all stops in the Line
        /// </summary>
        public IEnumerable<StopLine> StopsInLine { get; set; }
        /// <summary>
        /// Represents the more info of about the Line
        /// </summary>
        public string MoreInfo { get; set; }
    }
}
