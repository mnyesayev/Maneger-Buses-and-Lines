using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Areas
    {
        General, North, South, Center, Jerusalem, Lowland, JudeaAndSamaria
    }
    /// <summary>
    /// The class represents a Line, in the DO layer
    /// </summary>
    public class Line
    {
        /// <summary>
        ///  Represents if this class active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Represents the inner unique number of the "Line"
        /// </summary>
        public int IdLine { get ; set ; }
        /// <summary>
        /// Represents the number of the Line
        /// </summary>
        public int NumLine { get ; set ; }
        /// <summary>
        /// Represents the area of the Line
        /// </summary>
        public Areas Area { get ; set; }
        /// <summary>
        /// Represents the codeStop of first stop of the Line
        /// </summary>
        public int CodeFirstStop { get ; set ; }
        /// <summary>
        /// Represents the codeStop of last stop of the Line
        /// </summary>
        public int CodeLastStop { get ; set ; }
        /// <summary>
        /// Represents the more info of about the Line
        /// </summary>
        public string MoreInfo { get ; set; }        
    }
}
