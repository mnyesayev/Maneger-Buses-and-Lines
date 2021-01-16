using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    sealed class Watch
    {
        #region singelton
        static readonly Watch instance = new Watch();
        static Watch() { }// static ctor to ensure instance init is done just before first usage
        Watch() { } // default => private
        public static Watch Instance { get => instance; }// The public Instance property to use
        #endregion

        internal volatile bool Cancel;
        private TimeSpan curTime;
        private event EventHandler timeChanged;
        void onTimeChanged(TimeChangedEventArgs args)
        {
            if (timeChanged != null)
            {
                timeChanged(this, args);
            }
        }
        public TimeSpan CurTime
        {
            get => curTime;
            set
            {
                if (value != curTime)
                {
                    TimeChangedEventArgs args = new TimeChangedEventArgs(value);
                    curTime = value;
                    onTimeChanged(args);
                }
            }
        }
        public event EventHandler TimeChanged
        {
            add { timeChanged = value; }
            remove { timeChanged -= value; }
        }
    }
    public class TimeChangedEventArgs : EventArgs
    {
        public readonly TimeSpan NewTime;
        public TimeChangedEventArgs(TimeSpan time)
        {
            NewTime = time;
        }
    }
}
