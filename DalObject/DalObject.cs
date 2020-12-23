using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DS;
using DO;

namespace Dal
{
    sealed class DalObject : IDal
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }// static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; }// The public Instance property to use


        #endregion

        #region Bus
        public void CreateBus(Bus bus)
        {
            DataSource.Buses.Add(bus);
        }

        public void DeleteBus(int id)
        {
            int index = DataSource.Buses.FindIndex((Bus) => { return Bus.Id == id; });
            if (index == -1)
                throw new BusExceptionDO(id, "system not found the bus");
            if (DataSource.Buses[index].Active == false)
                throw new BusExceptionDO(id, "the bus is already not Active");
            DataSource.Buses[index].Active = false;
        }

        public void UpdateBus(Bus newBus)
        {
            int index = DataSource.Buses.FindIndex((Bus) => { return Bus.Id == newBus.Id; });
            if(index == -1)
                throw new BusExceptionDO((int)newBus.Id, "system not found the bus");
            DataSource.Buses[index] = newBus;
        }

        public Bus GetBus(int id)
        {
            var bus = DataSource.Buses.Find((Bus) => { return Bus.Id == id; });
            if (bus.Id != id)
                throw new BusExceptionDO(id, "system not found the bus");
            if (bus.Active == false)
                throw new BusExceptionDO(id, "the bus is not Active");
            return bus.Clone();
        }

        public IEnumerable<Bus> GetBuses()
        {
            return from Bus in DataSource.Buses
                   where Bus.Active == true
                   select Bus.Clone();
        }

        public IEnumerable<Bus> GetBusesBy(Predicate<Bus> predicate)
        {
            return from Bus in DataSource.Buses
                   where predicate(Bus)
                   where Bus.Active == true
                   select Bus.Clone();
        }

        #endregion

        #region User
        public User GetUser(string userName)
        {
            var user = DataSource.Users.Find((User) => { return User.UserName == userName; });
            if (user.UserName != userName)
                throw new UserExceptionDO(userName, "system not found the userName");
            if (user.Active == false)
                throw new UserExceptionDO(userName, "userName is not Active");
            return user.Clone();
        }
        public void addUser(User user)
        {
            DataSource.Users.Add(user);
        }
        public void deleteUser(string phone, DateTime dateTime)
        {
            int index =DataSource.Users.FindIndex((User) => { return (User.Phone == phone && User.Birthday == dateTime); });
            if (index == -1)
            {
                throw new UserExceptionDO(phone, "system not found the userName");
            }
            if (DataSource.Users[index].Active == false)
                throw new UserExceptionDO(phone, "userName is already not Active");
            DataSource.Users[index].Active = false;
        }
        public void updateUser(User user)
        {
            int index = DataSource.Users.FindIndex((User) => { return User.UserName == user.UserName; });
            if(index == -1)
                throw new UserExceptionDO(user.UserName, "system not found the userName");
            DataSource.Users[index] = user;
        }
        public IEnumerable<User> GetUsers()
        {
            return from user in DataSource.Users
                   where user.Active == true
                   select user.Clone();
        }

        public IEnumerable<User> GetUsersBy(Predicate<DO.User> predicate)
        {
            return from user in DataSource.Users
                   where predicate(user)
                   where user.Active == true
                   select user.Clone();
        }
        #endregion

        #region BusStop
        public BusStop GetBusStop(int code)
        {
            var busStop = DataSource.BusStops.Find((BusStop) => { return BusStop.Code == code; });

            if (busStop.Code != code)
                throw new BusStopExceptionDO(code, "system not found the busStop");
            else if (busStop.Active == false)
                throw new BusStopExceptionDO(code, "BusStop is not Active");

            return busStop.Clone();
        }

        public void updateBusStop(BusStop busStop)
        {
            int index = DataSource.BusStops.FindIndex((BusStop) => { return BusStop.Code == busStop.Code; });
            if (index == -1)
                throw new BusStopExceptionDO(busStop.Code, "system not found the busStop");
            DataSource.BusStops[index] = busStop;
        }

        public void deleteBusStop(int code)
        {
            int index = DataSource.BusStops.FindIndex((BusStop) => { return BusStop.Code == code; });
            if (index == -1)
                throw new BusStopExceptionDO(code, "system not found the busStop");
            if (DataSource.BusStops[index].Active == false)
                throw new BusStopExceptionDO(code, "BusStop is not Active");
            DataSource.BusStops[index].Active = false;
        }
        #endregion

        #region Driver
        public IEnumerable<Driver> GetDrivers()
        {
            return from Driver in DataSource.Drivers
                   where Driver.Active == true
                   select Driver.Clone();
        }

        public IEnumerable<Driver> GetDriversBy(Predicate<Driver> predicate)
        {
            return from Driver in DataSource.Drivers
                   where predicate(Driver)
                   where Driver.Active == true
                   select Driver.Clone();
        }

        public Driver GetDriver(int id)
        {
            var driver = DataSource.Drivers.Find((Driver) => { return Driver.Id == id; });
            if (driver.Id == id && driver.Active == true)
                return driver.Clone();
            else throw new DriverExceptionDO(id, "system not found the Driver");
        }

        public void UpdateDriver(Driver newDriver)
        {
            int index = DataSource.Drivers.FindIndex((Driver) => { return Driver.Id == newDriver.Id; });
            if (index != -1)
                DataSource.Drivers[index] = newDriver;
        }

        public void DeleteDriver(int id)
        {
            int index = DataSource.Drivers.FindIndex((Driver) => { return Driver.Id == id; });
            if(index != -1)
                throw new DriverExceptionDO(id, "You cannot delete bus the not exsist");
            else if (DataSource.Drivers[index].Active == false)
                throw new DriverExceptionDO(id, "You cannot delete bus the not Active");

            DataSource.Drivers[index].Active = false;
        }
        #endregion
    }
}
