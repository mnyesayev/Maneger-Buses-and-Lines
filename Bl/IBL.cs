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
        /// <summary>
        /// Starts the watch simulator running
        /// </summary>
        /// <param name="startTime">a Current time of the watch</param>
        /// <param name="speed">a speed of watch in ms</param>
        /// <param name="updateTime">the action of observer</param>
        void StartSimulator(TimeSpan startTime, int speed ,Action<TimeSpan> updateTime);
        /// <summary>
        /// Stops the watch simulator operation
        /// </summary>
        void StopSimulator();
        /// <summary>
        /// Updates the station on tracking
        /// </summary>
        /// <param name="station">code stop</param>
        /// <param name="updateBus">the action of observer</param>
        void SetStationPanel(int station, Action<LineTiming> updateBus=null);
        #endregion

        #region Bus
        /// <summary>
        /// return list of all buses 
        /// </summary>
        /// <returns>a IEnumerable&lt;Bus&gt;</returns>
        IEnumerable<Bus> GetBuses();
        /// <summary>
        /// <list type="table">
        /// <item>Deletes a bus according to its Id </item>
        /// <item><strong>Exceptions:</strong></item>
        /// <item><strong>DeleteException</strong> -thrown when bus does not exist.</item>
        /// </list>
        /// </summary>
        /// <param name="id">a Bus license number</param>
        void DeleteBus(int id);
        /// <summary>
        /// <list type="table">
        /// <item>adds bus to data source </item>
        /// <item><strong>Exceptions:</strong></item>
        /// <item><strong>AddException</strong> -thrown when bus is already exist.</item>
        /// </list>
        /// </summary>
        /// <param name="bus">bus to add to data source</param>
        /// <param name="isNew">A Boolean variable indicating whether the bus is new</param>
        /// <returns></returns>
        Bus AddBus(Bus bus,bool isNew=false);
        /// <summary>
        /// <list type="table">
        /// <item><strong>Exceptions:</strong></item>
        /// <item><strong>NullReferenceException</strong> thrown when bus was null</item>
        /// </list>
        /// </summary>
        /// <param name="bus">bus to check</param>
        /// <returns>a <strong>true:</strong> when id is valid <strong>false:</strong> when is is invalid </returns>
        bool CheckIdBus(Bus bus);
        Bus Care(Bus bus);
        Bus Fuel(Bus bus);
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
        User AddUser(User user);
        string RecoverPassword(string phone, DateTime birthday);
        User UpdateUser(User user);
        void DeleteUser(string phone, DateTime birthday);
        #endregion
        void InsertDistanceAndTime(int code1,int code2,double distance,TimeSpan time);
        #region Line
        IEnumerable<Line> GetLines();
        bool DeleteLine(int idLine);
        bool UpdateLine(int idLine ,string numLine,Areas area,string MoreInfo);
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
        IEnumerable<TripOnLine> GetTripsOnLine(int idLine);
        LineTrip AddLineTrip(int idLine, TimeSpan start, TimeSpan end,int f);
        void DeleteRangeTrips(int id);
        LineTrip UpdateLineSchedule(int idLine ,TimeSpan startTime, TimeSpan endTime, int f);
        #endregion
    }
}
