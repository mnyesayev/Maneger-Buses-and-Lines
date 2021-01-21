using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


        private int codeStop = -1;
        private static Random myRandom = new Random();

        private event Action<BO.LineTiming> tripObserver;
        internal int CodeStop{ set => codeStop = value; }
        internal event Action<BO.LineTiming> TripObserver
        {
            add { tripObserver = value; }
            remove { tripObserver = null; }
        }

        internal void Start() => new Thread(()=>
        {
            var curStop = BlImp.Instance.GetStop(codeStop);
            var trips = from Line in curStop.LinesPassInStop
                        let tripLines = BlImp.Instance.GetTripsOnLine(Line.IdLine)
                        from tl in tripLines
                        select tl;
            var exitTimes = trips.OrderBy(t => t.Time).ToList();
            while(WatchSimulator.Instance.Cancel==false)
            {
                foreach (var exitTime  in exitTimes)
                {
                    if (WatchSimulator.Instance.Cancel == false)
                        break;
                    TimeSpan curTime = WatchSimulator.Instance.Simulator.CurTime;
                    if (curTime > exitTime.Time)
                        continue;
                    Thread.Sleep((int)(exitTime.Time - curTime).TotalMilliseconds / WatchSimulator.Instance.Speed);
                    if (WatchSimulator.Instance.Cancel)
                        break;
                    new Thread(busTripThread).Start(exitTime);

                }
                Thread.Sleep(1000); 
            }
        }).Start();

        private void busTripThread()
        {
            throw new NotImplementedException();
        }
    }
}
