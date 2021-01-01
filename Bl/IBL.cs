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
        #region Bus
        IEnumerable<Bus> GetBuses();
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
        Line ChangeStopLine(int idLine, int codeStop1, int codeStop2, int index1, int index2);
        Line AddStopLine(int idLine ,int codeStop,int index);
        Line DeleteStopLine(int idLine ,int codeStop,int index);
        #endregion
        #region BusStop
        IEnumerable<Line> GetLinesInStop(int code);
        IEnumerable<BusStop> GetBusStops();
        string GetNameStop(int code);
        BusStop UpdateName(int code, string name);
        #endregion

        #region Driver
        IEnumerable<Driver> GetDrivers();
        #endregion
    }
}
