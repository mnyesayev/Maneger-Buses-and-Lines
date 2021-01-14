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
        event EventHandler<Watch> timeChanged;
        TimeSpan curTime;
        public event EventHandler<Watch> TimeChanged
        {
            add { timeChanged = value; }
            remove { timeChanged -= value; }
        }

    }
}
