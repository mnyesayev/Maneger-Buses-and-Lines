using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class LineOnStop
    {
        /// <summary>
        /// Represents the inner unique number of the "Line"
        /// </summary>
        public int IdLine { get; set; }
        /// <summary>
        /// Represents the number of the Line
        /// </summary>
        public string NumLine { get; set; }
        public string NameFirstLineStop { get; set; }
        public string NameLastLineStop { get; set; }
    }
}
