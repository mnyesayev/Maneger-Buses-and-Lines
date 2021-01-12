using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal
{
    public class DalXML : IDal
    {
        #region singelton
        static readonly DalXML instance = new DalXML();
        static DalXML() { }// static ctor to ensure instance init is done just before first usage
        DalXML() { } // default => private
        public static DalXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        string busesPath = @"BusesXml.xml"; //XElement

        string runNumbersPath = @"runingNumbersXml.xml";//XMLSerializer
        string busStopsPath = @"BusStopsXml.xml"; //XMLSerializer
        string busesOnTripPath = @"BusesOnTripXml.xml"; //XMLSerializer
        string consecutiveStopsPath = @"ConsecutiveStopsXml.xml"; //XMLSerializer
        string driversPath = @"DriversXml.xml"; //XMLSerializer
        string linesPath = @"LinesXml.xml"; //XMLSerializer
        string lineTripsPath = @"LineTripsXml.xml"; //XMLSerializer
        string stopLinesPath = @"StopLinesXml.xml"; //XMLSerializer
        string userTripsPath = @"UserTripsXml.xml"; //XMLSerializer
        string usersPath = @"UsersXml.xml"; //XMLSerializer
        #endregion

        #region Bus
        public Bus GetBus(int id)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            Bus b = (from bus in busesRootElem.Elements()
                     where int.Parse(bus.Element("Id").Value) == id &&
                     bool.Parse(bus.Element("Active").Value) == true
                     select new Bus()
                     {
                         Active = true,
                         Id = uint.Parse(bus.Element("Id").Value),
                         DateRoadAscent = DateTime.Parse(bus.Element("DateRoadAscent").Value),
                         Fuel = int.Parse(bus.Element("Fuel").Value),
                         LastCare = DateTime.Parse(bus.Element("LastCare").Value),
                         LastCareMileage = uint.Parse(bus.Element("LastCareMileage").Value),
                         Mileage = uint.Parse(bus.Element("Mileage").Value),
                         State = (States)int.Parse(bus.Element("State").Value)
                     }
                   ).FirstOrDefault();
            return b;//if b==null return null
        }

        public IEnumerable<Bus> GetBuses()
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);
            return from bus in busesRootElem.Elements()
                   where bool.Parse(bus.Element("Active").Value) == true
                   select new Bus()
                   {
                       Active = true,
                       Id = uint.Parse(bus.Element("Id").Value),
                       DateRoadAscent = DateTime.Parse(bus.Element("DateRoadAscent").Value),
                       Fuel = int.Parse(bus.Element("Fuel").Value),
                       LastCare = DateTime.Parse(bus.Element("LastCare").Value),
                       LastCareMileage = uint.Parse(bus.Element("LastCareMileage").Value),
                       Mileage = uint.Parse(bus.Element("Mileage").Value),
                       State = (States)int.Parse(bus.Element("State").Value)
                   };
        }

        public IEnumerable<Bus> GetBusesBy(Predicate<Bus> predicate)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);
            return from bus in busesRootElem.Elements()
                   let busDO = new Bus()
                   {
                       Active = true,
                       Id = uint.Parse(bus.Element("Id").Value),
                       DateRoadAscent = DateTime.Parse(bus.Element("DateRoadAscent").Value),
                       Fuel = int.Parse(bus.Element("Fuel").Value),
                       LastCare = DateTime.Parse(bus.Element("LastCare").Value),
                       LastCareMileage = uint.Parse(bus.Element("LastCareMileage").Value),
                       Mileage = uint.Parse(bus.Element("Mileage").Value),
                       State = (States)int.Parse(bus.Element("State").Value)
                   }
                   where busDO.Active == true && predicate(busDO)
                   select busDO;

        }

        public void CreateBus(Bus bus)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            var busElem = (from b in busesRootElem.Elements()
                           where int.Parse(b.Element("Id").Value) == bus.Id
                           select b).FirstOrDefault();
            if (busElem != null && bool.Parse(busElem.Element("Active").Value) == true)
                throw new BusExceptionDO((int)bus.Id, "the bus is already exists");
            if (busElem == null)
            {
                busElem = new XElement("Bus",
                          new XElement("Active", bus.Active),
                          new XElement("Id", bus.Id),
                          new XElement("DateRoadAscent", bus.DateRoadAscent),
                          new XElement("Fuel", bus.Fuel),
                          new XElement("Mileage", bus.Mileage),
                          new XElement("LastCareMileage", bus.LastCareMileage),
                          new XElement("LastCare", bus.LastCare),
                          new XElement("State", bus.State));
            }
            else
            {
                busElem.Element("Active").Value = bus.Active.ToString();
                busElem.Element("Id").Value = bus.Id.ToString();
                busElem.Element("DateRoadAscent").Value = bus.DateRoadAscent.ToString();
                busElem.Element("Fuel").Value = bus.Fuel.ToString();
                busElem.Element("Mileage").Value = bus.Mileage.ToString();
                busElem.Element("LastCareMileage").Value = bus.LastCareMileage.ToString();
                busElem.Element("LastCare").Value = bus.LastCare.ToString();
                busElem.Element("State").Value = bus.State.ToString();
            }
            busesRootElem.Add(busElem);
            XMLTools.SaveListToXMLElement(busesRootElem, busesPath);
        }

        public void DeleteBus(int id)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            var busElem = (from b in busesRootElem.Elements()
                           where int.Parse(b.Element("Id").Value) == id &&
                           bool.Parse(b.Element("Active").Value) == true
                           select b).FirstOrDefault();
            if (busElem == null)
                throw new BusExceptionDO((int)id, "the bus is not exists");
            busElem.Element("Active").Value = false.ToString();

            XMLTools.SaveListToXMLElement(busesRootElem, busesPath);
        }

        public void UpdateBus(Bus newBus)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            var busElem = (from b in busesRootElem.Elements()
                           where int.Parse(b.Element("Id").Value) == newBus.Id &&
                           bool.Parse(b.Element("Active").Value) == true
                           select b).FirstOrDefault();
            if (busElem == null)
                throw new BusExceptionDO((int)newBus.Id, "system not found the bus");

            busElem.Element("Active").Value = newBus.Active.ToString();
            busElem.Element("Id").Value = newBus.Id.ToString();
            busElem.Element("DateRoadAscent").Value = newBus.DateRoadAscent.ToString();
            busElem.Element("Fuel").Value = newBus.Fuel.ToString();
            busElem.Element("Mileage").Value = newBus.Mileage.ToString();
            busElem.Element("LastCareMileage").Value = newBus.LastCareMileage.ToString();
            busElem.Element("LastCare").Value = newBus.LastCare.ToString();
            busElem.Element("State").Value = newBus.State.ToString();

            XMLTools.SaveListToXMLElement(busesRootElem, busesPath);
        }
        #endregion

        #region BusStop
        public BusStop GetBusStop(int code)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopsPath);
            BusStop bStop = ListBusStops.Find(BusStop => BusStop.Code == code);

            return bStop; // if bStop == null return null
        }

        public IEnumerable<BusStop> GetStops()
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopsPath);
            return from bStop in ListBusStops
                   where bStop.Active == true
                   select bStop;
        }

        public IEnumerable<BusStop> GetStopsBy(Predicate<BusStop> predicate)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopsPath);
            return from Stop in ListBusStops
                   where Stop.Active == true && predicate(Stop)
                   select Stop;
        }

        public void AddStop(BusStop stop)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopsPath);

            if (ListBusStops.FirstOrDefault(BusStop => BusStop.Code == stop.Code) != null)
                throw new DO.BusStopExceptionDO(stop.Code, "Duplicate BusStop Code");

            //if (GetBusStop(stop.Code) == null)
            //    throw new DO.BusStopExceptionDO(stop.Code, "Missing BusStop Code");

            ListBusStops.Add(stop); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBusStops, busStopsPath);
        }

        public void UpdateBusStop(BusStop busStop)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopsPath);

            int index = ListBusStops.FindIndex((BusStop) => { return BusStop.Active && BusStop.Code == busStop.Code; });
            if (index == -1)
                throw new DO.BusStopExceptionDO(busStop.Code, "System not found the busStop");
            ListBusStops[index] = busStop;

            XMLTools.SaveListToXMLSerializer(ListBusStops, busStopsPath);
        }

        public void UpdateBusStop(int code, Action<BusStop> action)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopsPath);
            
            int index = ListBusStops.FindIndex((BusStop) => { return BusStop.Active && BusStop.Code == code; });
            if (index == -1)
                throw new BusStopExceptionDO(code, "system not found the busStop");
            action(ListBusStops[index]);

            XMLTools.SaveListToXMLSerializer(ListBusStops, busStopsPath);
        }

        public void DeleteBusStop(int code)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopsPath);

            int index = ListBusStops.FindIndex((BusStop) => { return BusStop.Active && BusStop.Code == code; });
            if (index == -1)
                throw new BusStopExceptionDO(code, "The busStop is not exists");
            ListBusStops[index].Active = false;
            
            XMLTools.SaveListToXMLSerializer(ListBusStops, busStopsPath);
        }
        #endregion

        #region Driver

        public void AddDriver(Driver driver)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Driver> GetDrivers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Driver> GetDriversBy(Predicate<Driver> predicate)
        {
            throw new NotImplementedException();
        }

        public Driver GetDriver(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDriver(Driver newDriver)
        {
            throw new NotImplementedException();
        }

        public void UpdateDriver(int id, Action<Driver> action)
        {
            throw new NotImplementedException();
        }

        public void DeleteDriver(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region User
        public User GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string phone, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string phone, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersBy(Predicate<User> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Line
        public int CreateLine(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            //load runing numbers from "runNumbersPath"
            XElement runNumbersElem = XMLTools.LoadListFromXMLElement(runNumbersPath);
            int runNumber = int.Parse(runNumbersElem.Element("Counter").Element("LineCounter").Value);
            line.IdLine = runNumber++;
            runNumbersElem.Element("Counter").Element("LineCounter").Value = runNumber.ToString();
            XMLTools.SaveListToXMLElement(runNumbersElem, runNumbersPath);

            ListLines.Add(line);
            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
            return line.IdLine;
        }

        public Line GetLine(int idLine)
        {
            var lines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            var line = lines.Find((Line) => { return Line.Active && Line.IdLine == idLine; });
            return line;
        }

        public IEnumerable<Line> GetLines()
        {
            var lines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            return from l in lines
                   where l.Active = true
                   select l;
        }

        public IEnumerable<Line> GetLinesBy(Predicate<Line> predicate)
        {
            var lines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            return from l in lines
                   where l.Active = true && predicate(l)
                   select l;
        }

        public void UpdateLine(Line line)
        {
            var lines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            var i = lines.FindIndex(l => l.Active && l.IdLine == line.IdLine);
            if (i == -1)
                throw new LineExceptionDO(line.IdLine, "system not found the line");
            lines[i] = line;
            XMLTools.SaveListToXMLSerializer(lines, linesPath);
        }

        public void UpdateLine(int idLine, Action<Line> action)
        {
            var lines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            var i = lines.FindIndex(l => l.Active && l.IdLine == idLine);
            if (i == -1)
                throw new LineExceptionDO(idLine, "system not found the line");
            action(lines[i]);
            XMLTools.SaveListToXMLSerializer(lines, linesPath);
        }

        public void DeleteLine(int idLine)
        {
            var lines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            int i = lines.FindIndex(l => l.Active && l.IdLine == idLine);
            if (i == -1)
                throw new LineExceptionDO(idLine, "the line is not exists");
            lines[i].Active = false;
            XMLTools.SaveListToXMLSerializer(lines, linesPath);
        }
        #endregion

        #region StopLine
        public void AddStopLine(StopLine stopLine)
        {
            throw new NotImplementedException();
        }

        public void AddRouteStops(IEnumerable<StopLine> stops)
        {
            throw new NotImplementedException();
        }

        public StopLine GetStopLine(int idLine, int codeStop)
        {
            throw new NotImplementedException();
        }

        public StopLine GetStopLineByIndex(int idLine, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StopLine> GetStopLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StopLine> GetStopLinesBy(Predicate<StopLine> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateStopLine(StopLine stopLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateStopLine(int idLine, int codeStop, Action<StopLine> action)
        {
            throw new NotImplementedException();
        }

        public void DeleteStopLine(int idLine, int codeStop)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllStopsInLine(int idLine)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ConsecutiveStops
        public ConsecutiveStops GetConsecutiveStops(int codeStop1, int codeStop2)
        {
            throw new NotImplementedException();
        }

        public void AddConsecutiveStops(ConsecutiveStops consecutiveStops)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConsecutiveStops> GetLstConsecutiveStops()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConsecutiveStops> GetLstConsecutiveStopsBy(Predicate<ConsecutiveStops> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateConsecutiveStops(ConsecutiveStops consecutiveStops)
        {
            throw new NotImplementedException();
        }

        public void UpdateConsecutiveStops(int codeStop1, int codeStop2, Action<ConsecutiveStops> action)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LineTrip
        public int CreateLineTrip(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }

        public LineTrip GetLineTrip(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetLineTrips()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetLineTripsBy(Predicate<LineTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineTrip(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineTrip(int id, Action<LineTrip> action)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip(int idLine, TimeSpan startTime)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
