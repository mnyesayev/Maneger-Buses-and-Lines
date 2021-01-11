using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DalApi;
using BlApi;

namespace Bl
{
    sealed class BlImp : IBL
    {
        readonly IDal dal = DalFactory.GetDal();
        #region Singelton
        static readonly BlImp instance = new BlImp();
        static BlImp() { }// static ctor to ensure instance init is done just before first usage
        BlImp() { } // default => private
        public static BlImp Instance { get => instance; }// The public Instance property to use

        #endregion
        #region BusStop
        public BusStop GetStop(int code)
        {
            var DObusStop=dal.GetBusStop(code);
            if (DObusStop == null) return null;
            var BObusStop = new BusStop();
            DObusStop.CopyPropertiesTo(BObusStop);
            BObusStop.LinesPassInStop = (DObusStop.PassLines == true) ? GetLinesInStop(BObusStop.Code) : default;
            return BObusStop;     
        }
        public void DeleteBusStop(int code)
        {
            try
            {
                dal.DeleteBusStop(code);
            }
            catch (DO.BusStopExceptionDO ex)
            {
                throw new DeleteException("BusStop", code.ToString(), ex.Message, ex);
            }
        }
        public IEnumerable<BusStop> GetBusStops()
        {
            return from BusStop in dal.GetStops().AsParallel()
                   let newBusStop = new BusStop
                   {
                       Latitude = BusStop.Latitude,
                       Longitude=BusStop.Longitude,
                       Code = BusStop.Code,
                       Name = BusStop.Name,
                       MoreInfo = BusStop.MoreInfo,
                       LinesPassInStop = (BusStop.PassLines == true) ? GetLinesInStop(BusStop.Code) : default
                   }
                   orderby newBusStop.Code
                   select newBusStop;
        }
        public BusStop AddStop(BusStop stop)
        {
            if(stop.Code>999999||stop.Code<1)
            {
                throw new AddException("BusStop", stop.Code.ToString());
            }
            if(stop.Name=="")
                throw new AddException("BusStop", stop.Name.ToString());
            if(stop.Latitude>90||stop.Latitude<-90)
                throw new AddException("BusStop", stop.Latitude.ToString());
            if(stop.Longitude>180||stop.Longitude<-180)
                throw new AddException("BusStop", stop.Longitude.ToString());
            try
            {
                var stopDO = new DO.BusStop();
                stop.CopyPropertiesTo(stopDO);
                stopDO.Active = true;
                stopDO.PassLines = false;
                dal.AddStop(stopDO);
            }
            catch (DO.BusStopExceptionDO )
            {
                return null;
            }
            var newStop = new BusStop();
            stop.CopyPropertiesTo(newStop);
            return newStop;
        }
        public IEnumerable<LineOnStop> GetLinesInStop(int code)
        {
            return from Line in GetLines()
                   where Line.StopsInLine.Any((StopLine) => StopLine.CodeStop == code)
                   select new LineOnStop() 
                   {   IdLine = Line.IdLine,
                       NumLine = Line.NumLine,
                       NameFirstLineStop=GetNameStop(Line.StopsInLine.First().CodeStop),
                       NameLastLineStop=GetNameStop(Line.StopsInLine.Last().CodeStop)
                   };
        }
        public string GetNameStop(int code)
        {
            var stop = dal.GetBusStop(code);
            if (stop != null)
                return stop.Name;
            else
                return null;
        }

        BusStop IBL.UpdateName(int code, string name)
        {
            var st = dal.GetBusStop(code);
            if (st == null) return null;
            dal.UpdateBusStop(code, (BusStop) => { BusStop.Name = name; });
            st = dal.GetBusStop(code);
            var busStop = new BusStop();
            return (BusStop)Bl.Cloning.CopyPropertiesToNew(st, busStop.GetType());
        }
        #endregion

