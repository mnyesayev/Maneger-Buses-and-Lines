using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IBL
    {
        #region Simulator
        void StartSimulator(TimeSpan startTime, int speed ,Action<TimeSpan> updateTime);
        void StopSimulator();
        #endregion
        #region Bus
        IEnumerable<Bus> GetBuses();
        void DeleteBus(int id);
        Bus AddBus(Bus bus);
        Bus AddBus(int id, DateTime dra,Action<Bus> action);
        Bus Care();
        Bus Fuel();
        /// <summary>
        /// Shows whether a bus can travel with the fuel it has
        /// </summary>
        ///<param name="id">key ID of Bus</param>
        ///<param name="distance">a distance of route</param>
        /// <returns>false if fuel enough to current drive</returns>
        bool CheckFuel(int id,int distance);
        /// <summary>
        /// The func. checks if the bus has passed a 20 thousand kilometers 
        /// by "addMileage"
        /// </summary>
        ///<param name="id">key ID of Bus</param>
        ///<param name="distance">a distance of route</param>
        /// <returns>false if the bus not need a care</returns>
        bool CheckCare(int id,int distance);
        #endregion

        #region User
        User GetUser(string userName, string password);
        void AddUser(User user);
        string RecoverPassword(string phone, DateTime birthday);
        void DeleteUser(string userName);
        #endregion
        void InsertDistanceAndTime(int code1,int code2,double distance,TimeSpan time);
        #region Line
        IEnumerable<Line> GetLines();
        bool DeleteLine(int idLine);
        bool UpdateLine(int idLine ,string numLine,Areas area,Agency agency);
        Line AddLine(string numLine, Areas area,IEnumerable<StopLine> stops,string moreInfo);
        #endregion
        #region StopLine
        IEnumerable<StopLine> GetStopsInLine(int id);
        StopLine GetStopInLine(int code, int idLine);
        Line AddStopLine(int idLine ,int codeStop,int index);
        Line DeleteStopLine(int idLine ,int codeStop,int index);
        double GetDistance(int code1, int code2);
        TimeSpan GetTime(int code1, int code2);
        #endregion
        #region BusStop
        IEnumerable<LineOnStop> GetLinesInStop(int code);
        IEnumerable<BusStop> GetBusStops();
        BusStop GetStop(int code);
        BusStop AddStop(BusStop stop);
        string GetNameStop(int code);
        BusStop UpdateName(int code, string name);
        void DeleteBusStop(int code);
        #endregion

        #region Driver
        IEnumerable<Driver> GetDrivers();
        Driver AddDriver(int id, string name, int Seniority);
        void DeleteDriver(int id);
        void EditDriver(int id,string name,int seniority);
        #endregion

        #region LineTrip
        IEnumerable<LineTrip> GetLineTrips();
        string GetNumLine(int idLine);
        LineTrip AddLineTrip(int idLine, TimeSpan start, TimeSpan end,int f);
        LineTrip UpdateLineSchedule(int idLine ,TimeSpan startTime, TimeSpan endTime, int f);
        #endregion
    }
}
