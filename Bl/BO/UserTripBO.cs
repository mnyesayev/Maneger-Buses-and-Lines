using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class UserTrip
    {
        public string UserName { get; set; }
       
        public DateTime DateTrip { get; set; }
        public string NumLine { get; set; }
        public BusStop StopUp { get; set; }
        public BusStop StopDown { get; set; }
        public TimeSpan TimeUp { get; set; }
        public TimeSpan TimeDown { get; set; }
    }
}
