using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// The class presents a bus on the go, in the DO layer
    /// </summary>
    public class GoBusDO
    {
        int idBus;
        int linceseNum;
        int idLine;
        TimeSpan startTime;
        TimeSpan realSatrtTime;
        int prevNumStop;
        TimeSpan prevTimeStop;
        TimeSpan nextTimeStop;
        int idDriver;
        string nameDriver;

        /// <summary>
        /// Represents the unique number of the "GoBus"
        /// </summary>
        public int IdBus { get => idBus; set => idBus = value; }
        /// <summary>
        /// Represents the unique number of the "Bus"
        /// </summary>
        public int LinceseNum { get => linceseNum; set => linceseNum = value; }
        /// <summary>
        /// Represents the unique number of the "Line"
        /// </summary>
        public int IdLine { get => idLine; set => idLine = value; }
        /// <summary>
        /// Represents the time of the start drive  
        /// </summary>
        public TimeSpan StartTime { get => startTime; set => startTime = value; }
        /// <summary>
        /// Represents the time of the actual start drive  
        /// </summary>
        public TimeSpan RealSatrtTime { get => realSatrtTime; set => realSatrtTime = value; }
        /// <summary>
        /// Represents the unique number of the prev stop/station
        /// </summary>
        public int PrevNumStop { get => prevNumStop; set => prevNumStop = value; }
        /// <summary>
        /// Represents the time when the bus passed in prev stop/station 
        /// </summary>
        public TimeSpan PrevTimeStop { get => prevTimeStop; set => prevTimeStop = value; }
        /// <summary>
        /// Represents the time when the bus will arrive in next stop/station 
        /// </summary>
        public TimeSpan NextTimeStop { get => nextTimeStop; set => nextTimeStop = value; }
        /// <summary>
        /// Represents the unique number of the "Driver"
        /// </summary>
        public int IdDriver { get => idDriver; set => idDriver = value; }
        /// <summary>
        /// Represents the name of the driver
        /// </summary>
        public string NameDriver { get => nameDriver; set => nameDriver = value; }
        /// <summary>
        /// Ctor of GoBusDO 
        /// </summary>
        /// <param name="idBus">a unique number of the "GoBus"</param>
        /// <param name="linceseNum">a unique number of the "Bus"</param>
        /// <param name="idLine">a unique number of the "Line"</param>
        /// <param name="startTime">a time of the start drive</param>
        /// <param name="realSatrtTime">a time of the actual start drive</param>
        /// <param name="prevNumStop">a unique number of the prev stop/station</param>
        /// <param name="prevTimeStop">a time when the bus passed in prev stop/station</param>
        /// <param name="nextTimeStop">a time when the bus will arrive in next stop/station </param>
        /// <param name="idDriver">a unique number of the "Driver"</param>
        /// <param name="nameDriver">a name of the driver</param>
        public GoBusDO(int idBus = 0, int linceseNum = 0, int idLine = 0, TimeSpan startTime = default,
        TimeSpan realSatrtTime = default, int prevNumStop = 0, TimeSpan prevTimeStop = default,
        TimeSpan nextTimeStop = default, int idDriver = 0, string nameDriver = "Without Driver")
        {
            LinceseNum = linceseNum;
            IdLine = idLine;
            StartTime = startTime;
            RealSatrtTime = realSatrtTime;
            PrevNumStop = prevNumStop;
            PrevTimeStop = prevTimeStop;
            NextTimeStop = nextTimeStop;
            IdDriver = idDriver;
            NameDriver = nameDriver;
        }
    }
}
