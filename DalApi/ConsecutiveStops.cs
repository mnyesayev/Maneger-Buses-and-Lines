using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class ConsecutiveStops
    {
        int codeStop1;
        int codeStop2;
        double distance;
        TimeSpan avregeDriveTime;



        public int CodeStop1 { get => codeStop1; set => codeStop1 = value; }
        public int CodeStop2 { get => codeStop2; set => codeStop2 = value; }
        public double Distance { get => distance; set => distance = value; }
        public TimeSpan AvregeDriveTime { get => avregeDriveTime; set => avregeDriveTime = value; }

        public ConsecutiveStops(int codeStop1, int codeStop2, double distance, TimeSpan avregeDriveTime)
        {
            CodeStop1 = codeStop1;
            CodeStop2 = codeStop2;
            Distance = distance;
            AvregeDriveTime = avregeDriveTime;
        }
    }
}
