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

        #endregion

        #region Driver
        IEnumerable<DO.Driver> GetDrivers();
        IEnumerable<DO.Driver> GetDriversBy(Predicate<DO.Driver> predicate);
        DO.Driver GetDriver();
        void UpdateDriver(DO.Driver newDriver);
        void DeleteDriver(int id);
        #endregion

        #region User
        DO.User GetUser(string userName);
        void deleteUser(string phone, DateTime dateTime);
        void addUser(DO.User user);
        void updateUser(DO.User user);
        #endregion


    }
}
