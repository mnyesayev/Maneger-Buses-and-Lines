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
            if (index == -1)
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
            int index = DataSource.Users.FindIndex((User) => { return (User.Phone == phone && User.Birthday == dateTime); });
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
            if (index == -1)
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
                throw new BusStopExceptionDO(code, "BusStop is already not Active");
            DataSource.BusStops[index].Active = false;
        }
        #endregion

        #region Driver
        public void addDriver(DO.Driver driver)
        {
            DataSource.Drivers.Add(driver);
        }
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

            if (driver.Id != id)
                throw new BusStopExceptionDO(id, "system not found the driver");
            else if (driver.Active == false)
                throw new BusStopExceptionDO(id, "Driver is not Active");
            return driver.Clone();
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
            if (index != -1)
                throw new DriverExceptionDO(id, "system not found the driver");
            else if (DataSource.Drivers[index].Active == false)
                throw new DriverExceptionDO(id, "Driver is already not Active");

            DataSource.Drivers[index].Active = false;
        }
        #endregion

        #region Line
        public int createLine()
        {
            DataSource.Lines.Add(new Line() { IdLine = Config.LineCounter, Active = false });
            return DataSource.Lines[DataSource.Lines.Count - 1].IdLine;
        }
        public DO.Line GetLine(int idLine)
        {
            var line = DataSource.Lines.Find((Line) => { return Line.IdLine == idLine; });
            if (line.IdLine != idLine)
                throw new LineExceptionDO(idLine, "system not found the line");
            else if (line.Active == false)
                throw new LineExceptionDO(idLine, "line is not Active");
            return line.Clone();

        }
        public IEnumerable<DO.Line> GetLines()
        {
            return from Line in DataSource.Lines
                   where Line.Active == true
                   select Line.Clone();
        }
        public IEnumerable<DO.Line> GetLinesBy(Predicate<DO.Line> predicate)
        {
            return from Line in DataSource.Lines
                   where predicate(Line)
                   where Line.Active == true
                   select Line.Clone();
        }
        public void updateLine(DO.Line line)
        {
            int index = DataSource.Lines.FindIndex((Line) => { return Line.IdLine == line.IdLine; });
            if (index == -1)
                throw new LineExceptionDO(line.IdLine, "system not found the line");
            DataSource.Lines[index] = line;

        }
        public void deleteLine(int idLine)
        {
            int index = DataSource.Lines.FindIndex((Line) => { return Line.IdLine == idLine; });
            if (index == -1)
                throw new LineExceptionDO(idLine, "system not found the line");
            if (!DataSource.Lines[index].Active)
                throw new LineExceptionDO(idLine, "line is already not Active");
        }
        #endregion

        #region StopLine
        public DO.StopLine GetStopLine(int idLine, int codeStop)
        {
            var stopLine = DataSource.StopLines.Find((StopLine) =>
                            { return StopLine.IdLine == idLine && StopLine.CodeStop == codeStop; });
            if (stopLine.CodeStop != codeStop)
                throw new StopLineExceptionDO(idLine, codeStop, "system not found the line");
            return stopLine.Clone();
        }
        public IEnumerable<DO.StopLine> GetStopLines()
        {
            return from StopLine in DataSource.StopLines
                   select StopLine.Clone();
        }
        public IEnumerable<DO.StopLine> GetStopLinesBy(Predicate<DO.StopLine> predicate)
        {
            return from StopLine in DataSource.StopLines
                   where predicate(StopLine)
                   select StopLine.Clone();
        }
        public void updateStopLine(DO.StopLine stopLine)
        {
            int index = DataSource.StopLines.FindIndex((StopLine) =>
            { return StopLine.IdLine == stopLine.IdLine && StopLine.CodeStop == stopLine.CodeStop; });
            if (index == -1)
                throw new StopLineExceptionDO(stopLine.IdLine, stopLine.CodeStop, "system not found the line");
            DataSource.StopLines[index] = stopLine;
        }
        public void deleteStopLine(int idLine, int codeStop)
        {
            int index = DataSource.StopLines.FindIndex((StopLine) =>
            { return StopLine.IdLine == idLine && StopLine.CodeStop == codeStop; });
            if (index == -1)
                throw new StopLineExceptionDO(idLine, codeStop, "system not found the line");
            DataSource.StopLines.RemoveAt(index);
        }
        #endregion

        #region ConsecutiveStops
        public DO.ConsecutiveStops GetConsecutiveStops(int codeStop1, int codeStop2)
        {
            var conStops = DataSource.LstConsecutiveStops.Find((ConsecutiveStops) =>
            {
                return ConsecutiveStops.CodeBusStop1 == codeStop1
                && ConsecutiveStops.CodeBusStop2 == codeStop2;
            });
            if (conStops.CodeBusStop1 != codeStop1)
                throw new ConsecutiveStopsExceptionDO(codeStop1, codeStop2, "system not found these stops");
            return conStops.Clone();
        }
        public IEnumerable<DO.ConsecutiveStops> GetConsecutiveStops()
        {
            return from ConsecutiveStops in DataSource.LstConsecutiveStops
                   select ConsecutiveStops.Clone();
        }
        public IEnumerable<DO.ConsecutiveStops> GetConsecutiveStopsBy(Predicate<DO.ConsecutiveStops> predicate)
        {
            return from ConsecutiveStops in DataSource.LstConsecutiveStops
                   where predicate(ConsecutiveStops)
                   select ConsecutiveStops.Clone();
        }
        public void updateConsecutiveStops(ConsecutiveStops consecutiveStops)
        {
            int index = DataSource.LstConsecutiveStops.FindIndex((ConsecutiveStops) =>
            {
                return ConsecutiveStops.CodeBusStop1 == consecutiveStops.CodeBusStop1
                && ConsecutiveStops.CodeBusStop2 == consecutiveStops.CodeBusStop2;
            });
            if (index == -1)
                throw new ConsecutiveStopsExceptionDO(consecutiveStops.CodeBusStop1, consecutiveStops.CodeBusStop2);
        }
        #endregion

        #region LineTrip
        public int createLineTrip()
        {
            DataSource.LineTrips.Add(new LineTrip() { Active = false, Id = Config.LineTripCounter });
            return DataSource.LineTrips[DataSource.LineTrips.Count - 1].Id;
        }
        public DO.LineTrip GetLineTrip(int idLine, TimeSpan startTime)
        {
            var lineTrip = DataSource.LineTrips.Find((LineTrip) =>
            {
                return LineTrip.IdLine == idLine && LineTrip.StartTime == startTime;
            });
            if (lineTrip.IdLine != idLine)
                throw new LineTripExceptionDO(idLine, "system not found the lineTrip");
            if (lineTrip.Active == false)
                throw new LineTripExceptionDO(idLine, "lineTrip is not Active");
            return lineTrip.Clone();
        }
        public IEnumerable<DO.LineTrip> GetLineTrips()
        {
            return from LineTrip in DataSource.LineTrips
                   where LineTrip.Active == true
                   select LineTrip.Clone();
        }
        public IEnumerable<DO.LineTrip> GetLineTripsBy(Predicate<DO.LineTrip> predicate)
        {
            return from LineTrip in DataSource.LineTrips
                   where predicate(LineTrip)
                   where LineTrip.Active == true
                   select LineTrip.Clone();
        }
        public void updateLine(DO.LineTrip lineTrip)
        {
            int index = DataSource.LineTrips.FindIndex((LineTrip) => { return LineTrip.Id == lineTrip.Id; });
            if (index == -1)
                throw new LineTripExceptionDO(lineTrip.Id, "system not found the line");
            DataSource.LineTrips[index] = lineTrip;
        }
        public void deleteLineTrip(int idLine, TimeSpan startTime)
        {
            var index = DataSource.LineTrips.FindIndex((LineTrip) =>
            {
                return LineTrip.IdLine == idLine && LineTrip.StartTime == startTime;
            });
            if (index == -1)
                throw new LineTripExceptionDO(idLine, "system not found the lineTrip");
            if (DataSource.LineTrips[index].Active == false)
                throw new LineTripExceptionDO(idLine, "lineTrip is not Active");
            DataSource.LineTrips[index].Active = false;
        }
        #endregion
    }
}
