﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DALApi
{
    public interface IDAL
    {

        #region Bus
        DO.Bus GetBus(int id);
        /// <summary>
        /// return all buses in Data Source
        /// </summary>
        /// <returns>a all buses in Data Source</returns>
        IEnumerable<DO.Bus> GetBuses();
        IEnumerable<DO.Bus> GetBusesBy(Predicate<DO.Bus> predicate);
        void AddBus(DO.Bus bus);
        void DeleteBus(int id);
        void UpdateBus(DO.Bus newBus);
        #endregion

        #region BusStop
        DO.BusStop GetBusStop(int code);
        IEnumerable<DO.BusStop> GetStops();
        IEnumerable<DO.BusStop> GetStopsBy(Predicate<DO.BusStop> predicate);
        void AddStop(DO.BusStop stop);
        void UpdateBusStop(DO.BusStop busStop);
        void UpdateBusStop(int code, Action<DO.BusStop> action);
        void DeleteBusStop(int code);
        #endregion

        #region Driver
        void AddDriver(DO.Driver driver);
        IEnumerable<DO.Driver> GetDrivers();
        IEnumerable<DO.Driver> GetDriversBy(Predicate<DO.Driver> predicate);
        DO.Driver GetDriver(int id);
        void UpdateDriver(DO.Driver newDriver);
        void UpdateDriver(int id,Action<DO.Driver> action);
        void DeleteDriver(int id);
        #endregion

        #region User
        DO.User GetUser(string userName);
        DO.User GetUser(string phone, DateTime dateTime);
        void DeleteUser(string phone, DateTime dateTime);
        void AddUser(DO.User user);
        void UpdateUser(DO.User user);
        IEnumerable<DO.User> GetUsers();
        IEnumerable<DO.User> GetUsersBy(Predicate<DO.User> predicate);
        #endregion

        #region Line
        int CreateLine(DO.Line line);
        DO.Line GetLine(int idLine);
        IEnumerable<DO.Line> GetLines();
        IEnumerable<DO.Line> GetLinesBy(Predicate<DO.Line> predicate);
        void UpdateLine(DO.Line line);
        void UpdateLine(int idLine,Action<DO.Line> action);
        void DeleteLine(int idLine);
        #endregion

        #region StopLine
        void AddStopLine(DO.StopLine stopLine);
        void AddRouteStops(IEnumerable<DO.StopLine> stops);
        DO.StopLine GetStopLine(int idLine,int codeStop);
        DO.StopLine GetStopLineByIndex(int idLine,int index);
        IEnumerable<DO.StopLine> GetStopLines();
        IEnumerable<DO.StopLine> GetStopLinesBy(Predicate<DO.StopLine> predicate);
        void UpdateStopLine(DO.StopLine stopLine);
        void UpdateStopLine(int idLine,int codeStop,Action<DO.StopLine> action);
        void DeleteStopLine(int idLine, int codeStop);
        void DeleteAllStopsInLine(int idLine);
        #endregion

        #region ConsecutiveStops
        DO.ConsecutiveStops GetConsecutiveStops(int codeStop1, int codeStop2);
        void AddConsecutiveStops(DO.ConsecutiveStops consecutiveStops);
        void UpdateConsecutiveStops(DO.ConsecutiveStops consecutiveStops);
        void UpdateConsecutiveStops(int codeStop1, int codeStop2,Action<DO.ConsecutiveStops> action);
        #endregion

        #region LineTrip
        int CreateLineTrip(DO.LineTrip lineTrip);
        DO.LineTrip GetLineTrip(int id);
        bool IsExitsLineTrip(int idLine, TimeSpan start);
        IEnumerable<DO.LineTrip> GetLineTrips();
        IEnumerable<DO.LineTrip> GetLineTripsBy(Predicate<DO.LineTrip> predicate);
        void UpdateLineTrip(DO.LineTrip lineTrip);
        void UpdateLineTrip(int id,Action<DO.LineTrip> action);
        void DeleteLineTrip(int id);
        #endregion
    }
}
