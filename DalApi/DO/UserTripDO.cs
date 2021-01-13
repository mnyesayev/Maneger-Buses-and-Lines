using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
namespace DO
{
    /// <summary>
    ///The class displays a user's trip,in the DO layer. 
    /// </summary>
    public class UserTripDO
    {
        /// <summary>
        ///  Represents if this class active
        /// </summary>
        public int IdDriveGo { get; set; }
        public string UserName { get; set; }
        public int IdLine { get; set; }
        public int CodeStopUp { get; set; }
        public int CodeStopDown { get; set; }
        public TimeSpan TimeUp { get; set; }
        public TimeSpan TimeDown { get; set; }
    }
}
