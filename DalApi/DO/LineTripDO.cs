﻿using System;
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
    ///the class represents a line Trip (hours and frequency),in the DO layer 
    /// </summary>
    public class LineTrip
    {
        /// <summary>
        ///  Represents if this class active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Represents the unique  number of the "LineTrip"
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the unique inner number of the "Line"
        /// </summary>
        public int IdLine { get; set ; }
        /// <summary>
        /// Represents the frequency of line per hour
        /// </summary>
        public int Frequency { get; set; }
        /// <summary>
        /// Represents the time of start line
        /// </summary>
        [XmlIgnore]
        public TimeSpan StartTime { get; set; }
        /// <summary>
        /// Represents the time of end line
        /// </summary>
        [XmlIgnore]
        public TimeSpan EndTime { get; set; }
        [XmlElement("StartTime", DataType = "duration")]
        [DefaultValue("PT10M")]
        public string XmlStartTime
        {
            get { return XmlConvert.ToString(StartTime); }
            set { StartTime = XmlConvert.ToTimeSpan(value); }
        }

        [XmlElement("EndTime", DataType = "duration")]
        [DefaultValue("PT10M")]
        public string XmlEndTime
        {
            get { return XmlConvert.ToString(EndTime); }
            set { EndTime = XmlConvert.ToTimeSpan(value); }
        }
    }
}
