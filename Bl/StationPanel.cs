using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DalApi;
using BO;
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

        private readonly IDal dal = DalFactory.GetDal();
        private int codeStop = -1;
        private static readonly Random myRandom = new Random();
        private static double GetDoubleRandom(double min, double max)
        {
            return (myRandom.NextDouble() * (max - min)) + min;
        }
        private event Action<BO.LineTiming> tripObserver;
        internal int CodeStop{ set => codeStop = value; get =>codeStop; }
        internal event Action<BO.LineTiming> TripObserver
        {
            add { tripObserver = value; }
            remove { tripObserver = null; }
        }

        internal void Start() => new Thread(()=>
        {
            var trips = from tripOnLine in BlImp.Instance.getAllTripsOnLines()
                        orderby tripOnLine.Time
                        select tripOnLine;
            while(WatchSimulator.Instance.Cancel==false)
            {
                foreach (var exitTime  in trips)
                {
                    if (WatchSimulator.Instance.Cancel)
                        break;
                    TimeSpan curTime = WatchSimulator.Instance.Simulator.CurTime;
                    if (curTime > exitTime.Time)
                        continue;
                    Thread.Sleep((int)(exitTime.Time - curTime).TotalMilliseconds / WatchSimulator.Instance.Speed);
                    if (WatchSimulator.Instance.Cancel)
                        break;
                    new Thread(driveThread).Start(exitTime);

                }
                Thread.Sleep(1000); 
            }
        }).Start();

        private void driveThread(object exitLine)
        {
            TripOnLine exitTripLine = (TripOnLine)exitLine;
            int codeStop = CodeStop;
            LineTiming lineTiming = new LineTiming
            {
                IdLine = exitTripLine.IdLine,
                NumLine = dal.GetLine(exitTripLine.IdLine).NumLine,
                StartTime=exitTripLine.Time
            };
            //setter name fot local thread
            Thread.CurrentThread.Name = $"idline:{lineTiming.IdLine}, numLine:{lineTiming.NumLine}, startTime:{lineTiming.StartTime.ToString(@"hh\:mm\:ss")}";
            

            var stopsInLineDO = dal.GetStopLinesBy(sl => sl.IdLine == lineTiming.IdLine).ToList();

            var stopsInLineBO = (from stopLine in stopsInLineDO
                           select stopLine.CopyPropertiesToNew<StopLine, DO.StopLine>()).ToList();
            stopsInLineBO.ForEach(sl => sl.Name = dal.GetBusStop(sl.CodeStop).Name);
            //setter LastStopName of lineTiming
            lineTiming.LastStopName = stopsInLineBO.Last().Name;

            for (int i = 0; i < stopsInLineBO.Count-1; ++i)//no need to distance
                stopsInLineBO[i].AvregeDriveTimeToNext = dal.GetConsecutiveStops(stopsInLineBO[i].CodeStop, stopsInLineBO[i+1].CodeStop).AvregeDriveTime;
            
            for (int i = 0; i < stopsInLineBO.Count; ++i)
            {
                if (codeStop != CodeStop)
                {
                    lineTiming.ArriveTime = TimeSpan.Zero;
                    if (tripObserver == null) return;
                    tripObserver(lineTiming);//arrive to station 
                    lineTiming = new LineTiming
                    {
                        IdLine = lineTiming.IdLine,
                        LastStopName = lineTiming.LastStopName,
                        NumLine = lineTiming.NumLine,
                        StartTime = lineTiming.StartTime
                    };
                    codeStop = CodeStop;
                }
                if (WatchSimulator.Instance.Cancel) break;
                if (CodeStop == stopsInLineBO[i].CodeStop)
                { 
                    lineTiming.ArriveTime = TimeSpan.Zero;
                    if (tripObserver == null) return;
                    tripObserver(lineTiming); //arrive to station
                }
                TimeSpan sum = TimeSpan.Zero;
                for (int j = i + 1; j < stopsInLineBO.Count; ++j)
                {
                    sum += stopsInLineBO[j].AvregeDriveTimeToNext;
                    if (CodeStop == stopsInLineBO[j].CodeStop)
                    {
                        lineTiming.ArriveTime = sum;
                        if (tripObserver == null) return;
                        tripObserver(lineTiming);
                        break;
                    }
                }
                if (i + 1 < stopsInLineBO.Count)
                    Thread.Sleep((int)(stopsInLineBO[i + 1].AvregeDriveTimeToNext.TotalMilliseconds * (0.9 + GetDoubleRandom(0,2.1)) / WatchSimulator.Instance.Speed));
            }

        }
    }
}
