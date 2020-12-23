using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalApi
{
    public interface IDal
    {

        #region Bus
        DO.Bus GetBus(int id);
        /// <summary>
        /// return all buses in Data Source
        /// </summary>
        /// <returns>a all buses in Data Source</returns>
        IEnumerable<DO.Bus> GetBuses();
        IEnumerable<DO.Bus> GetBusesBy(Predicate<DO.Bus> predicate);
        void CreateBus(DO.Bus bus);
        void DeleteBus(int id);
        void UpdateBus(DO.Bus newBus);
        #endregion

        #region BusStop
        DO.BusStop GetBusStop(int code);
        void updateBusStop(DO.BusStop busStop);
        void deleteBusStop(int code);
        #endregion

        #region Driver
        void addDriver(DO.Driver driver);
        IEnumerable<DO.Driver> GetDrivers();
        IEnumerable<DO.Driver> GetDriversBy(Predicate<DO.Driver> predicate);
        DO.Driver GetDriver(int id);
        void UpdateDriver(DO.Driver newDriver);
        void DeleteDriver(int id);
        #endregion

        #region User
        DO.User GetUser(string userName);
        void deleteUser(string phone, DateTime dateTime);
        void addUser(DO.User user);
        void updateUser(DO.User user);
        IEnumerable<DO.User> GetUsers();
        IEnumerable<DO.User> GetUsersBy(Predicate<DO.User> predicate);
        #endregion

        #region Line
        int createLine();
        DO.Line GetLine(int idLine);
        IEnumerable<DO.Line> GetLines();
        IEnumerable<DO.Line> GetLinesBy(Predicate<DO.Line> predicate);
        void updateLine(DO.Line line);
        void deleteLine(int idLine);
        #endregion

        #region StopLine
        DO.StopLine GetStopLine(int idLine,int codeStop);
        IEnumerable<DO.StopLine> GetStopLines();
        IEnumerable<DO.StopLine> GetStopLinesBy(Predicate<DO.StopLine> predicate);
        void updateStopLine(DO.StopLine stopLine);
        void deleteStopLine(int idLine, int codeStop);
        #endregion

        #region ConsecutiveStops
        DO.ConsecutiveStops GetConsecutiveStops(int codeStop1, int codeStop2);
        IEnumerable<DO.ConsecutiveStops> GetConsecutiveStops();
        IEnumerable<DO.ConsecutiveStops> GetConsecutiveStopsBy(Predicate<DO.ConsecutiveStops> predicate);
        void updateConsecutiveStops(DO.ConsecutiveStops consecutiveStops);
        #endregion

        #region LineTrip
        int createLineTrip();
        DO.LineTrip GetLineTrip(int idLine, TimeSpan startTime);
        IEnumerable<DO.LineTrip> GetLineTrips();
        IEnumerable<DO.LineTrip> GetLineTripsBy(Predicate<DO.LineTrip> predicate);
        void updateLine(DO.LineTrip lineTrip);
        void deleteLineTrip(int idLine, TimeSpan startTime);
        #endregion
    }
}
