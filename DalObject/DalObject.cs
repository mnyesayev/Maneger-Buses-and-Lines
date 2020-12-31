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
            int index = DataSource.Buses.FindIndex((Bus) => { return Bus.Id == Bus.Id; });
            if (index == -1)
            {
                DataSource.Buses.Add(bus);
                return;
            }
            else if (DataSource.Buses[index].Active == true)
            {
                throw new BusExceptionDO((int)bus.Id, "the bus is already exists");
            }
            else
                DataSource.Buses[index] = bus;

        }

        public void DeleteBus(int id)
        {
            int index = DataSource.Buses.FindIndex((Bus) => { return Bus.Active && Bus.Id == id; });
            if (index == -1)
                throw new BusExceptionDO(id, "the bus is not exists");
            DataSource.Buses[index].Active = false;
        }

        public void UpdateBus(Bus newBus)
        {
            int index = DataSource.Buses.FindIndex((Bus) => { return Bus.Active && Bus.Id == newBus.Id; });
            if (index == -1)
                throw new BusExceptionDO((int)newBus.Id, "system not found the bus");
            DataSource.Buses[index] = newBus;
        }

        public Bus GetBus(int id)
        {
            var bus = DataSource.Buses.Find((Bus) => { return Bus.Active && Bus.Id == id; });
            if (bus == null)
                return null;
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
                   where predicate(Bus)&&Bus.Active == true
                   select Bus.Clone();
        }

        #endregion

        #region User
        public User GetUser(string userName)
        {
            var user = DataSource.Users.Find((User) => { return User.Active && User.UserName == userName; });
            if (user == null)
                return null;
            return user.Clone();
        }
        public User GetUser(string phone, DateTime dateTime)
        {
            var user = DataSource.Users.Find((User) =>
            { return User.Active && User.Phone == phone && User.Birthday == dateTime; });
            if (user == null)
                return null;
            return user.Clone();
        }
        public void AddUser(User user)
        {
            int index = DataSource.Users.FindIndex((User) => { return User.UserName == user.UserName; });
            if (index == -1)
            {
                DataSource.Users.Add(user);
                return;
            }
            if (DataSource.Users[index].Active == true)
                throw new UserExceptionDO(user.UserName, "the User is already exists");
        }
        public void DeleteUser(string phone, DateTime dateTime)
        {
            int index = DataSource.Users.FindIndex((User) => { return (User.Active && User.Phone == phone && User.Birthday == dateTime); });
            if (index == -1)
            {
                throw new UserExceptionDO(phone, "the user is not exists");
            }
            DataSource.Users[index].Active = false;
        }
        public void UpdateUser(User user)
        {
            int index = DataSource.Users.FindIndex((User) => { return User.Active && User.UserName == user.UserName; });
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
                   where predicate(user)&&user.Active == true
                   select user.Clone();
        }
        #endregion

        #region BusStop
        public IEnumerable<BusStop> GetStops()
        {
            return from Stop in DataSource.BusStops
                   where Stop.Active == true
                   select Stop.Clone();
        }

        public IEnumerable<BusStop> GetStopsBy(Predicate<BusStop> predicate)
        {
            return from Stop in DataSource.BusStops
                   where Stop.Active == true&&predicate(Stop)
                   select Stop.Clone();
        }
        public BusStop GetBusStop(int code)
        {
            var busStop = DataSource.BusStops.Find((BusStop) => { return BusStop.Active && BusStop.Code == code; });
            if (busStop == null)
                return null;
            return busStop.Clone();
        }
        public void UpdateBusStop(int code, Action<BusStop> action)
        {
            int index = DataSource.BusStops.FindIndex((BusStop) => { return BusStop.Active && BusStop.Code == code; });
            if (index == -1)
                throw new BusStopExceptionDO(code, "system not found the busStop");
            action(DataSource.BusStops[index]);
        }
        public void UpdateBusStop(BusStop busStop)
        {
            int index = DataSource.BusStops.FindIndex((BusStop) => { return BusStop.Active && BusStop.Code == busStop.Code; });
            if (index == -1)
                throw new BusStopExceptionDO(busStop.Code, "system not found the busStop");
            DataSource.BusStops[index] = busStop;
        }

        public void DeleteBusStop(int code)
        {
            int index = DataSource.BusStops.FindIndex((BusStop) => { return BusStop.Active && BusStop.Code == code; });
            if (index == -1)
                throw new BusStopExceptionDO(code, "the busStop is not exists");
            DataSource.BusStops[index].Active = false;
        }
        #endregion

        #region Driver
        public void AddDriver(DO.Driver driver)
        {
            int index = DataSource.Drivers.FindIndex((Driver) => { return Driver.Id == driver.Id; });
            if (index == -1)
                DataSource.Drivers.Add(driver);
            if (DataSource.Drivers[index].Active == true)
                throw new DriverExceptionDO(driver.Id, "the driver is already exists");
            DataSource.Drivers[index] = driver;
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
                   where predicate(Driver) && Driver.Active == true
                   select Driver.Clone();
        }

        public Driver GetDriver(int id)
        {
            var driver = DataSource.Drivers.Find((Driver) => { return Driver.Active && Driver.Id == id; });
            if (driver == null)
                return null;
            return driver.Clone();
        }

        public void UpdateDriver(Driver newDriver)
        {
            int index = DataSource.Drivers.FindIndex((Driver) => { return Driver.Active && Driver.Id == newDriver.Id; });
            if (index == -1)
                throw new DriverExceptionDO(newDriver.Id, "system not found the driver");
            DataSource.Drivers[index] = newDriver;
        }

        public void DeleteDriver(int id)
        {
            int index = DataSource.Drivers.FindIndex((Driver) => { return Driver.Active && Driver.Id == id; });
            if (index == -1)
                throw new DriverExceptionDO(id, "the driver is not exists");
            DataSource.Drivers[index].Active = false;
        }
        #endregion

        #region Line
        public int CreateLine(DO.Line line)
        {
            line.IdLine = Config.LineCounter;
            DataSource.Lines.Add(line);
            return DataSource.Lines[DataSource.Lines.Count - 1].IdLine;
        }
        public DO.Line GetLine(int idLine)
        {
            var line = DataSource.Lines.Find((Line) => { return Line.Active && Line.IdLine == idLine; });
            if (line == null)
                return null;
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
                   where predicate(Line) && Line.Active == true
                   select Line.Clone();
        }
        public void UpdateLine(DO.Line line)
        {
            int index = DataSource.Lines.FindIndex((Line) => { return Line.Active && Line.IdLine == line.IdLine; });
            if (index == -1)
                throw new LineExceptionDO(line.IdLine, "system not found the line");
            DataSource.Lines[index] = line;

        }
        public void UpdateLine(int idLine, Action<Line> action)
        {
            int index = DataSource.Lines.FindIndex((Line) => { return Line.Active && Line.IdLine == idLine; });
            if (index == -1)
                throw new LineExceptionDO(idLine, "system not found the line");
            action(DataSource.Lines[index]);  
        }
        public void DeleteLine(int idLine)
        {
            int index = DataSource.Lines.FindIndex((Line) => { return Line.Active && Line.IdLine == idLine; });
            if (index == -1)
                throw new LineExceptionDO(idLine, "the line is not exists");
            DataSource.Lines[index].Active = false;
        }
        #endregion

        #region StopLine
        public void AddStopLine(StopLine stopLine)
        {
            var index = DataSource.StopLines.FindIndex((StopLine) =>
            {
                return StopLine.IdLine == stopLine.IdLine && StopLine.CodeStop == stopLine.CodeStop;
            });
            if (index == -1)
            {
                DataSource.StopLines.Add(stopLine);
                return;
            }
            throw new StopLineExceptionDO(stopLine.IdLine, stopLine.CodeStop, "the stop Line is already exists");
        }
        public StopLine GetStopLine(int idLine, int codeStop)
        {
            var stopLine = DataSource.StopLines.Find((StopLine) =>
                            { return StopLine.IdLine == idLine && StopLine.CodeStop == codeStop; });
            if (stopLine == null)
                return null;
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
                   orderby StopLine.NumStopInLine
                   select StopLine.Clone();
        }
        public void UpdateStopLine(DO.StopLine stopLine)
        {
            int index = DataSource.StopLines.FindIndex((StopLine) =>
            { return StopLine.IdLine == stopLine.IdLine && StopLine.CodeStop == stopLine.CodeStop; });
            if (index == -1)
                throw new StopLineExceptionDO(stopLine.IdLine, stopLine.CodeStop, "system not found the stop line");
            DataSource.StopLines[index] = stopLine;
        }
        public void UpdateStopLine(int idLine, int codeStop, Action<StopLine> action)
        {
            int index = DataSource.StopLines.FindIndex((StopLine) =>
            { return StopLine.IdLine == idLine && StopLine.CodeStop == codeStop; });
            if (index == -1)
                throw new StopLineExceptionDO(idLine, codeStop, "system not found the stop line");
            action(DataSource.StopLines[index]);
        }
        public void DeleteStopLine(int idLine, int codeStop)
        {
            int index = DataSource.StopLines.FindIndex((StopLine) =>
            { return StopLine.IdLine == idLine && StopLine.CodeStop == codeStop; });
            if (index == -1)
                throw new StopLineExceptionDO(idLine, codeStop, "the stop line is not exists");
            DataSource.StopLines.RemoveAt(index);
        }
        #endregion

        #region ConsecutiveStops
        public void AddConsecutiveStops(ConsecutiveStops consecutiveStops)
        {
            int index = DataSource.LstConsecutiveStops.FindIndex((ConsecutiveStops) =>
              {
                  return ConsecutiveStops.CodeBusStop1 == consecutiveStops.CodeBusStop1
                   && ConsecutiveStops.CodeBusStop2 == consecutiveStops.CodeBusStop2;
              });
            if (index == -1)
            {
                DataSource.LstConsecutiveStops.Add(consecutiveStops);
                return;
            }
            throw new ConsecutiveStopsExceptionDO(consecutiveStops.CodeBusStop1, consecutiveStops.CodeBusStop2, "the driver is already exists");
        }
        public ConsecutiveStops GetConsecutiveStops(int codeStop1, int codeStop2)
        {
            var conStops = DataSource.LstConsecutiveStops.Find((ConsecutiveStops) =>
            {
                return ConsecutiveStops.CodeBusStop1 == codeStop1
                && ConsecutiveStops.CodeBusStop2 == codeStop2;
            });
            if (conStops == null)
                return null;
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
        public void UpdateConsecutiveStops(ConsecutiveStops consecutiveStops)
        {
            int index = DataSource.LstConsecutiveStops.FindIndex((ConsecutiveStops) =>
            {
                return ConsecutiveStops.CodeBusStop1 == consecutiveStops.CodeBusStop1
                && ConsecutiveStops.CodeBusStop2 == consecutiveStops.CodeBusStop2;
            });
            if (index == -1)
                throw new ConsecutiveStopsExceptionDO(consecutiveStops.CodeBusStop1, consecutiveStops.CodeBusStop2, "system not found the consecutiveStops");
            DataSource.LstConsecutiveStops[index] = consecutiveStops;
        }
        #endregion

        #region LineTrip
        public int CreateLineTrip(LineTrip lineTrip)
        {
            lineTrip.Id = Config.LineTripCounter;
            DataSource.LineTrips.Add(lineTrip);
            return DataSource.LineTrips[DataSource.LineTrips.Count - 1].Id;
        }
        public LineTrip GetLineTrip(int idLine, TimeSpan startTime)
        {
            var lineTrip = DataSource.LineTrips.Find((LineTrip) =>
            {
                return LineTrip.Active && LineTrip.IdLine == idLine && LineTrip.StartTime == startTime;
            });
            if (lineTrip == null)
                return null;
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
                   where predicate(LineTrip) && LineTrip.Active == true
                   select LineTrip.Clone();
        }
        public void UpdateLineTrip(DO.LineTrip lineTrip)
        {
            int index = DataSource.LineTrips.FindIndex((LineTrip) => { return LineTrip.Active && LineTrip.Id == lineTrip.Id; });
            if (index == -1)
                throw new LineTripExceptionDO(lineTrip.Id, "system not found the line trip");
            DataSource.LineTrips[index] = lineTrip;
        }
        public void DeleteLineTrip(int idLine, TimeSpan startTime)
        {
            var index = DataSource.LineTrips.FindIndex((LineTrip) =>
            {
                return LineTrip.Active && LineTrip.IdLine == idLine && LineTrip.StartTime == startTime;
            });
            if (index == -1)
                throw new LineTripExceptionDO(idLine, "the line trip is not exists");
            DataSource.LineTrips[index].Active = false;
        }

       
        #endregion
    }
}
