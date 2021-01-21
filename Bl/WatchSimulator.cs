using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bl
{
    sealed class WatchSimulator
    {
        #region singelton
        static readonly WatchSimulator instance = new WatchSimulator();
        static WatchSimulator() { }// static ctor to ensure instance init is done just before first usage
        WatchSimulator() { } // default => private
        public static WatchSimulator Instance { get => instance; }// The public Instance property to use
        #endregion
        
        private TimeSpan startTime;
        private volatile int speed;
        private volatile Watch simulator = null;

        //Inner class to ensure proper time-reading and writing
        internal class Watch
        {
            internal TimeSpan CurTime;
            internal Watch(TimeSpan time) => CurTime = time;
        }

        //variable that indicate if watch canceled
        internal volatile bool Cancel;
        internal Watch Simulator { get => simulator; private set => simulator = value; }
        internal int Speed { get => speed; private set => speed = value; }

        private Stopwatch stopwatch = new Stopwatch();
        private event Action<TimeSpan> watchObserver = null;

        internal event Action<TimeSpan> WatchObserver
        {
            add { watchObserver = value; }
            remove { watchObserver = null; }
        }
        internal void StartWatch(TimeSpan startTime, int speed)
        {
            this.startTime = startTime;
            Simulator= new Watch(startTime);
            Speed = speed;
            Cancel = false;
            stopwatch.Restart();
            new Thread(()=> 
            {
                while (!Cancel)
                {
                    var watch = new Watch(startTime + new TimeSpan(stopwatch.ElapsedTicks * Speed));
                    watchObserver(new TimeSpan(watch.CurTime.Hours, watch.CurTime.Minutes, watch.CurTime.Seconds));
                    Thread.Sleep(100);
                }
                watchObserver = null;
            }).Start();
            StationPanel.Instance.Start();
        }

    }
   
}
