using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    sealed class StationPanel
    {
        #region singelton
        static readonly StationPanel instance = new StationPanel();
        static StationPanel() { }// static ctor to ensure instance init is done just before first usage
        StationPanel() { } // default => private
        public static StationPanel Instance { get => instance; }// The public Instance property to use
        #endregion

        private TimeSpan arriveTime;
        private event EventHandler arriveTimeChanged;


        void onArriveTime(TimeChangedEventArgs args)
        {
            if (arriveTime != null)
            {
                arriveTimeChanged(this, args);
            }
        }

        public TimeSpan ArriveTime
        {
            get => arriveTime;
            set
            {
                if (value != arriveTime)
                {
                    TimeChangedEventArgs args = new TimeChangedEventArgs(value);
                    arriveTime = value;
                    onArriveTime(args);
                }
            }
        }
  
        public event EventHandler ArriveTimeChanged
        {
            add { arriveTimeChanged = value; }
            remove { arriveTimeChanged -= value; }
        }
    }
    public class ArriveTimeChangedEventArgs : EventArgs
    {
        public readonly TimeSpan NewTime;
        public ArriveTimeChangedEventArgs(TimeSpan time)
        {
            NewTime = time;
        }
    }
}