        #region user
        public User GetUser(string userName, string password)
        {
            var user = dal.GetUser(userName);
            if (user != null && user.Password == password)
            {
                var newUserBO = new User();
                Bl.Cloning.CopyPropertiesTo(user, newUserBO);
                return newUserBO;
            }
            return null;
        }
        public string RecoverPassword(string phone, DateTime birthday)
        {
            var user = dal.GetUser(phone, birthday);
            if (user == null)
                throw new PasswordRecoveryException(phone);
            return user.Password;
        }

        public void DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region StopLine
        public Line AddStopLine(int idLine, int codeStop, int index)
        {
            if(codeStop<1||codeStop>999999)
                throw new AddException("StopLine", $"{codeStop}", "The this codeStop exceeds from limits of code");
            var st = dal.GetStopLine(idLine, codeStop);
            List<DO.StopLine> stopsInLine = new List<DO.StopLine>(
                dal.GetStopLinesBy((StopLine) => StopLine.IdLine == idLine));
            if (st != null || stopsInLine == null)
                return null;
            var busStop = dal.GetBusStop(codeStop);
            if (busStop == null)
                return null;
            DO.StopLine curStop = null;
            if (index - 1 == stopsInLine.Count())
            {
                curStop = dal.GetStopLineByIndex(idLine, index - 1);
            }
            if (index <= stopsInLine.Count())
                curStop = dal.GetStopLineByIndex(idLine, index);
            if (index<1||index > stopsInLine.Count()+1)
                throw new AddException("StopLine", $"{index}", "The index exceeds the station limit");
            if (curStop == null)
                return null;
            try
            {
                if (index != 1&&index!=stopsInLine.Count+1)
                {
                    var d1 = GetDistance(curStop.PrevStop, codeStop);
                }
            }
            catch (ConsecutiveStopsException ex)
            {
                throw new ConsecutiveStopsException(curStop.PrevStop, codeStop, "No time and distance available", ex);
            }
            try
            {
                if (index != stopsInLine.Count + 1)
                {
                    var d1 = GetDistance(codeStop, curStop.CodeStop);
                }
            }
            catch (ConsecutiveStopsException ex)
            {
                throw new ConsecutiveStopsException(codeStop, curStop.CodeStop, "No time and distance available", ex);
            }
            try
            {
                if (index == stopsInLine.Count() + 1)
                {
                    var d1 = GetDistance(curStop.CodeStop, codeStop);
                }
            }
            catch (ConsecutiveStopsException ex)
            {

                throw new ConsecutiveStopsException(curStop.CodeStop, codeStop, "No time and distance available", ex);
            }

            var l = dal.GetLine(idLine);
            try
            {
                //add to head of route
                if (index == 1)
                {
                    var head = stopsInLine.First();
                    head.PrevStop = codeStop;
                    head.NumStopInLine++;
                    dal.UpdateStopLine(head);
                    foreach (var item in stopsInLine)
                    {
                        if (item.CodeStop == head.CodeStop)
                            continue;
                        dal.UpdateStopLine(item.IdLine, item.CodeStop, (StopLine) => StopLine.NumStopInLine++);
                    }
                    dal.AddStopLine(new DO.StopLine() { NextStop = head.CodeStop, IdLine = idLine, CodeStop = codeStop, NumStopInLine = 1 });
                    dal.UpdateLine(idLine, (Line) => Line.CodeFirstStop = codeStop);
                }
                if (index != 1)
                {
                    var prev = stopsInLine.ElementAt(index - 2);
                    dal.AddStopLine(new DO.StopLine()
                    {
                        PrevStop = prev.CodeStop,
                        IdLine = idLine,
                        CodeStop = codeStop,
                        NumStopInLine = index,
                        NextStop = prev.NextStop
                    });
                    prev.NextStop = codeStop;
                    dal.UpdateStopLine(prev);
                    for (int i = index - 1; i < stopsInLine.Count(); i++)
                    {
                        var t = stopsInLine.ElementAt(i);
                        if (i == index - 1)
                        {
                            dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                            {
                                StopLine.NumStopInLine++;
                                StopLine.PrevStop = codeStop;
                            });
                            continue;
                        }
                        dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                        {
                            StopLine.NumStopInLine++;
                        });
                    }
                    if (index - 1 == stopsInLine.Count())
                    {
                        dal.UpdateLine(idLine, (Line) => Line.CodeLastStop = codeStop);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            dal.UpdateBusStop(codeStop, (BusStop) => BusStop.PassLines = true);
            return new Line()
            {
                Area = (Areas)l.Area,
                IdLine = l.IdLine,
                CodeAgency = (Agency)l.CodeAgency,
                MoreInfo = l.MoreInfo,
                NumLine = l.NumLine,
                StopsInLine = GetStopsInLine(idLine)
            };
        }

        public Line DeleteStopLine(int idLine, int codeStop, int index)
        {
            var st = dal.GetStopLine(idLine, codeStop);
            var stopsInLine = new List<DO.StopLine>(dal.GetStopLinesBy((StopLine) =>
            StopLine.IdLine == idLine));
            if (st == null || stopsInLine.Count == 0)
                return null;
            if (stopsInLine.Count() == 2)
                throw new DeleteException("StopLine",codeStop.ToString(), "You can not delete the line station \nConsider deleting the line instead!");
            if (index > 1)
            {
                var distance = GetDistance(st.PrevStop, st.CodeStop) + GetDistance(st.CodeStop, st.NextStop);
                var time = GetTime(st.PrevStop, st.CodeStop) + GetTime(st.CodeStop, st.NextStop);
                InsertDistanceAndTime(st.PrevStop, st.NextStop, distance, time);
            }
            var l = dal.GetLine(idLine);
            try
            {
                if (index == 1)
                {
                    dal.UpdateLine(idLine, (Line) =>
                    { Line.CodeFirstStop = stopsInLine.ElementAt(1).CodeStop; });
                    dal.DeleteStopLine(idLine, codeStop);
                    for (int i = 1; i < stopsInLine.Count(); i++)
                    {
                        var t = stopsInLine.ElementAt(i);
                        if (i == 1)
                        {
                            dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                            {
                                StopLine.NumStopInLine--;
                                StopLine.PrevStop = 0;
                            });
                            continue;
                        }
                        dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                        {
                            StopLine.NumStopInLine--;
                        });
                    }

                }
                if (index == stopsInLine.Count())
                {
                    dal.UpdateLine(idLine, (Line) =>
                    { Line.CodeFirstStop = stopsInLine.ElementAt(index- 2).CodeStop; });
                    dal.UpdateStopLine(idLine, stopsInLine.ElementAt(index - 2).CodeStop,
                        (StopLine) => StopLine.NextStop = 0);
                    dal.DeleteStopLine(idLine, codeStop);
                }
                if (index != 1 && index != stopsInLine.Count())
                {
                    var prev = stopsInLine.ElementAt(index - 2);
                    prev.NextStop = st.NextStop;
                    dal.UpdateStopLine(prev);
                    dal.DeleteStopLine(idLine, codeStop);
                    for (int i = index; i < stopsInLine.Count(); i++)
                    {
                        var t = stopsInLine.ElementAt(i);
                        if (i == index)
                        {
                            dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                            {
                                StopLine.NumStopInLine--;
                                StopLine.PrevStop = prev.CodeStop;
                            });
                            continue;
                        }
                        dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                        {
                            StopLine.NumStopInLine--;
                        });
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return new Line()
            {
                Area = (Areas)l.Area,
                IdLine = l.IdLine,
                CodeAgency = (Agency)l.CodeAgency,
                MoreInfo = l.MoreInfo,
                NumLine = l.NumLine,
                StopsInLine = GetStopsInLine(idLine)
            };
        }
        public StopLine GetStopInLine(int code, int idLine)
        {
            var doStopLine = dal.GetStopLine(idLine, code);
            if (doStopLine == null) return null;
            var newStopLine = new StopLine();
            doStopLine.CopyPropertiesTo(newStopLine);
            newStopLine.Name = GetNameStop(code);
            newStopLine.AvregeDriveTimeToNext = GetTime(newStopLine.CodeStop, newStopLine.NextStop);
            newStopLine.DistanceToNext = GetDistance(newStopLine.CodeStop, newStopLine.NextStop);
            return newStopLine;
        }
        public IEnumerable<StopLine> GetStopsInLine(int id)
        {
            return from StopLine in dal.GetStopLinesBy((StopLine) =>
            { return StopLine.IdLine == id; })
                   let newStopLine = new BO.StopLine()
                   {
                       CodeStop = StopLine.CodeStop,
                       IdLine = StopLine.IdLine,
                       Name = GetNameStop(StopLine.CodeStop),
                       NumStopInLine = StopLine.NumStopInLine,
                       NextStop = StopLine.NextStop,
                       PrevStop = StopLine.PrevStop,
                       AvregeDriveTimeToNext = GetTime(StopLine.CodeStop, StopLine.NextStop),
                       DistanceToNext = GetDistance(StopLine.CodeStop, StopLine.NextStop)
                   }
                   orderby newStopLine.NumStopInLine
                   select newStopLine;
        }
        public double GetDistance(int code1, int code2)
        {
            if (code2 == 0) return default;
            var cs = dal.GetConsecutiveStops(code1, code2);
            if (cs == null)
            {
                throw new ConsecutiveStopsException(code1, code2, "No information is available on these pair of stations");
            }
            return cs.Distance;
        }

