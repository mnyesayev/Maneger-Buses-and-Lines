using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    /// <summary>
    /// The class represents a Consecutive Stops, in the DO layer
    /// </summary>
    public class ConsecutiveStops
    {

        /// <summary>
        /// Represents the unique number of the "CodeBusStop1"
        /// </summary>
        public int CodeBusStop1 { get; set; }
        /// <summary>
        /// Represents the unique number of the "CodeBusStop2"
        /// </summary>
        public int CodeBusStop2 { get; set; }
        /// <summary>
        /// Represents the distance beetwen of the "ConsecutiveStops"
        /// </summary>
        public double Distance { get; set; }
        private TimeSpan time;
        /// <summary>
        /// Represents the avrege drive time beetwen of the "ConsecutiveStops"
        /// </summary>
        [XmlIgnore]
        public TimeSpan AvregeDriveTime { get => time; set => time = value; }

        [XmlElement("AvregeDriveTime", DataType = "duration")]
        [DefaultValue("PT10M")]
        public string XmlTime
        {
            get { return XmlConvert.ToString(time); }
            set { time = XmlConvert.ToTimeSpan(value); }
        }

    }
}
