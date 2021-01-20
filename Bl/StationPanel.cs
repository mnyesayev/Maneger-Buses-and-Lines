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

        private int codeStop;
        private event EventHandler codeStopChanged;


        void onCodeStop(CodeStopChangedEventArgs args)
        {
            if (codeStopChanged != null)
            {
                codeStopChanged(this, args);
            }
        }

        public int CodeStop
        {
            get => codeStop;
            set
            {
                if (value != codeStop)
                {
                    CodeStopChangedEventArgs args = new CodeStopChangedEventArgs(value);
                    codeStop = value;
                    onCodeStop(args);
                }
            }
        }
  
        public event EventHandler CodeStopChanged
        {
            add { codeStopChanged = value; }
            remove { codeStopChanged -= value; }
        }
    }
    public class CodeStopChangedEventArgs : EventArgs
    {
        public readonly int NewCode;
        public CodeStopChangedEventArgs(int code)
        {
            NewCode = code;
        }
    }
}