        public TimeSpan GetTime(int code1, int code2)
        {
            if (code2 == 0) return default;
            var cs = dal.GetConsecutiveStops(code1, code2);
            if (cs == null)
            {
                throw new ConsecutiveStopsException(code1, code2, "No information is available on these pair of stations");
            }
            return cs.AvregeDriveTime;
        }
        #endregion

        #region Line
        public IEnumerable<Line> GetLines()
        {
            return from Line in dal.GetLines()
                   let newLine = new Line()
                   {
                       IdLine = Line.IdLine,
                       NumLine = Line.NumLine,
                       Area = (BO.Areas)Line.Area,
                       CodeAgency = (BO.Agency)Line.CodeAgency,
                       StopsInLine = GetStopsInLine(Line.IdLine),
                       MoreInfo = Line.MoreInfo
                   }
                   orderby newLine.NumLine
                   select newLine;
        }
        public Line AddLine(string numLine, Areas area, IEnumerable<StopLine> stops, string moreInfo)
        {
            int idLine = dal.CreateLine(new DO.Line()
            {
                NumLine = numLine,
                Active = true,
                Area = (DO.Areas)area,
                CodeFirstStop = stops.First().CodeStop,
                CodeLastStop = stops.Last().CodeStop,
                MoreInfo = moreInfo
            });
            var stopsLine = from StopLine in stops
                            let sL = new DO.StopLine()
                            {
                                IdLine = idLine,
                                PrevStop = (StopLine.NumStopInLine == 1) ? 0 : stops.ElementAt(StopLine.NumStopInLine - 2).CodeStop,
                                NextStop = (StopLine.NumStopInLine == stops.Count()) ? 0 : stops.ElementAt(StopLine.NumStopInLine).CodeStop,
                                CodeStop = StopLine.CodeStop,
                                NumStopInLine = StopLine.NumStopInLine
                            }
                            select sL;
            dal.AddRouteStops(stopsLine);
            foreach (var item in stops)
            {
                dal.UpdateBusStop(item.CodeStop, (stop) => stop.PassLines = true);
            }
            return new Line()
            {
                IdLine = idLine,
                NumLine = numLine,
                Area = area,
                StopsInLine = GetStopsInLine(idLine),
                MoreInfo = moreInfo
            };
        }
        public bool UpdateLine(int idLine, string numLine, Areas area, Agency agency)
        {
            var l = dal.GetLine(idLine);
            if (l == null) return false;
            try
            {
                dal.UpdateLine(idLine, (Line) =>
                {
                    Line.NumLine = numLine;
                    Line.Area = (DO.Areas)area;
                    Line.CodeAgency = (DO.Agency)agency;
                });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool DeleteLine(int idLine)
        {
            var l = dal.GetLine(idLine);
            if (l == null) return false;
            try
            {
                dal.DeleteLine(idLine);
                dal.DeleteAllStopsInLine(idLine);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        public void InsertDistanceAndTime(int code1, int code2, double distance, TimeSpan time)
        {
            var cs = dal.GetConsecutiveStops(code1, code2);
            if (cs != null)
            {
                dal.UpdateConsecutiveStops(code1, code2, (cstops) => { cstops.Distance = distance; cstops.AvregeDriveTime = time; });
                return;
            }
            if (cs == null)
            {
                dal.AddConsecutiveStops(new DO.ConsecutiveStops()
                {
                    CodeBusStop1 = code1,
                    CodeBusStop2 = code2,
                    AvregeDriveTime = time,
                    Distance = distance
                });
            }
        }

        #region Bus
        public Bus AddBus(int id, DateTime dra, Action<Bus> action)
        {
            var temp = new Bus() { Id = (uint)id, DateRoadAscent = dra };
            throw new NotImplementedException();
        }
        public Bus AddBus(Bus bus)
        {
            try
            {
                DO.Bus busDo = new DO.Bus();
                Bl.Cloning.CopyPropertiesTo(bus, busDo);
                dal.CreateBus(busDo);
            }
            catch (DO.BusExceptionDO ex)
            {
                throw new AddException("Bus", bus.Id.ToString(), $"the bus {ex.Id} already exits", ex);
            }
            return bus;
        }
        public IEnumerable<Bus> GetBuses()
        {
            return from Bus in dal.GetBuses()
                   let newBus = new Bus
                   {
                       DateRoadAscent = Bus.DateRoadAscent,
                       Fuel = Bus.Fuel,
                       Id = Bus.Id,
                       State = (States)Bus.State,
                       LastCare = Bus.LastCare,
                       LastCareMileage = Bus.LastCareMileage,
                       Mileage = Bus.Mileage
                   }
                   orderby newBus.Id
                   select newBus;
        }

        public void DeleteBus(int id)
        {
            try
            {
                dal.DeleteBus(id);
            }
            catch (DO.BusExceptionDO ex)
            {
                throw new DeleteException("Bus", id.ToString(), ex.Message, ex);
            }
        }

        public bool CheckFuel(int id, int distance)
        {
            var b = dal.GetBus(id);
            if (b == null)
                return false;//should be "throw"
            if (b.Fuel - distance > 0)
                return false;
            return true;
        }

        public bool CheckCare(int id, int distance)
        {
            var b = dal.GetBus(id);
            if (b == null)
                return false;//should be "throw"
            if (DateTime.Compare(b.LastCare, DateTime.Now.AddYears(-1)) <= 0)
                return true;
            if (b.Mileage + distance - b.LastCareMileage >= 20000)
                return true;
            return false;
        }

        public Bus Care()
        {
            throw new NotImplementedException();
        }

        public Bus Fuel()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Driver
        public IEnumerable<Driver> GetDrivers()
        {
            return from Driver in dal.GetDrivers()
                   let newDriver = new Driver()
                   {
                       Id = Driver.Id,
                       Name = Driver.Name,
                       Seniority = Driver.Seniority
                   }
                   orderby newDriver.Name
                   select newDriver;
        }

        public Driver AddDriver(int id, string name, int seniority)
        {
            if (id > 999999999)
                throw new AddException("Driver", id.ToString(), "the Id not valid!!");
            try
            {
                dal.AddDriver(new DO.Driver() { Id = id, Name = name, Seniority = seniority, Active = true });
            }
            catch (DO.DriverExceptionDO ex)
            {
                throw new AddException("Driver", id.ToString(), ex.Message, ex);
            }
            return new Driver() { Id = id, Name = name, Seniority = seniority };
        }

        public void DeleteDriver(int id)
        {
            if (id > 999999999)
                throw new DeleteException("Driver", id.ToString(), "the Id not valid!!");
            try
            {
                dal.DeleteDriver(id);
            }
            catch (DO.DriverExceptionDO ex)
            {
                throw new DeleteException("Driver", id.ToString(), ex.ToString(), ex);
            }
        }

        public void EditDriver(int id, string name, int seniority)
        {
            if (id > 999999999)
                throw new IdException("Driver", id.ToString(), "this id not valid!!");
            try
            {
                dal.UpdateDriver(id, (Driver) =>
                {
                    if (Driver.Seniority < seniority)
                        Driver.Seniority = seniority;
                    if (name != Driver.Name)
                        Driver.Name = name;
                });
            }
            catch (DO.DriverExceptionDO ex)
            {
                throw new IdException("Driver", id.ToString(), ex.Message, ex);
            }
        }
        #endregion

        #region LineTrip
        public IEnumerable<LineTrip> GetLineTrips()
        {
            return from LineTrip in dal.GetLineTrips()
                   let newLineTrip = new LineTrip()
                   {
                       Id = LineTrip.Id,
                       IdLine = LineTrip.IdLine,
                       numLine = GetNumLine(LineTrip.IdLine),
                       Frequency = LineTrip.Frequency,
                       DepartureSchedule = getSchedule(LineTrip.StartTime, LineTrip.EndTime, LineTrip.Frequency)
                   }
                   orderby newLineTrip.numLine
                   select newLineTrip;
        }
        IEnumerable<TimeSpan> getSchedule(TimeSpan startTime, TimeSpan endTime, int f)
        {
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            var t1 = startTime;
            var m = 60 / f;
            int tm, th;
            timeSpans.Add(startTime);
            while (t1 < endTime)
            {
                if (t1.Minutes + m > 59)
                {
                    tm = 60 - t1.Minutes + m;
                    th = t1.Hours + 1;
                    timeSpans.Add(startTime.Add(new TimeSpan(th, tm, t1.Seconds)));
                    t1 = timeSpans[timeSpans.Count - 1];
                    continue;
                }
                timeSpans.Add(startTime.Add(new TimeSpan(t1.Hours, t1.Minutes + m, t1.Seconds)));
            }
            timeSpans.Add(endTime);
            return timeSpans;
        }
        public string GetNumLine(int idLine)
        {
            var l = dal.GetLine(idLine);
            if (l == null)
                return null;
            return l.NumLine;
        }
        public LineTrip UpdateLineSchedule(int id, TimeSpan startTime, TimeSpan endTime, int f)
        {
            if (startTime > endTime || f < 0)
            {
                return null;
            }
            try
            {
                dal.UpdateLineTrip(id, (LineTrip) =>
                 {
                     if (LineTrip.StartTime != startTime)
                         LineTrip.StartTime = startTime;
                     if (LineTrip.EndTime != LineTrip.EndTime)
                         LineTrip.StartTime = startTime;
                     if (LineTrip.Frequency != f)
                         LineTrip.Frequency = f;
                 });
            }
            catch (DO.LineTripExceptionDO ex)
            {
                throw new IdException("LineTrip", id.ToString(), ex.Message, ex);
            }
            var lt = dal.GetLineTrip(id);
            return new LineTrip()
            {
                Id = lt.Id,
                IdLine = lt.IdLine,
                numLine = GetNumLine(lt.IdLine),
                Frequency = lt.Frequency,
                DepartureSchedule = getSchedule(lt.StartTime, lt.EndTime, lt.Frequency)
            };
        }

        public LineTrip AddLineTrip(int idLine, TimeSpan start, TimeSpan end, int f)
        {
            if (start > end || f < 0)
                return null;
            var id = dal.CreateLineTrip(new DO.LineTrip()
            {
                Active = true,
                IdLine = idLine,
                StartTime = start,
                EndTime = end,
                Frequency = f
            });
            return new LineTrip()
            {
                Frequency = f,
                Id = id,
                IdLine = idLine,
                numLine = GetNumLine(idLine),
                DepartureSchedule = getSchedule(start, end, f)
            };
        }

       
        #endregion
    }
}
